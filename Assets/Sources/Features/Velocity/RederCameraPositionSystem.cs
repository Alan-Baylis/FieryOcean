using UnityEngine;
using System;
using System.Collections.Generic;
using Entitas;

public sealed class RederCameraPositionSystem : ISetPools, IEntityCollectorSystem
{
    public EntityCollector entityCollector { get { return _groupObserver; } }

    EntityCollector _groupObserver;

    public void SetPools(Pools pools)
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
