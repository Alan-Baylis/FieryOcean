using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

public sealed class EnemyPositionSystem : ISetPools, IReactiveSystem
{
    Group _movableGroups;
    public TriggerOnEvent trigger { get { return CoreMatcher.PlayerPosition.OnEntityAdded(); } }

    public void Execute(List<Entity> entities)
    {
        foreach(var e in entities)
        {
            foreach (var enemy in _movableGroups.GetEntities())
            {
                enemy.enemyView.controller.AIController.RecalculatePath(e.playerPosition.position);
            }
        }
    }

    public void SetPools(Pools pools)
    {
        _movableGroups = pools.core.GetGroup(CoreMatcher.EnemyView);
    }
}

