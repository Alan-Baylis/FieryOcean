using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RenderPositionSystem : ISetPools, IExecuteSystem
{
    const string PLAYER_ID = "Player1";
    public EntityCollector entityCollector { get { return _groupObserver; } }

    EntityCollector _groupObserver;
    Group inputs;
    Pools _pools;
    private UltimateJoystick _joystick;
    Movement1 move1;
    public RenderPositionSystem(UltimateJoystick joystick, Dictionary<PlayerInputController.speedTypes, float> speedMap)
    {
        _joystick = joystick;
        
        move1 = new Movement1(speedMap);
    }

    public void SetPools(Pools pools) {
        /*_groupObserver = new [] { pools.core, pools.bullets }
            .CreateEntityCollector(Matcher.AllOf(CoreMatcher.PlayerView,  CoreMatcher.Position, CoreMatcher.Forse));
            */

        inputs = pools.input.GetGroup(InputMatcher.MoveInput);
        _pools = pools;
    }

    float lastAc = 0;
    
    public void Execute()
    {    
        var player = _pools.core.GetEntityWithPlayerId(PLAYER_ID);
        Vector3 direct = player.playerView.controller.shipDirectional.GetShipDirectional().normalized;
        Vector3 tmp = player.playerView.controller.transform.position;
        //tmp.y = 2f;
        player.playerView.controller.transform.position = tmp;

        //Debug.Log("++++++++ Directional ++++++++++");
        //Debug.Log(direct);

        if (inputs.count > 0)
            lastAc = inputs.GetEntities()[0].moveInput.accelerate;

        move1.Move(player.playerView.controller.shipDirectional.GetShipDirectional(),
                   player.playerView.controller.rigidbody,
                   _joystick.GetPosition(),
                   lastAc,
                   player.playerView.controller.transform);
                   
    }
}
