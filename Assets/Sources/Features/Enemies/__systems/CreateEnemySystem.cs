using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public sealed class CreateEnemySystem : ISetPools, IInitializeSystem, IReactiveSystem
{
    private Vector3 _position;
    Pools _pools;
    public TriggerOnEvent trigger {  get  {  return CoreMatcher.UnitAdd.OnEntityAdded();    }    }

    public void SetPools(Pools pools)
    {
        _pools = pools;
    }

    void IInitializeSystem.Initialize()
    {
        _pools.blueprints.blueprints.instance.ApplyEnemy(_pools.core.CreateEntity(),_position);
    }

    public CreateEnemySystem(Vector3[] positions)
    {
        _position = positions[0];
    }
    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            _pools.blueprints.blueprints.instance.AddUnit(_pools.core.CreateEntity(), entity.unitAdd.entity);
        }
    }
}

