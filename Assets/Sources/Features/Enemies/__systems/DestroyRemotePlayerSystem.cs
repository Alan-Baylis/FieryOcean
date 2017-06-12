using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public partial class DestroyRemotePlayerSystem : ISetPools, IMultiReactiveSystem
{
    public TriggerOnEvent[] triggers { get { return new TriggerOnEvent[] { CoreMatcher.DestroyUnit.OnEntityAdded() }; } }

    Context[] _pools;

    public void SetPools(Contexts pools)
    {
        _pools = new[] { pools.core };
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            foreach (var pool in _pools)
            {
                if (pool.HasEntity(e))
                {
                    pool.DestroyEntity(e);
                    break;
                }
            }
        }
    }
}



