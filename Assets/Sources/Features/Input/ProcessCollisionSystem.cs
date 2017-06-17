using System;
using System.Collections.Generic;
using Entitas;

public sealed class ProcessCollisionSystem : ReactiveSystem { //ReactiveSystemICleanupSystem {

    public ProcessCollisionSystem(Contexts contexts) : base(contexts.core) {

    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(InputMatcher.Collision);
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }

    Context _pool;
    Group _collisions;

    protected override void Execute(List<Entity> entities) {
       foreach(var e in entities) {
            e.collision.self.ReplaceHealth(e.collision.self.health.value - 1);
            var newHealth = e.collision.other.health.value - e.collision.self.damage.value;
            e.collision.other.ReplaceHealth(Math.Max(0, newHealth));
        }
    }

    public void Cleanup() {
        foreach(var e in _collisions.GetEntities()) {
            _pool.DestroyEntity(e);
        }
    }
}
