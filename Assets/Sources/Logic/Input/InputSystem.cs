using System;
using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class InputSystem : IExecuteSystem, ICleanupSystem {

    const string PLAYER1_ID = "Player1";

    Contexts _contexts;
    PlayerInputController _playerController;

    public InputSystem(PlayerInputController playerController, Contexts contexts)
    {
        _playerController = playerController;
        _playerController.SetActions(SpeedInput, FireInput, null);

        _contexts = contexts;
    }
    
    public void SpeedInput(PlayerInputController pc, InputEntity ie)
    {
        ie.ReplaceMoveInput(pc.accelerate);
        ie.ReplaceInputOwner(PLAYER1_ID);
    }

    public void FireInput(PlayerInputController pc, InputEntity ie)
    {
        uint cannonId = 1;
        ie.ReplaceCannonShoot(new CannonParams(cannonId));
        ie.ReplaceInputOwner(PLAYER1_ID);
    }

    public void Execute()
    {
        Queue<Action<PlayerInputController,InputEntity>> queueActions = _playerController.GetQueue();

        int count = queueActions.Count;

        if (count > 0 )
        {
            InputEntity ie = Contexts.sharedInstance.input.CreateEntity();

            for (int i = 0; i < count; i++)
            {
                var ac = queueActions.Dequeue();
                if (ac != null)
                    ac(_playerController, ie);
            }
        }
    }

    public void Cleanup() {
        foreach (var e in _contexts.input.GetEntities())
        {
            e.Destroy();
        }
    }
}
