using System.Collections.Generic;
using Entitas;
using UnityEngine;
using KBEngine;

public sealed class PlayerPositionSystem : IExecuteSystem
{
    const string PLAYER_ID = "Player1";
   // public EntityCollector entityCollector { get { return _groupObserver; } }

    //EntityCollector _groupObserver;
    Group inputs;
    Contexts _pools;
    private UltimateJoystick _joystick;
    PlayerMovementController move1;
    public PlayerPositionSystem(UltimateJoystick joystick, Dictionary<PlayerInputController.speedTypes, float> speedMap, Vector3 startPosition)
    {
        _joystick = joystick;
        
        move1 = new PlayerMovementController(speedMap, startPosition.y);
        _beforePosition = startPosition;
    }

    // TODO Entitas 0.36.0 Migration (constructor)
    public void SetPools(Contexts pools) {
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
        createPlayer(player);

        if (inputs.count > 0)
            lastAc = inputs.GetEntities()[0].moveInput.accelerate;

        _nextPosition = move1.Move( player.playerView.controller.shipDirectional.GetShipDirectional(),
                                    player.playerView.controller.rigidbody,
                                    _joystick.GetPosition(),
                                    lastAc                                                              );

        if(Vector3.Distance(_beforePosition,_nextPosition)>2f)
            player.ReplacePlayerPosition(player.playerView.controller.transform.position);
    }

    private UnityEngine.GameObject player_server_impl = null;

    private void createPlayer(Entitas.Entity e)
    {
        if (player_server_impl != null)
        {
            //if (terrain != null)
            player_server_impl.GetComponent<GameEntity>().entityEnable();
            return;
        }

        if (KBEngineApp.app.entity_type != "Avatar")
        {
            return;
        }

        KBEngine.Avatar avatar = (KBEngine.Avatar)KBEngineApp.app.player();
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
        go.AddComponent<GameEntity>();
        player_server_impl = go;
        player_server_impl.GetComponent<GameEntity>().entityDisable();
        e.serverImpOfUnit.entity = avatar;
        avatar.renderObj = player_server_impl;

        ((UnityEngine.GameObject)(e.serverImpOfUnit.entity.renderObj)).GetComponent<GameEntity>().gameEngineEntity = e;
        ((UnityEngine.GameObject)avatar.renderObj).GetComponent<GameEntity>().isPlayer = true;

        // 有必要设置一下，由于该接口由Update异步调用，有可能set_position等初始化信息已经先触发了
        // 那么如果不设置renderObj的位置和方向将为0，人物会陷入地下
       
        ((UnityEngine.GameObject)(e.serverImpOfUnit.entity.renderObj)).GetComponent<GameEntity>().destPosition = avatar.position;
        ((UnityEngine.GameObject)(e.serverImpOfUnit.entity.renderObj)).GetComponent<GameEntity>().position = avatar.position;

        //set_position(avatar);
        //set_direction(avatar);
    }
}
