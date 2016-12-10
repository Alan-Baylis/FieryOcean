using System;
using Entitas;
using UnityEngine;

public sealed class InputSystem : ISetPools, IExecuteSystem, IInitializeSystem, ICleanupSystem {

    const string PLAYER1_ID = "Player1";

    Pools _pools;
    Group _moveInputs;
    PlayerControlController _playerControlController;

    public InputSystem(PlayerControlController playerControlController)
    {
        _playerControlController = playerControlController;
    }

    public void SetPools(Pools pools) {
        _pools = pools;
        _moveInputs = pools.input.GetGroup(InputMatcher.MoveInput);
    }

    PlayerControlController.speedTypes _curSpeedType = PlayerControlController.speedTypes.Stop;

    public void Execute()
    {
        if (_curSpeedType != _playerControlController.currentSpeedType)
        {
            _curSpeedType = _playerControlController.currentSpeedType;

            _pools.input.CreateEntity()
               .AddMoveInput(_playerControlController.inputAccelerate)
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
