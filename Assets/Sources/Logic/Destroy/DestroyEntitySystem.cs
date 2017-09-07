using System;
using System.Collections.Generic;
using Entitas;

public sealed class DestroyEntitySystem : ReactiveSystem<BulletsEntity> {

    Contexts context;

    public DestroyEntitySystem(Contexts contexts) : base(contexts.bullets)
    { }

    protected override void Execute(List<BulletsEntity> entities) {
        foreach(var e in entities) {
            e.view.controller.Hide(true);
            e.Destroy();
           
        }
    }

    protected override bool Filter(BulletsEntity entity)
    {
        return true;
    }

    protected override ICollector<BulletsEntity> GetTrigger(IContext<BulletsEntity> context)
    {
        return new Collector<BulletsEntity>(
           new[] {
                //context.GetGroup(BulletsMatcher.OutOfScreen),
                context.GetGroup(BulletsMatcher.Destroy)
                //context.GetGroup(BulletsMatcher.DestroyUnit)
           },
           new[] {
                //GroupEvent.Added,
                GroupEvent.Added
                // GroupEvent.Added
           });
    }
}
