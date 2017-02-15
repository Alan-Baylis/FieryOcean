using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public sealed class CreateEnemySystem : ISetPools, IInitializeSystem
{
    private Vector3 _position;
    Pools _pools;

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
}

