using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

//[Turrets]
public sealed class TurretComponent:IComponent
{
    public float speed;
    public Vector3 swivelRotation;
    public Vector3 barrelRotation;
}

