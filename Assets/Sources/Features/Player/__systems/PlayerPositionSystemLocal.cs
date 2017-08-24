using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class PlayerPositionSystemLocal : IExecuteSystem
{
    const string PLAYER_ID = "Player1";

    IGroup<InputEntity> inputs;
    Contexts _pools;
    private UltimateJoystick _joystick;
    PlayerMovementController move;
    public PlayerPositionSystemLocal(Contexts contexts, UltimateJoystick joystick, Dictionary<PlayerInputController.speedTypes, float> speedMap, Vector3 startPosition)
    {
        _pools = contexts;
        inputs = contexts.input.GetGroup(InputMatcher.MoveInput);
        _joystick = joystick;

        move = new PlayerMovementController(speedMap, startPosition.y);
        _beforePosition = startPosition;
    }

    float lastAc = 0;
    Vector3 _nextPosition;
    Vector3 _beforePosition;
    float delta;

    public void Execute()
    {
        var player = _pools.game.GetEntityWithPlayerId(PLAYER_ID);

        if (inputs.count > 0)
            lastAc = inputs.GetEntities()[0].moveInput.accelerate;

        _nextPosition = move.Move(  player.playerView.controller.shipDirectional.GetShipDirectional(),
                                    player.playerView.controller.rigidbody,
                                    _joystick.GetPosition(),
                                    lastAc                                                                      );

        delta += Vector3.Distance(_beforePosition, _nextPosition);

        if (delta > 2f)
        {
            player.ReplacePlayerPosition(player.playerView.controller.transform.position);
            _beforePosition = _nextPosition;
            delta = 0;
        }
    }
}
