using UnityEngine;
using System.Collections;
using Entitas;
using System;

public sealed class CreateCameraSystem : ISetPools, IInitializeSystem
{

    Contexts _pools;
    Camera _cam;

    public void SetPools(Contexts pools)
    {
        _pools = pools;
    }

    public CreateCameraSystem(Camera cam)
    {
        _cam = cam;
    }

    public void Initialize()
    {
        _pools.core.CreateEntity().AddCamera(_cam).AddCameraPosition(new Vector3(0,0,-10));

        //_pools.input.CreateEntity()
        //      .AddMoveInput(0f);
              
    }
}

