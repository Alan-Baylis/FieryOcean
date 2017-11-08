using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

[Input]
public sealed class SelectTurretComponent:IComponent
{
    public UInt32 turretId;
    public bool Enable;
}
