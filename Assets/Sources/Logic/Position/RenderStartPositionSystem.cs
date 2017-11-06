using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public sealed class RenderStartPositionSystem : ReactiveSystem<BulletsEntity>
{
    public RenderStartPositionSystem(Contexts contexts):base(contexts.bullets)
    {    }

    protected override void Execute(List<BulletsEntity> entities)
    {
        foreach (var e in entities)
        {
            e.view.controller.gameObject.transform.rotation = Quaternion.LookRotation(e.bulletStartPosition.rotation.forward);
            e.view.controller.position = e.bulletStartPosition.position;
        }
    }

    protected override bool Filter(BulletsEntity entity)
    {
        return entity.hasView;
    }

    protected override ICollector<BulletsEntity> GetTrigger(IContext<BulletsEntity> context)
    {
        return new Collector<BulletsEntity>(
          new[] { context.GetGroup(BulletsMatcher.BulletStartPosition)},
          new[] { GroupEvent.Added });
    }
}

