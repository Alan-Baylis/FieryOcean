using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public sealed class DestroyEnemySystem : ISetPools, IEntityCollectorSystem
{
    public Collector entityCollector { get { return _groupObserver; } }
    Collector _groupObserver;

    Context[] _pools;
    public void SetPools(Contexts pools)
    {
        _pools = new[] { pools.core };
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

