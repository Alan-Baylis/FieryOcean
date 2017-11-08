using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System;
using Entitas.Utils;
public sealed class ShootSystem : ReactiveSystem<InputEntity>
{
    private Contexts _contexts;
    private ObjectPool<GameObject> _bulletsObjectPool;
    private GameContext _gameContext;

    public ShootSystem(Contexts contexts) : base(contexts.input) {
        _contexts = contexts;
        _gameContext = contexts.game;
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
        foreach (var e in entities)
        {
            foreach (GameEntity g in _gameContext.GetEntities())
            {
                if (g.hasPlayerView)
                {
                    foreach (PlayerViewController.TurretShip t in g.playerView.controller.GetTurrets())
                    {
                        if (t.ID == e.cannonShoot.cannonParams.ID)
                        {
                            if (t.turret.IsAiming)
                            {
                               
                                _contexts.bullets.ApplyBullet(t.turret.cannonParams.fireAnchor.position, t.turret.cannonParams.swivel, Vector3.zero, Vector3.zero, t.turret.cannonParams.vX, t.turret.cannonParams.vY, 9f, _bulletsObjectPool);
                            }
                        }
                    }
                }
            }
        }
    }
}
