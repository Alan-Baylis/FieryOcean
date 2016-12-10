using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.CodeGenerator;

[Core,Bullets]
public class ForseComponent : IComponent
{
    public Vector3 force;
    public float accelerate;

}
