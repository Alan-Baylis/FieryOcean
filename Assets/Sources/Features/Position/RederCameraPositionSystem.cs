using UnityEngine;
using System;
using System.Collections.Generic;
using Entitas;

public sealed class RederCameraPositionSystem : ReactiveSystem //IEntityCollectorSystem
{
    public RederCameraPositionSystem(Contexts contexts) : base(contexts.core)
    {
    }
    public Collector entityCollector { get { return _groupObserver; } }

    Collector _groupObserver;

    // TODO Entitas 0.36.0 Migration (constructor)
    //public void SetPools(Contexts pools)
    //{
    //    _groupObserver = new[] { pools.core, pools.bullets }
    //        .CreateEntityCollector(Matcher.AllOf(CoreMatcher.CameraPosition, CoreMatcher.Position));
    //}

    protected override void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
           // e.cameraPosition.position = e.

        }
    }

    protected override Collector GetTrigger(Context context)
    {
        return context.CreateCollector(Matcher.AllOf(CoreMatcher.CameraPosition, CoreMatcher.Position));
    }

    protected override bool Filter(Entity entity)
    {
        throw new NotImplementedException();
    }
}
