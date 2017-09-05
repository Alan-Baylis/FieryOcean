using System.Collections.Generic;
using Entitas;
using UnityEngine;
using KBEngine;

public sealed class PlayerPositionSystem : IExecuteSystem
{
    const string PLAYER_ID = "Player1";
   // public EntityCollector entityCollector { get { return _groupObserver; } }

    //EntityCollector _groupObserver;
    IGroup<InputEntity> inputs;
    Contexts _pools;
    private UltimateJoystick _joystick;
    PlayerMovementController movePlayer;
    public PlayerPositionSystem(Contexts contexts, UltimateJoystick joystick, Dictionary<PlayerInputController.speedTypes, float> speedMap, Vector3 startPosition)
    {
        _pools = contexts;
        inputs = contexts.input.GetGroup(InputMatcher.MoveInput);
        _joystick = joystick;
        
        movePlayer = new PlayerMovementController(speedMap, startPosition.y);
        _beforePosition = startPosition;
    }

    float lastAc = 0;
    Vector3 _nextPosition;
    Vector3 _beforePosition;
    float delta;

    public void Execute()
    {    
        var player = _pools.game.GetEntityWithPlayerId(PLAYER_ID);
        createPlayer(player);

        if (inputs.count > 0)
            lastAc = inputs.GetEntities()[0].moveInput.accelerate;

        _nextPosition = movePlayer.Move( player.playerView.controller.shipDirectional.GetShipDirectional(),
                                    player.playerView.controller.rigidbody,
                                    _joystick.GetPosition(),
                                    lastAc                                                              );

        delta += Vector3.Distance(_beforePosition, _nextPosition);

        if (delta > 2f)
        {
            player.ReplacePlayerPosition(player.playerView.controller.transform.position);
            _beforePosition = _nextPosition;
            delta = 0;
        }
            
    }

    private UnityEngine.GameObject player_server_impl = null;

    private void createPlayer(GameEntity e)
    {
        if (player_server_impl != null)
        {
            //if (terrain != null)
            player_server_impl.GetComponent<SrvGameEntity>().entityEnable();
            return;
        }

        KBEngine.Avatar avatar = null;

        if (KBEngineAppThread.app != null)
        {
            if (KBEngineAppThread.app.entity_type != "Avatar")
                return;

            avatar = (KBEngine.Avatar)KBEngineAppThread.app.player();
        }
        else
        {
            if (KBEngineApp.app.entity_type != "Avatar")
            {
                return;
            }

            avatar = (KBEngine.Avatar)KBEngineApp.app.player();
        }
         
        if (avatar == null)
        {
            Debug.Log("wait create(palyer)!");
            return;
        }

        //float y = avatar.position.y;
        //if (avatar.isOnGround)
        //    y = 1.3f;

        //player_server_impl = Instantiate(avatarPerfab, new Vector3(avatar.position.x, y, avatar.position.z),
        //                     Quaternion.Euler(new Vector3(avatar.direction.y, avatar.direction.z, avatar.direction.x))) as UnityEngine.GameObject;

        UnityEngine.GameObject go = new UnityEngine.GameObject();
        go.AddComponent<SrvGameEntity>();
        player_server_impl = go;
        player_server_impl.GetComponent<SrvGameEntity>().entityDisable();
        e.serverImpOfUnit.entity = avatar;
        avatar.renderObj = player_server_impl;

        ((UnityEngine.GameObject)(e.serverImpOfUnit.entity.renderObj)).GetComponent<SrvGameEntity>().gameEngineEntity = e;
        ((UnityEngine.GameObject)avatar.renderObj).GetComponent<SrvGameEntity>().isPlayer = true;

        // 有必要设置一下，由于该接口由Update异步调用，有可能set_position等初始化信息已经先触发了
        // 那么如果不设置renderObj的位置和方向将为0，人物会陷入地下
       
        ((UnityEngine.GameObject)(e.serverImpOfUnit.entity.renderObj)).GetComponent<SrvGameEntity>().destPosition = avatar.position;
        ((UnityEngine.GameObject)(e.serverImpOfUnit.entity.renderObj)).GetComponent<SrvGameEntity>().position = avatar.position;

        //set_position(avatar);
        //set_direction(avatar);
    }
}
