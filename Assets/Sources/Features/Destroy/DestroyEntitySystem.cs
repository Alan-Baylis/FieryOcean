using System.Collections.Generic;
using Entitas;

public sealed class DestroyEntitySystem : ISetPools, IEntityCollectorSystem {

    public Collector entityCollector { get { return _groupObserver; } }

    Context[] _pools;
    Collector _groupObserver;

    public void SetPools(Contexts pools) {
        _pools = new [] { pools.core, pools.bullets };
        _groupObserver = _pools.CreateEntityCollector(Matcher.AnyOf(CoreMatcher.Destroy, CoreMatcher.OutOfScreen));
    }

    public void Execute(List<Entity> entities) {
        foreach(var e in entities) {
            foreach(var pool in _pools) {
                if(pool.HasEntity(e))
                {
                    pool.DestroyEntity(e);
                    
                    break;
                }
            }
        }
    }
}
