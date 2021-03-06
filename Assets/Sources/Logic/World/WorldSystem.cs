using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;
using KBEngine;

public sealed class WorldSystem : IInitializeSystem
{
   
    const string PLAYER_ID = "Player1";
    private float _ocean_y;
    Contexts _pools;

    public WorldSystem(Contexts contexts) //: base(contexts.game)
    {
        _pools = contexts;
    }

    public void Initialize()
    {
        InstallEvents();
    }

    public void InstallEvents()
    {
        installEvents();
    }

    public void SetOcean(float ocean_y)
    {
        _ocean_y = ocean_y;
    }

    void installEvents()
    {
        KBEngine.Event.registerOut("onAvatarEnterWorld", this, "onAvatarEnterWorld");
        KBEngine.Event.registerOut("addSpaceGeometryMapping", this, "addSpaceGeometryMapping");
        // in world

        KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        KBEngine.Event.registerOut("onLeaveWorld", this, "onLeaveWorld");
        KBEngine.Event.registerOut("set_position", this, "set_position");
        KBEngine.Event.registerOut("set_direction", this, "set_direction");
        KBEngine.Event.registerOut("updatePosition", this, "updatePosition");
        KBEngine.Event.registerOut("onControlled", this, "onControlled");

        // in world(register by scripts)
        
        KBEngine.Event.registerOut("set_HP", this, "set_HP");
        KBEngine.Event.registerOut("set_MP", this, "set_MP");
        KBEngine.Event.registerOut("set_HP_Max", this, "set_HP_Max");
        KBEngine.Event.registerOut("set_MP_Max", this, "set_MP_Max");
        KBEngine.Event.registerOut("set_level", this, "set_level");
        KBEngine.Event.registerOut("set_name", this, "set_entityName");
        KBEngine.Event.registerOut("set_state", this, "set_state");
        KBEngine.Event.registerOut("set_moveSpeed", this, "set_moveSpeed");
        KBEngine.Event.registerOut("set_modelScale", this, "set_modelScale");
        KBEngine.Event.registerOut("set_modelID", this, "set_modelID");
        KBEngine.Event.registerOut("recvDamage", this, "recvDamage");
        KBEngine.Event.registerOut("otherAvatarOnJump", this, "otherAvatarOnJump");
        KBEngine.Event.registerOut("onAddSkill", this, "onAddSkill");
    }
    public void addSpaceGeometryMapping(string respath)
    {
        Debug.Log("loading scene(" + respath + ")...");
        //UI.inst.info("scene(" + respath + "), spaceID=" + KBEngineApp.app.spaceID);

        //if (terrain == null)
        //    terrain = Instantiate(terrainPerfab) as UnityEngine.GameObject;

        //if (player)
        //    player.GetComponent<GameEntity>().entityEnable();
    }
    public void onAvatarEnterWorld(UInt64 rndUUID, Int32 eid, KBEngine.Avatar avatar)
    {
        //if (!avatar.isPlayer())
        //{
        //    return;
        //}

        //UI.inst.info("loading scene...(加载场景中...)");
        Debug.Log("loading scene...");
    }


    public void onAddSkill(KBEngine.KbEntity entity)
    {
        Debug.Log("onAddSkill");
    }

    public void onEnterWorld(KBEngine.KbEntity entity)
    {
        Debug.Log("------------ OnEnterWorld -------------");
        Debug.Log(entity.className);
        Debug.Log(entity.id);
        Debug.Log(entity.isPlayer());

        if (entity.isPlayer())
            return;

        entity.position.y = _ocean_y;

        UnityEngine.GameObject go = new UnityEngine.GameObject();
        go.AddComponent<SrvGameEntity>();
        entity.renderObj = go;

        _pools.game.CreateEntity().AddUnitAdd(entity);

        //entity.renderObj = Instantiate(entityPerfab, new Vector3(entity.position.x, y, entity.position.z),
        //    Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x))) as UnityEngine.GameObject;

        ((UnityEngine.GameObject)entity.renderObj).name = entity.className + "_" + entity.id; 
    }

