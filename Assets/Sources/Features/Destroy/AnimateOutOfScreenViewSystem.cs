using System;
using System.Collections.Generic;
using Entitas;

public sealed class AnimateOutOfScreenViewSystem : ReactiveSystem //IEntityCollectorSystem {
{
    public AnimateOutOfScreenViewSystem(Contexts contexts) : base(contexts.core)
    {

    }

    protected override void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var controller = e.view.controller;
            controller.Hide(false);
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

//public Collector entityCollector { get { return _groupObserver; } }

//Collector _groupObserver;

//// TODO Entitas 0.36.0 Migration (constructor)
//public void SetPools(Contexts pools) {
//    _groupObserver = new [] { pools.core, pools.bullets }
//        .CreateEntityCollector(Matcher.AllOf(CoreMatcher.View, CoreMatcher.OutOfScreen));
//}


