using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

public sealed class AddPlayerStartPosition : ISetPool, IReactiveSystem
{
    const string PLAYER_ID = "Player1";
    public TriggerOnEvent trigger { get { return CoreMatcher.PlayerView.OnEntityAdded(); } }

    Pool _pool;

    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var player = _pool.GetEntityWithPlayerId(PLAYER_ID);
            //player.playerView.controller.transform.position = e.playerPosition.position;
            player.playerView.controller.transform.position = e.position.value;
        }
    }
}