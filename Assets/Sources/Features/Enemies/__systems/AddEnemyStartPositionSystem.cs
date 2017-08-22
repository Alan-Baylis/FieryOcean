using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

class AddEnemyStartPositionSystem : ReactiveSystem<GameEntity>
{
    public AddEnemyStartPositionSystem(Contexts contexts) : base(contexts.game) {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.EnemyView.Added());
    }

    protected override bool Filter(GameEntity entity) {

        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            //e.enemyView.controller.transform.position = e.enemyPosition.position;
            e.enemyView.controller.transform.position = e.position.value;

        }
    }
}



