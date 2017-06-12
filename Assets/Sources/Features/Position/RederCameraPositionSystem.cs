using UnityEngine;
using System;
using System.Collections.Generic;
using Entitas;

public sealed class RederCameraPositionSystem : ISetPools, IEntityCollectorSystem
{
    public Collector entityCollector { get { return _groupObserver; } }

    Collector _groupObserver;

    public void SetPools(Contexts pools)
    {
        _groupObserver = new[] { pools.core, pools.bullets }
            .CreateEntityCollector(Matcher.AllOf(CoreMatcher.CameraPosition, CoreMatcher.Position));
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
           // e.cameraPosition.position = e.

        }
    }

}
