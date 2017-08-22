using System.Collections.Generic;
using Entitas;
using UnityEngine;
using KBEngine;

public sealed class RenderPositionSystem : IExecuteSystem
{
    const string PLAYER_ID = "Player1";
   // public EntityCollector entityCollector { get { return _groupObserver; } }

    //EntityCollector _groupObserver;
    IGroup<InputEntity> inputs;
    Contexts _pools;
    private UltimateJoystick _joystick;
    PlayerMovementController move1;
    public RenderPositionSystem(Contexts contexts, UltimateJoystick joystick, Dictionary<PlayerInputController.speedTypes, float> speedMap, float masterY)
    {
        _pools = contexts;
        inputs = contexts.input.GetGroup(InputMatcher.MoveInput);
        _joystick = joystick;
        
        move1 = new PlayerMovementController(speedMap, masterY);
    }

    float lastAc = 0;
    public void Execute()
    {    
        var player = _pools.game.GetEntityWithPlayerId(PLAYER_ID);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("KeyCode.Space");
            KBEngine.Event.fireIn("jump");
        }

        if (inputs.count > 0)
            lastAc = inputs.GetEntities()[0].moveInput.accelerate;

        move1.Move (
                        player.playerView.controller.shipDirectional.GetShipDirectional(),
                        player.playerView.controller.rigidbody,
                        _joystick.GetPosition(),
                        lastAc
                   );

        player.ReplacePlayerPosition(player.playerView.controller.transform.position);
    }
}
