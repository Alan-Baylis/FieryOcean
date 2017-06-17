using System;
using System.Collections.Generic;
using Entitas;

public sealed class DestroyEntitySystem : ReactiveSystem {//IEntityCollectorSystem {

    public DestroyEntitySystem(Contexts contexts) : base(contexts.core)
    { }
    
    // public Collector entityCollector { get { return _groupObserver; } }

    Context[] _pools;
    //Collector _groupObserver;

    // TODO Entitas 0.36.0 Migration (constructor)
    //public void SetPools(Contexts pools) {
    //    _pools = new [] { pools.core, pools.bullets };
    //    _groupObserver = _pools.CreateEntityCollector(Matcher.AnyOf(CoreMatcher.Destroy, CoreMatcher.OutOfScreen));
    //}

    protected override void Execute(List<Entity> entities) {
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

    protected override bool Filter(Entity entity)
    {
        throw new NotImplementedException();
    }

    protected override Collector GetTrigger(Context context)
    {
        throw new NotImplementedException();
    }
}
