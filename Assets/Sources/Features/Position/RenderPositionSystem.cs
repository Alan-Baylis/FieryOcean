using System.Collections.Generic;
using Entitas;
using UnityEngine;
using KBEngine;
using System;

public sealed class RenderPositionSystem : ReactiveSystem<BulletsEntity>
{
    Contexts _pools;

    public RenderPositionSystem(Contexts contexts) : base(contexts.bullets)
    {
        _pools = contexts;
    }

    protected override ICollector<BulletsEntity> GetTrigger(IContext<BulletsEntity> context)
    {
        return new Collector<BulletsEntity>(
           new[] {
                 context.GetGroup(BulletsMatcher.Position),
                 context.GetGroup(BulletsMatcher.View)
           },
           new[] {
                 GroupEvent.Added,
                 GroupEvent.Added
           });
    }

    protected override void Execute(List<BulletsEntity> entities)
    {
        foreach (var e in entities)
        {
            e.view.controller.position = e.position.value;
        }
    }

    protected override bool Filter(BulletsEntity entity)
    {
        return entity.hasView;
    }
}
