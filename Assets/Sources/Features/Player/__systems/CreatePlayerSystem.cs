using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CreatePlayerSystem : ISetPools, IInitializeSystem {
    private Vector3 _position;
    Contexts _pools;

    public void SetPools(Contexts pools) {
        _pools = pools;
    }
    public CreatePlayerSystem(Vector3 playerStartPosition)
    {
        _position = playerStartPosition;
    }

    public void Initialize() {
        _pools.blueprints.blueprints.instance
              .ApplyPlayer1(_pools.core.CreateEntity(), _position);
    }
}
