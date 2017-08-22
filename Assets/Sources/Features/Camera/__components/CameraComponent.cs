using UnityEngine;
using System.Collections;
using Entitas;

[Game]
public sealed class CameraComponent : IComponent
{
    public Camera cam;
    public Vector3 position;
}
