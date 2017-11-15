using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

[Game]
public sealed class ShipHealthComponent : IComponent
{
    public int forward;
    public int middle;
    public int backward;
}
