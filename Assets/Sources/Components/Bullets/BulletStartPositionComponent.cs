﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[Bullets]
public sealed class BulletStartPositionComponent : IComponent
{
    public Vector3 position;
    public Transform rotation; 
}