    public void onLeaveWorld(KBEngine.KbEntity entity)
    {
        if (entity.renderObj == null)
            return;

        UnityEngine.GameObject.Destroy(((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().gameEngineEntity.playerView.controller.gameObject);
        ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().gameEngineEntity.AddDestroyUnit(entity.id);
        entity.renderObj = null;

        Debug.Log("onLeaveWorld was called");
    }
    
    /// <summary>
    /// set remote player position
    /// </summary>
    /// <param name="entity"></param>
    public void set_position(KBEngine.KbEntity entity)
    {
        if (entity.renderObj == null)
            return;

        //Entitas.Entity e = (Entitas.Entity)entity.renderObj;
        //e.playerView.controller.rigidbody.position = new Vector3(entity.position.x,e.playerView.controller.rigidbody.position.y, entity.position.z);
        entity.position.y = _ocean_y;
        ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().destPosition = entity.position;
        ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().position = entity.position;

        Debug.Log("set_position was called");
    }

    public void updatePosition(KBEngine.KbEntity entity)
    {
        Debug.Log("---------   updatePosition   ------------");
        Debug.Log(entity.className);
        Debug.Log(entity.id);

        if (entity.renderObj == null)
            return;

        entity.position.y = _ocean_y;
        SrvGameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>();
        gameEntity.destPosition = entity.position;
        gameEntity.isOnGround = entity.isOnGround;

    }

    public void onControlled(KBEngine.KbEntity entity, bool isControlled)
    {
        if (entity.renderObj == null)
            return;

        SrvGameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>();
        gameEntity.isControlled = isControlled;
    }

    public void set_direction(KBEngine.KbEntity entity)
    {
        if (entity.renderObj == null)
            return;

        ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().destDirection =
            new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
    }

    public void set_HP(KBEngine.KbEntity entity, object v)
    {
        if (entity.renderObj != null)
        {
            ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().hp = "" + (Int32)v + "/" + (Int32)entity.getDefinedProperty("HP_Max");
        }
    }

    public void set_MP(KBEngine.KbEntity entity, object v)
    {
    }

    public void set_HP_Max(KBEngine.KbEntity entity, object v)
    {
        if (entity.renderObj != null)
        {
            ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().hp = (Int32)entity.getDefinedProperty("HP") + "/" + (Int32)v;
        }
    }

    public void set_MP_Max(KBEngine.KbEntity entity, object v)
    {
    }

    public void set_level(KBEngine.KbEntity entity, object v)
    {
    }

    public void set_entityName(KBEngine.KbEntity entity, object v)
    {
        if (entity.renderObj != null)
        {
            ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().entity_name = (string)v;
        }
    }

    public void set_state(KBEngine.KbEntity entity, object v)
    {
        if (entity.renderObj != null)
        {
            ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().set_state((SByte)v);
        }

        if (entity.isPlayer())
        {
            Debug.Log("player->set_state: " + v);

            //if (((SByte)v) == 1)
            //    UI.inst.showReliveGUI = true;
            //else
            //    UI.inst.showReliveGUI = false;

            return;
        }
    }

    public void set_moveSpeed(KBEngine.KbEntity entity, object v)
    {
        float fspeed = ((float)(Byte)v) / 10f;

        if (entity.renderObj != null)
        {
            ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().speed = fspeed;
        }
    }

    public void set_modelScale(KBEngine.KbEntity entity, object v)
    {
    }

    public void set_modelID(KBEngine.KbEntity entity, object v)
    {
    }

    public void recvDamage(KBEngine.KbEntity entity, KBEngine.KbEntity attacker, Int32 skillID, Int32 damageType, Int32 damage)
    {
    }

    public void otherAvatarOnJump(KBEngine.KbEntity entity)
    {
        Debug.Log("otherAvatarOnJump: " + entity.id);
        if (entity.renderObj != null)
        {
            ((UnityEngine.GameObject)entity.renderObj).GetComponent<SrvGameEntity>().OnJump();
        }
    }
}

