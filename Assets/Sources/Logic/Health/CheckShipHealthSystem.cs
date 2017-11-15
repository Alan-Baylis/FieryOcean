using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class CheckShipHealthSystem : ReactiveSystem<GameEntity>
{
    public CheckShipHealthSystem(Contexts contexts):base(contexts.game)
    {

    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(var e in entities)
        {
           // e
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Health.Added());
    }
}
