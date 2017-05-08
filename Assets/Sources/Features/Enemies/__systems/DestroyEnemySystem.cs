using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public sealed class DestroyEnemySystem : ISetPools, IEntityCollectorSystem
{
    public EntityCollector entityCollector { get { return _groupObserver; } }
    EntityCollector _groupObserver;

    Pool[] _pools;
    public void SetPools(Pools pools)
    {
        _pools = new[] { pools.core, pools.bullets };
        _groupObserver = _pools.CreateEntityCollector(Matcher.AnyOf(CoreMatcher.DestroyUnit));
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

