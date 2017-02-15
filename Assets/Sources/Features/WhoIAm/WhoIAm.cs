using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[Core]
public sealed class WhoIAm : IComponent
{
    public enum IAm {PLAYER,ENEMY }

    public IAm iAm;
}

