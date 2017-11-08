using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public sealed class BulletsThrowSystem : ReactiveSystem<BulletsEntity>
{
    Contexts _contexts;
    public BulletsThrowSystem(Contexts context): base(context.bullets)
    {
        _contexts = context;
    }
    protected override ICollector<BulletsEntity> GetTrigger(IContext<BulletsEntity> context)
    {
        return new Collector<BulletsEntity>(
          new[] { context.GetGroup(BulletsMatcher.Bullet), context.GetGroup(BulletsMatcher.BulletElapsedTime), context.GetGroup(BulletsMatcher.View)},
          new[] { GroupEvent.Added, GroupEvent.Added, GroupEvent.Added });
    }

    protected override void Execute(List<BulletsEntity> entities)
    {
        foreach(BulletsEntity b in entities)
        {
            Vector3 deltaPos = new Vector3(0, (-b.velocity.vY - (b.bullet.gravity * b.bulletElapsedTime.elapsedTime)) * Time.deltaTime, b.velocity.vX * Time.deltaTime);
            b.view.controller.gameObject.transform.Translate(deltaPos);

            if (b.view.controller.gameObject.transform.position.y > -1)
            {
                b.ReplaceBulletElapsedTime(b.bulletElapsedTime.elapsedTime + Time.deltaTime);
            }
            else
            {
                b.view.controller.Hide(true);
                b.Destroy();
            }
        }
    }

    protected override bool Filter(BulletsEntity entity)
    {
        return (entity.hasView && entity.hasBullet);
    }
}

