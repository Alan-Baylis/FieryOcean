using System;
using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class InputSystem : IExecuteSystem, IInitializeSystem, ICleanupSystem {

    const string PLAYER1_ID = "Player1";

    Contexts _contexts;
    IGroup<InputEntity> _moveInputs;
    IGroup<InputEntity> _cannonShootInputs;
    PlayerInputController _playerController;
    Dictionary<Int32, Int32> selectedTurrets;

    public InputSystem(PlayerInputController playerController, Contexts contexts)
    {
        selectedTurrets = new Dictionary<int, int>();
        _playerController = playerController;

        _playerController.SetActions(SpeedInput, FireInput, null /*SelectTurretInput*/);

        _contexts = contexts;
        _moveInputs = contexts.input.GetGroup(InputMatcher.MoveInput);
        _cannonShootInputs = contexts.input.GetGroup(InputMatcher.CannonShoot);
        //_moveInputs = contexts.input.GetGroup
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

    //public void SelectTurretInput(PlayerInputController pc, InputEntity ie)
    //{
    //    ie.AddSelectTurret(1, true);

    //    int i;
    //    if (!selectedTurrets.TryGetValue(1, out i))
    //        selectedTurrets.Add(1, 1);
    //}

    public void Execute()
    {
        Queue<Action<PlayerInputController,InputEntity>> queueActions = _playerController.GetQueue();

        int count = queueActions.Count;

        if (count > 0 || selectedTurrets.Count>0)
        {
            InputEntity ie = GetInputEntity();

            for (int i = 0; i < count; i++)
            {
                var ac = queueActions.Dequeue();
                if (ac != null)
                    ac(_playerController, ie);
            }

            foreach (KeyValuePair<Int32, Int32> id in selectedTurrets)
            {
                ie.ReplaceSelectTurret(id.Key, true);
            }
        }
    }

    //<summary>
    // We assume that the input entity is present in a single copy
    //</summary>
    public InputEntity GetInputEntity()
    {
        InputEntity[] inputs = _contexts.input.GetEntities();

        // we assume that the input entity is present in a single copy
        if (inputs.Length == 0)
            return _contexts.input.CreateEntity();
        else
            return inputs[0];
    }

    public void Cleanup() {
        //foreach(var e in _cannonShootInputs.GetEntities()) { e.Destroy(); }
        //foreach (var e in _moveInputs.GetEntities()) { e.Destroy(); }

        foreach (var e in _contexts.input.GetEntities())
        {
            e.Destroy();
        }
    }

    public void Initialize()
    {
        /*_pools.input.CreateEntity()
               .AddMoveInput(_playerControlController.inputAccelerate)
               .AddInputOwner(PLAYER1_ID);*/
    }
}
