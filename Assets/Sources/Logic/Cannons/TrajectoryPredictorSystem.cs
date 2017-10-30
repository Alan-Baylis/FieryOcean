using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public sealed class TrajectoryPredictorSystem : IExecuteSystem
{
    Contexts _contexts;
    Transform _crosshairTransform;
    BulletsContext _bulletContext;

    public TrajectoryPredictorSystem(Contexts contexts, Transform crosshairTransform)
    {
        _contexts = contexts;
        _bulletContext = _contexts.bullets;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
