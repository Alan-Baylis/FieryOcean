using System;
using System.Collections.Generic;
using Entitas;

public sealed class ProcessCollisionSystem : ReactiveSystem<InputEntity>, ICleanupSystem
{ 
    public ProcessCollisionSystem(Contexts contexts) : base(contexts.input) {
        _poolInput = contexts.input;
        //_collisions = contexts.input.GetGroup(InputMatcher.Collision);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.Collision.Added());
    }

    protected override bool Filter(InputEntity entity) {
        return true;
    }

    InputContext _poolInput;
    //IGroup<InputEntity> _collisions;

    protected override void Execute(List<InputEntity> entities) {
        foreach(var e in entities) {
            if (e.collision.self is BulletsEntity)
            {
                //we need to destroy bullet
                ((BulletsEntity)e.collision.self).ReplaceHealth(0);

                //Change health of ship
                if(e.collision.other is GameEntity)
                {
                    if(e.collision.other.HasComponents(GameMatcher.WhoAMi.indices))
                    {
                       ShipSectors.ShipSector sector = e.collision.shipSector;
                    }
                } 

                //GameEntity ge = ((GameEntity)e.collision.other);
                //var newHealth = ge.health.value - ((BulletsEntity)e.collision.self).damage.value;
                //ge.ReplaceHealth(Math.Max(0, newHealth));
             }
         }
    }

    public void Cleanup() {
        //_collisions = _poolInput.GetGroup(InputMatcher.Collision);
        //foreach (var e in _collisions.GetEntities()) {
        //    e.Destroy();
        //}
    }
}
