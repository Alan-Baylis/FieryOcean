using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System;
using Entitas.Utils;
public sealed class ProcessShootInputSystem : ReactiveSystem<InputEntity>
{
    Contexts _pools;
    ObjectPool<GameObject> _bulletsObjectPool;

    public ProcessShootInputSystem(Contexts contexts) : base(contexts.input) {
        _pools = contexts;

       // TODO Put on a component
        _bulletsObjectPool = new ObjectPool<GameObject>(() => Assets.Instantiate<GameObject>(Res.Bullet));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return new Collector<InputEntity>(
          new[] { context.GetGroup(InputMatcher.CannonShoot),  context.GetGroup(InputMatcher.ShootInput) },
          new[] { GroupEvent.Added, GroupEvent.Added });
    }

    protected override bool Filter(InputEntity entity) {
        return entity.hasCannonShoot;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        //var input = entities[entities.Count - 1];

        foreach (InputEntity e in entities)
        {
            var player = _pools.game.GetEntityWithPlayerId(e.inputOwner.playerId);
           
            _pools.bullets.ApplyBullet( player.position.value + e.cannonShoot.cannonParams.shipPosition,
                                       /* e.cannonShoot.cannonParams.target*/ new Vector3(30,0,140),                ///TODO
                                        player.playerView.controller.rigidbody.velocity,
                                        _bulletsObjectPool                                              ); 
        }
    }
}
