using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game,Bullets]
public class ForseComponent : IComponent
{
    public Vector3 force;
    public float accelerate;

}
