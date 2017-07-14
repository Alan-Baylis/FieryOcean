using UnityEngine;
using System;
using System.Collections.Generic;
using Entitas;

public sealed class RederCameraPositionSystem : ReactiveSystem<GameEntity> //IEntityCollectorSystem
{
    public RederCameraPositionSystem(Contexts contexts) : base(contexts.game)
    {
    }
    public ICollector<GameEntity> entityCollector { get { return _groupObserver; } }

    Collector<GameEntity> _groupObserver;

    // TODO Entitas 0.36.0 Migration (constructor)
    //public void SetPools(Contexts pools)
    //{
    //    _groupObserver = new[] { pools.core, pools.bullets }
    //        .CreateEntityCollector(Matcher.AllOf(CoreMatcher.CameraPosition, CoreMatcher.Position));
    //}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
           // e.cameraPosition.position = e.

        }
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Camera);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }
}
