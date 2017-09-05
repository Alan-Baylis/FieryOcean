using Entitas;
using Entitas.Utils;

public static class EntityIndexPoolsExtensions {

    public const string PlayerKey = "Player";

    public static void AddEntityIndices(this Contexts pools)
    {
        var playerIndex = new Entitas.PrimaryEntityIndex<GameEntity,string>(PlayerKey,
            pools.game.GetGroup(GameMatcher.Player),
            (entity, component) =>
            {
                var playerComponent = (PlayerComponent)component;
                return playerComponent != null
                    ? playerComponent.id
                    : entity.player.id;
            }
        );

        pools.game.AddEntityIndex(playerIndex);
    }

    public static GameEntity GetEntityWithPlayerId(this GameContext pool, string id)
    {
        var index = (PrimaryEntityIndex<GameEntity,string>)pool.GetEntityIndex(PlayerKey);
        return index.GetEntity(id);
    }
}
