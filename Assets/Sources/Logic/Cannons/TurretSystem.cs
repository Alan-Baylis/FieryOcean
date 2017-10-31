using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public sealed class TurretSystem : IExecuteSystem
{
    Contexts _contexts;
    TurretsContext _turretContext;
    public TurretSystem(Contexts contexts)
    {
        _contexts = contexts;
        _turretContext = _contexts.turrets;
    }

    public void Execute()
    {
        foreach(TurretsEntity t in _turretContext.GetEntities())
        {
            if(t.turret.isActive)
            {



            }
        }
    }
}
