using System;
using Entitas;
using UnityEngine;

public sealed class InputSystem : IExecuteSystem, IInitializeSystem, ICleanupSystem {

    const string PLAYER1_ID = "Player1";

    Contexts _pools;
    IGroup<InputEntity> _moveInputs;
    PlayerInputController _playerController;

    public InputSystem(PlayerInputController playerController,Contexts contexts)
    {
        _playerController = playerController;
        _pools = contexts;

        //_moveInputs = contexts.input.GetGroup
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
            InputEntity[] inputs = _pools.input.GetEntities();
            if (inputs.Length == 0)
            {
                InputEntity e = _pools.input.CreateEntity();
                e.ReplaceMoveInput(_playerController.accelerate);
                e.ReplaceInputOwner(PLAYER1_ID);
            }
            else
            {
                inputs[0].ReplaceMoveInput(_playerController.accelerate);
                inputs[0].ReplaceInputOwner(PLAYER1_ID);
            }
        }
    }

    public void Cleanup() {
        //foreach(var e in _moveInputs.GetEntities()) {
        //    _pools.input.DestroyEntity(e);
        //}
    }

    public void Initialize()
    {
        /*_pools.input.CreateEntity()
               .AddMoveInput(_playerControlController.inputAccelerate)
               .AddInputOwner(PLAYER1_ID);*/
    }
}
