using System;
using Entitas;
using UnityEngine;

public sealed class InputSystem : IExecuteSystem, IInitializeSystem, ICleanupSystem {

    const string PLAYER1_ID = "Player1";

    Contexts _pools;
    Group _moveInputs;
    PlayerInputController _playerController;

    public InputSystem(PlayerInputController playerController, Contexts contexts)
    {
        _pools = contexts;
        _moveInputs = _pools.input.GetGroup(InputMatcher.MoveInput);
        _playerController = playerController;
    }

    // TODO Entitas 0.36.0 Migration (constructor)
    //public void SetPools(Contexts pools) {
    //    _pools = pools;
    //    _moveInputs = pools.input.GetGroup(InputMatcher.MoveInput);
    //}

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
