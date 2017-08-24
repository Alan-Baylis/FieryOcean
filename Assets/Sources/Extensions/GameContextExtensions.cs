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
}
