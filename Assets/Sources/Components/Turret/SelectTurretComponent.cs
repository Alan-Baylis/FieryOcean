using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

[Input]
public sealed class SelectTurretComponent:IComponent
{
    public float turretId;
    public bool Enable;
}
