using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System;

public sealed class ProcessShootInputSystem : ReactiveSystem<InputEntity>
{

    public ProcessShootInputSystem(Contexts contexts) : base(contexts.input) {
        _pools = contexts;

       // TODO Put on a component
       // _bulletsObjectPool = new ObjectPool<GameObject>(() => Assets.Instantiate<GameObject>(Res.Bullet));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.ShootInput);
    }

    protected override bool Filter(InputEntity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }

    Contexts _pools;
    //ObjectPool<GameObject> _bulletsObjectPool;

    protected override void Execute(List<InputEntity> entities)
    {
        var input = entities[entities.Count - 1];
        //var ownerId = input.inputOwner.playerId;

        //var e = _pools.core.GetEntityWithPlayerId(ownerId);
        //if(!e.hasBulletCoolDown) {

        // TODO CoolDown should be configurable
        // e.AddBulletCoolDown(7);

        //var velX = GameRandom.core.RandomFloat(-0.02f, 0.02f);
        //var velY = GameRandom.core.RandomFloat(0.3f, 0.5f);
        //var velocity = new Vector3(velX, velY, 0);
        //_pools.blueprints.blueprints.instance.ApplyBullet(
        //    _pools.bullets.CreateEntity(), e.position.value, velocity, _bulletsObjectPool
        //);
    }
}
