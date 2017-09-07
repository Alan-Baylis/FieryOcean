using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas.Utils;
using Entitas;

public static class GameEntityExtensions {

    public static GameEntity CreateEnemy(this GameContext context, Vector3 position)
    {
        var entity = context.CreateEntity();
        entity.AddAsset("Ships/Prefabs/katran");
        entity.AddWhoAMi(1);
        entity.AddPosition(position);

        return entity;
    }

    public static GameEntity CreatePlayer(this GameContext context, Vector3 position)
    {
        var entity = context.CreateEntity();
        entity.AddAsset("Ships/Prefabs/katran");
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
        entity.AddAsset("Ships/Prefabs/katran");
        entity.AddWhoAMi(2);
        entity.AddServerImpOfUnit(server_entity);
        entity.AddPosition(server_entity.position);
        return entity;
    }

    public static BulletsEntity ApplyBullet(this BulletsContext context, Vector3 position, Vector3 target, Vector3 velocity, Entitas.Utils.ObjectPool<GameObject> gameObjectPool)
    {
        float firingAngle = 45.0f;
        float gravity = 9.8f;

        // Calculate distance to target
        float target_Distance = Vector3.Distance(position, target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

       // gameObjectPool.Get().transform.rotation = Quaternion.LookRotation(target - position);
        BulletsEntity bulletEntity = context.CreateEntity();

        bulletEntity.AddPosition(position);
        bulletEntity.AddVelocity(velocity);
        bulletEntity.AddViewObjectPool(gameObjectPool);
        bulletEntity.AddBullet( 45.0f, 9.8f, position, target, flightDuration, Vx, Vy);
        bulletEntity.AddBulletLiveTime(0);

        return bulletEntity;
    }

    public static GameEntity ApplyBullet(this GameContext context, Vector3 position, Vector3 velocity, Entitas.Utils.ObjectPool<GameObject> gameObjectPool)
    {
        GameEntity bulletEntity = context.CreateEntity();

        bulletEntity.AddPosition(position);
        bulletEntity.AddVelocity(velocity);
        bulletEntity.AddViewObjectPool(gameObjectPool);

        return bulletEntity;
    }
}
