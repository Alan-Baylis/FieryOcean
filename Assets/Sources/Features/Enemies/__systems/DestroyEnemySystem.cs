using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public sealed class DestroyEnemySystem : ReactiveSystem //IEntityCollectorSystem
{
    public DestroyEnemySystem(Contexts contexts) : base(contexts.core)
    {
        _pools = new Context[] { contexts.core };
    }

    //public Collector entityCollector { get { return _groupObserver; } }
    //Collector _groupObserver;

    Context[] _pools;
    // TODO Entitas 0.36.0 Migration (constructor)
    //public void SetPools(Contexts pools)
    //{
    //    _pools = new[] { pools.core };
    //    _groupObserver = _pools.CreateEntityCollector(Matcher.AnyOf(CoreMatcher.DestroyUnit));
    //}

    protected override void Execute(List<Entity> entities)
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

    protected override bool Filter(Entity entity)
    {
        return true; //throw new NotImplementedException();
    }

    protected override Collector GetTrigger(Context context)
    {
       return context.CreateCollector(CoreMatcher.DestroyUnit);
        //throw new NotImplementedException();
    }
}

