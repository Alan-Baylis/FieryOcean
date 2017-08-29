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
        return context.CreateCollector(BulletsMatcher.Health.AddedOrRemoved());
    }

    protected override bool Filter(BulletsEntity entity)
    {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }


    protected override void Execute(List<BulletsEntity> entities) {
        foreach(var e in entities) {
            //if(e.health.value <= 0) {
                e.flagDestroy = true;
            //}
        }
    }
}
