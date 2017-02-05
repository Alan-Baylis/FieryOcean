using System;
using Entitas;
using UnityEngine;

public sealed class InputSystem : ISetPools, IExecuteSystem, IInitializeSystem, ICleanupSystem {

    const string PLAYER1_ID = "Player1";

    Pools _pools;
    Group _moveInputs;
    PlayerInputController _playerController;

    public InputSystem(PlayerInputController playerController)
    {
        _playerController = playerController;
    }

    public void SetPools(Pools pools) {
        _pools = pools;
        _moveInputs = pools.input.GetGroup(InputMatcher.MoveInput);
    }

    public void Execute()
    {
        if (_playerController.IsSpeedChanged)
        {
            _pools.input.CreateEntity()
               .AddMoveInput(_playerController.accelerate)
               .AddInputOwner(PLAYER1_ID);
        }
    }

    public void Cleanup() {
        foreach(var e in _moveInputs.GetEntities()) {
            _pools.input.DestroyEntity(e);
        }
    }

    public void Initialize()
    {
        /*_pools.input.CreateEntity()
               .AddMoveInput(_playerControlController.inputAccelerate)
               .AddInputOwner(PLAYER1_ID);*/
    }
}
