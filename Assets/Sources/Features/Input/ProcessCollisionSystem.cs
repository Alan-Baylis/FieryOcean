using System;
using System.Collections.Generic;
using Entitas;

public sealed class ProcessCollisionSystem : ReactiveSystem<InputEntity> { //ReactiveSystemICleanupSystem {

    public ProcessCollisionSystem(Contexts contexts) : base(contexts.input) {
        _collisions = contexts.input.GetGroup(InputMatcher.Collision);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.Collision);
    }

    protected override bool Filter(InputEntity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 
        
        return true;
    }

    IContext<InputEntity> _pool;
    IGroup<InputEntity> _collisions;

    protected override void Execute(List<InputEntity> entities) {
       //foreach(var e in entities) {

       //     if (e.collision.self is GameEntity)
       //     {
       //         GameEntity ge = (GameEntity)e.collision.self;

       //         ge.ReplaceHealth(ge.health.value - 1);
       //         var newHealth = e.collision.other.health.value - e.collision.self.damage.value;
       //         e.collision.other.ReplaceHealth(Math.Max(0, newHealth));
       //     }
       // }
    }

    public void Cleanup() {
        foreach(var e in _collisions.GetEntities()) {
            e.Destroy();
        }
    }
}
