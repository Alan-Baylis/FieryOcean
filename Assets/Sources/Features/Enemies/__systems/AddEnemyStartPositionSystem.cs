using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

class AddEnemyStartPositionSystem : ISetPool, IReactiveSystem
{
    public TriggerOnEvent trigger { get { return CoreMatcher.EnemyView.OnEntityAdded(); } }

    Context _pool;
    Group _enemiesGroup;
    public void SetPool(Context pool)
    {
        _pool = pool;
        _enemiesGroup = pool.GetGroup(Matcher.AllOf(CoreMatcher.Enemy));
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            //e.enemyView.controller.transform.position = e.enemyPosition.position;
            e.enemyView.controller.transform.position = e.position.value;

        }
    }
}



