using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;
using Forge3D;
using Entitas.Utils;

public sealed class TurretSystem : IExecuteSystem //MultiReactiveSystem<IEntity, Contexts>
{
    private GameContext             _gameContext;
    private ObjectPool<GameObject>  _bulletsObjectPool;

    public TurretSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _bulletsObjectPool = new ObjectPool<GameObject>(() => Assets.Instantiate<GameObject>(Res.Bullet));
    }

    public void Execute()
    {
        foreach (GameEntity g in _gameContext.GetEntities())
        {
            if (g.hasPlayerView)
            {
                foreach (PlayerViewController.TurretShip t in g.playerView.controller.GetTurrets())
                {
                    if (t.trajectoryEnable)
                        t.Update();
                }
            }
        }
    }
}
