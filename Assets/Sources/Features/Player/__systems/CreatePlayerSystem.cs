using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CreatePlayerSystem : IInitializeSystem {
    private Vector3 _position;
    Contexts _pools;

    public CreatePlayerSystem(Contexts pools, Vector3 playerStartPosition)
    {
        _pools = pools;
        _position = playerStartPosition;
    }

    public void Initialize() {
        _pools.game.CreatePlayer(_position);
    }
}
