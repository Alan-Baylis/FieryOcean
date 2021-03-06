using System.Collections.Generic;
using Entitas;

public sealed class CheckHealthSystem : ReactiveSystem<BulletsEntity>
{
    Contexts _pool;
    public CheckHealthSystem(Contexts contexts) : base(contexts.bullets) {
        _pool = contexts;
    }

    protected override ICollector<BulletsEntity> GetTrigger(IContext<BulletsEntity> context)
    {
        return context.CreateCollector(BulletsMatcher.Health.Added());
    }

    protected override bool Filter(BulletsEntity entity)
    {
        return true;
    }

    protected override void Execute(List<BulletsEntity> entities) {
        foreach(var e in entities) {
            e.flagDestroy = true;
        }
    }
}
