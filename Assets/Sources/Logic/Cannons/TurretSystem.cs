using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;
using Forge3D;

public sealed class TurretSystem : IExecuteSystem
{
    Contexts _contexts;
    GameContext _gameContext;
    TurretsContext _turretContext;

    public TurretSystem(Contexts contexts)
    {
        _contexts = contexts;
        _turretContext = _contexts.turrets;
        _gameContext = _contexts.game;
    }

    private CannonParams _cannonParams;

    public void Execute()
    {
        foreach(GameEntity g in _gameContext.GetEntities())
        {
            if(g.hasPlayerView)
            {
                foreach(PlayerViewController.TurretShip t in g.playerView.controller.GetTurrets())
                {
                    if(t.turret.IsAiming())
                    {
                        t.Update(out _cannonParams);


                    }
                }
            }
        }
    }
}
