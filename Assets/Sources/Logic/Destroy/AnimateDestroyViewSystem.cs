using System;
using System.Collections.Generic;
using Entitas;

public sealed class AnimateDestroyViewSystem : ReactiveSystem<BulletsEntity> { 
    readonly GameContext _context;

    public AnimateDestroyViewSystem(Contexts contexts) :base(contexts.bullets)
    {
        _context = contexts.game;
    }

    protected override void Execute(List<BulletsEntity> entities) {
        foreach(var e in entities) {
            var controller = e.view.controller;
            controller.Hide(true);
        }
    }

    protected override bool Filter(BulletsEntity entity)
    {
        return entity.hasView;
    }

    protected override ICollector<BulletsEntity> GetTrigger(IContext<BulletsEntity> context)
    {
        return new Collector<BulletsEntity>(
            new[] {
                //context.GetGroup(BulletsMatcher.View),
                context.GetGroup(BulletsMatcher.Destroy)
            }, 
            new[] {
                //GroupEvent.AddedOrRemoved//,
                GroupEvent.Added
            });
    }
}
