using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

public sealed class AddPlayerStartPosition : ReactiveSystem
{
    const string PLAYER_ID = "Player1";
    public AddPlayerStartPosition(Contexts contexts) : base(contexts.core) {
        _pool = contexts.core;
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.PlayerView);
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }

    Context _pool;

    

    protected override void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            if (e.whoAMi.value == 0)
            {
                var player = _pool.GetEntityWithPlayerId(PLAYER_ID);
                //player.playerView.controller.transform.position = e.playerPosition.position;
                player.playerView.controller.transform.position = e.position.value;
            }

            // set start position for remote player
            if(e.whoAMi.value == 2)
            {
                e.playerView.controller.transform.position = e.serverImpOfUnit.entity.position;
            }
        }
    }
}