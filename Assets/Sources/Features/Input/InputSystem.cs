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
            InputEntity input = GetInputEntity();
           
            input.ReplaceMoveInput(_playerController.accelerate);
            input.ReplaceInputOwner(PLAYER1_ID);
            
        }

        if (_playerController.IsFire())
        {
            InputEntity inputShoot = _pools.input.CreateEntity();

            uint cannonId = 1;
            CannonParams cannonParams = new CannonParams(cannonId);

            inputShoot.AddCannonShoot(cannonParams);
            inputShoot.AddInputOwner(PLAYER1_ID);
        }
    }

    //<summary>
    // We assume that the input entity is present in a single copy
    //</summary>
    public InputEntity GetInputEntity()
    {
        InputEntity[] inputs = _pools.input.GetEntities();

        // we assume that the input entity is present in a single copy
        if (inputs.Length == 0)
            return _pools.input.CreateEntity();
        else
            return inputs[0];
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
