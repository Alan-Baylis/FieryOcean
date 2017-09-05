using System;
using System.Collections.Generic;
using Entitas;

public sealed class ProcessCollisionSystem : ReactiveSystem<InputEntity>, ICleanupSystem
{ 
    public ProcessCollisionSystem(Contexts contexts) : base(contexts.input) {
        _poolInput = contexts.input;
        _collisions = contexts.input.GetGroup(InputMatcher.Collision);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.Collision.Added());
    }

    protected override bool Filter(InputEntity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 
        
        return true;
    }

    InputContext _poolInput;
    IGroup<InputEntity> _collisions;

    protected override void Execute(List<InputEntity> entities) {
       foreach(var e in entities) {

             if (e.collision.self is BulletsEntity)
             {
                 BulletsEntity bge = (BulletsEntity)e.collision.self;

                 bge.ReplaceHealth(/*ge.health.value - 1*/ 0);
                 //GameEntity ge = ((GameEntity)e.collision.other);
                 //var newHealth = ge.health.value - ((BulletsEntity)e.collision.self).damage.value;
                 //ge.ReplaceHealth(Math.Max(0, newHealth));
             }
         }
    }

    public void Cleanup() {
        _collisions = _poolInput.GetGroup(InputMatcher.Collision);
        foreach (var e in _collisions.GetEntities()) {
            e.Destroy();
        }
    }
}
