using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

public sealed class EnemyPositionSystem : ReactiveSystem<GameEntity>
{
    IGroup<GameEntity> _movableGroups;
    public EnemyPositionSystem(Contexts contexts) : base(contexts.game) {
        //_movableGroups = pools.core.GetGroup(CoreMatcher.EnemyView);
        _movableGroups = contexts.game.GetGroup(GameMatcher.EnemyView);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.PlayerPosition.Added());
    }

    protected override bool Filter(GameEntity entity) {

        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            foreach (var enemy in _movableGroups.GetEntities())
            {
                enemy.enemyView.controller.AIController.RecalculatePath(e.playerPosition.position);
            }
        }
    }
    
}

