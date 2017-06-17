using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

class AddEnemyStartPositionSystem : ReactiveSystem
{
    public AddEnemyStartPositionSystem(Contexts contexts) : base(contexts.core) {
       // _pool = pool;
       // _enemiesGroup = pool.GetGroup(Matcher.AllOf(CoreMatcher.Enemy));
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.EnemyView);
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }

    Context _pool;
    Group _enemiesGroup;
    

    protected override void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            //e.enemyView.controller.transform.position = e.enemyPosition.position;
            e.enemyView.controller.transform.position = e.position.value;

        }
    }
}



