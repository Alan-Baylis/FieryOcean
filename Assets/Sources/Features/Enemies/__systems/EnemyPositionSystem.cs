using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

public sealed class EnemyPositionSystem : ReactiveSystem
{
    Group _movableGroups;
    public EnemyPositionSystem(Contexts contexts) : base(contexts.core) {
        //_movableGroups = pools.core.GetGroup(CoreMatcher.EnemyView);
        _movableGroups = contexts.core.GetGroup(CoreMatcher.EnemyView);
        //contexts.core.ge
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.PlayerPosition);
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }

    protected override void Execute(List<Entity> entities)
    {
        foreach(var e in entities)
        {
            foreach (var enemy in _movableGroups.GetEntities())
            {
                enemy.enemyView.controller.AIController.RecalculatePath(e.playerPosition.position);
            }
        }
    }
}

