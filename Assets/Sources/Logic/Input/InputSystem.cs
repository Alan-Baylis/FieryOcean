using System;
using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class InputSystem : IExecuteSystem, IInitializeSystem, ICleanupSystem {

    const string PLAYER1_ID = "Player1";

    Contexts _pools;
    IGroup<InputEntity> _moveInputs;
    IGroup<InputEntity> _cannonShootInputs;
    PlayerInputController _playerController;

    public InputSystem(PlayerInputController playerController, Contexts contexts)
    {
        _playerController = playerController;

        _playerController.SetActions(SpeedInput, FireInput, null);

        _pools = contexts;
        _moveInputs = contexts.input.GetGroup(InputMatcher.MoveInput);
        _cannonShootInputs = contexts.input.GetGroup(InputMatcher.CannonShoot);
        //_moveInputs = contexts.input.GetGroup
    }
    
    public void SpeedInput(PlayerInputController pc)
    {
        InputEntity input = GetInputEntity();

        input.ReplaceMoveInput(pc.accelerate);
        input.ReplaceInputOwner(PLAYER1_ID);
    }

    public void FireInput(PlayerInputController pc)
    {
        InputEntity inputShoot = _pools.input.CreateEntity();

        uint cannonId = 1;
        inputShoot.AddCannonShoot(new CannonParams(cannonId));
        inputShoot.AddInputOwner(PLAYER1_ID);
    }

    public void SelectTurretInput(PlayerInputController pc)
    { }

    public void Execute()
    {
        Queue<Action<PlayerInputController>> queueActions = _playerController.GetQueue();

        for (int i = 0; i < queueActions.Count; i++) {
            var ac = queueActions.Dequeue(); 
            if(ac!=null)
                ac(_playerController);
        }
    }

    //<summary>
    // We assume that the input entity is present in a single copy
    //</summary>
    public InputEntity GetInputEntity()
    {
        InputEntity[] inputs = _pools.input.GetEntities();

        // we assume that the input entity is present in a single copy
        if (inputs.Length == 0)  return _pools.input.CreateEntity();
        else   return inputs[0];
    }

    public void Cleanup() {
        foreach(var e in _cannonShootInputs.GetEntities()) { e.Destroy(); }
        foreach (var e in _moveInputs.GetEntities()) { e.Destroy(); }
    }

    public void Initialize()
    {
        /*_pools.input.CreateEntity()
               .AddMoveInput(_playerControlController.inputAccelerate)
               .AddInputOwner(PLAYER1_ID);*/
    }
}
