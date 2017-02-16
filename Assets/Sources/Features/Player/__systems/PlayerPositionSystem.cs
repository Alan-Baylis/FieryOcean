using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class PlayerPositionSystem : ISetPools, IExecuteSystem
{
    const string PLAYER_ID = "Player1";
   // public EntityCollector entityCollector { get { return _groupObserver; } }

    //EntityCollector _groupObserver;
    Group inputs;
    Pools _pools;
    private UltimateJoystick _joystick;
    PlayerMovementController move1;
    public PlayerPositionSystem(UltimateJoystick joystick, Dictionary<PlayerInputController.speedTypes, float> speedMap, Vector3 startPosition)
    {
        _joystick = joystick;
        
        move1 = new PlayerMovementController(speedMap, startPosition.y);
        _beforePosition = startPosition;
    }

    public void SetPools(Pools pools) {
        /*_groupObserver = new [] { pools.core, pools.bullets }
            .CreateEntityCollector(Matcher.AllOf(CoreMatcher.PlayerView,  CoreMatcher.Position, CoreMatcher.Forse));
            */

        inputs = pools.input.GetGroup(InputMatcher.MoveInput);
        _pools = pools;
    }

    float lastAc = 0;
    Vector3 _nextPosition;
    Vector3 _beforePosition;
    public void Execute()
    {    
        var player = _pools.core.GetEntityWithPlayerId(PLAYER_ID);
       
        if (inputs.count > 0)
            lastAc = inputs.GetEntities()[0].moveInput.accelerate;

        _nextPosition = move1.Move( player.playerView.controller.shipDirectional.GetShipDirectional(),
                    player.playerView.controller.rigidbody,
                    _joystick.GetPosition(),
                    lastAc                                                              );

        if(Vector3.Distance(_beforePosition,_nextPosition)>2f)
            player.ReplacePlayerPosition(player.playerView.controller.transform.position);
    }
}