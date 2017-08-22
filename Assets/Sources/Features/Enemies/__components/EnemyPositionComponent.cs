using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[Game]
public sealed class EnemyPositionComponent : IComponent
{
    public Vector3 position;
}
