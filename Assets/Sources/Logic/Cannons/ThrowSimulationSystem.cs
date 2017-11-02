using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public sealed class ThrowSimulationSystem : ReactiveSystem<BulletsEntity>
{
    public ThrowSimulationSystem(Contexts contexts) : base(contexts.bullets)
    {

    }

    protected override ICollector<BulletsEntity> GetTrigger(IContext<BulletsEntity> context)
    {
        return new Collector<BulletsEntity>(new[] { context.GetGroup(BulletsMatcher.Position) }, new[] {GroupEvent.Added });
    }

    protected override void Execute(List<BulletsEntity> entities)
    {
        throw new NotImplementedException();
    }

    protected override bool Filter(BulletsEntity entity)
    {
        throw new NotImplementedException();
    }
}

