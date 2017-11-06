using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;
using Forge3D;
using Entitas.Utils;

public sealed class TurretSystem : ReactiveSystem<InputEntity>
{
    Contexts _contexts;
    GameContext _gameContext;
    TurretsContext _turretContext;
    IGroup<InputEntity> _moveInputs;
    ObjectPool<GameObject> _bulletsObjectPool;

    public TurretSystem(Contexts contexts):base(contexts.input)
    {
        _contexts = contexts;
        _turretContext = _contexts.turrets;
        _gameContext = _contexts.game;
        _moveInputs = contexts.input.GetGroup(InputMatcher.CannonShoot);
        _bulletsObjectPool = new ObjectPool<GameObject>(() => Assets.Instantiate<GameObject>(Res.Bullet));
    }

    private CannonParams  _cannonParams;

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (GameEntity g in _gameContext.GetEntities())
        {
            if (g.hasPlayerView)
            {
                foreach (PlayerViewController.TurretShip t in g.playerView.controller.GetTurrets())
                {
                    foreach (InputEntity ie in entities)
                    {
                        if (t.turret.TurretId == ie.selectTurret.turretId)
                        {
                            if (ie.selectTurret.Enable)
                            {
                                t.Update(out _cannonParams);

                                if (ie.hasCannonShoot)
                                {
                                    _contexts.bullets.ApplyBullet(_cannonParams.fireAnchor.position, _cannonParams.swivel , Vector3.zero, Vector3.zero, _cannonParams.vX, _cannonParams.vY, 9f, _bulletsObjectPool);
                                }

                                if(ie.selectTurret.Enable)
                                    ie.ReplaceSelectTurret(ie.selectTurret.turretId, true);

                                //ie.ReplaceSelectTurret(ie.selectTurret.turretId, true);
                            }
                        }
                    }
                }
            }
        }
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return new Collector<InputEntity>(
          new[] { context.GetGroup(InputMatcher.CannonShoot), context.GetGroup(InputMatcher.ShootInput), context.GetGroup(InputMatcher.SelectTurret)},
          new[] { GroupEvent.AddedOrRemoved, GroupEvent.AddedOrRemoved, GroupEvent.AddedOrRemoved });
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasSelectTurret;
    }
}
