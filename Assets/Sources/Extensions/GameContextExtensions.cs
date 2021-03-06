﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas.Utils;
using Entitas;

public static class GameEntityExtensions {

    public static GameEntity CreateEnemy(this GameContext context, Vector3 position)
    {
        var entity = context.CreateEntity();
        entity.AddAsset("Ships/Prefabs/katran", "CanvasActions", "Projector/Prefabs/ProjectorAim", "Projector/Prefabs/ProjectorAimCross");
        entity.AddWhoAMi(1);
        entity.AddPosition(position);

        return entity;
    }

    public static GameEntity CreatePlayer(this GameContext context, Vector3 position)
    {
        var entity = context.CreateEntity();
        entity.AddAsset("Ships/Prefabs/katran","Interface/CanvasBattle/CanvasActions","Projector/Prefabs/ProjectorAim", "Projector/Prefabs/ProjectorAimCross");
        
        entity.AddWhoAMi(0);
        entity.AddPlayer("Player1");
        entity.AddForse(Vector3.zero, 0);
        entity.AddServerImpOfUnit(null);
        entity.AddPosition(position);

        return entity;
    }
    public static GameEntity CreateRemotePlayer(this GameContext context, Vector3 position, KBEngine.KbEntity server_entity)
    {
        var entity = context.CreateEntity();
        entity.AddAsset("Ships/Prefabs/katran", "CanvasActions", "Projector/Prefabs/ProjectorAim", "Projector/Prefabs/ProjectorAimCross");
        entity.AddWhoAMi(2);
        entity.AddServerImpOfUnit(server_entity);
        entity.AddPosition(server_entity.position);
        return entity;
    }

    public static BulletsEntity ApplyBullet(this BulletsContext context, Vector3 position, Transform swivel, Vector3 target, Vector3 velocity, float vX, float vY, float gravity, Entitas.Utils.ObjectPool<GameObject> gameObjectPool)
    {
        // gameObjectPool.Get().transform.rotation = Quaternion.LookRotation(target - position);
        BulletsEntity bulletEntity = context.CreateEntity();

        bulletEntity.AddPosition(position);
        bulletEntity.AddVelocity(velocity, vX, vY);
        bulletEntity.AddViewObjectPool(gameObjectPool);
        bulletEntity.AddBullet( 45.0f, gravity, 0f, position, target,0, 0, 0, 0, false);
        bulletEntity.AddBulletElapsedTime(0);
        bulletEntity.AddBulletStartPosition(position, swivel);
        
        //bulletEntity.AddBulletLiveTime(0);

        return bulletEntity;
    }
}
