using System.Collections.Generic;
using Entitas;
using UnityEngine;
using KBEngine;
using System;

public sealed class RenderPositionSystem : ReactiveSystem<BulletsEntity>//, ICleanupSystem
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
                 context.GetGroup(BulletsMatcher.View),
                 context.GetGroup(BulletsMatcher.DeltaPosition)
           },
           new[] {
                 GroupEvent.Added,
                 GroupEvent.Added,
           });
    }

    protected override void Execute(List<BulletsEntity> entities)
    {
        //foreach (var e in entities)
        //{
        //    e.view.controller.gameObject.transform.Translate(e.deltaPosition.position);
        //    //e.ReplacePosition()
        //}
    }

    protected override bool Filter(BulletsEntity entity)
    {
        return entity.hasView;
    }

    //public void Cleanup()
    //{
    //    Debug.Log("work cleanup");
    //}
}
