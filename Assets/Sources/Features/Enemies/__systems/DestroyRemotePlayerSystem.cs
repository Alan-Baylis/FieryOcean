using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public partial class DestroyRemotePlayerSystem : ReactiveSystem //IMultiReactiveSystem
{
    public DestroyRemotePlayerSystem(Contexts contexts) : base(contexts.core) {
            _pools = new[] { contexts.core };
    }

    //public TriggerOnEvent[] triggers { get { return new TriggerOnEvent[] { CoreMatcher.DestroyUnit.OnEntityAdded() }; } }

    Context[] _pools;

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

        return true;
    }

    protected override Collector GetTrigger(Context context)
    {
        return context.CreateCollector(CoreMatcher.DestroyUnit,GroupEvent.Added);
        //throw new NotImplementedException();
    }
}



