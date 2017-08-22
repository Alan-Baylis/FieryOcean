using UnityEngine;
using System.Collections;
using Entitas;
using System;

public sealed class CreateCameraSystem : IInitializeSystem
{
    Contexts _pools;
    Camera _cam;

    public CreateCameraSystem(Contexts contexts, Camera cam)
    {
        _pools = contexts;
        _cam = cam;
    }

    public void Initialize()
    {
        _pools.game.CreateEntity().AddCamera(_cam,new Vector3(0,0,-10)); 

        //_pools.input.CreateEntity()
        //      .AddMoveInput(0f);
              
    }
}

