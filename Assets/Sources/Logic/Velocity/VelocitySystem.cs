using System;
using Entitas;
using UnityEngine;

public sealed class VelocitySystem : IExecuteSystem
{
    Contexts _contexts;
    readonly IGroup<BulletsEntity> _bullets;
    private Contexts contexts;

    public VelocitySystem(Contexts contexts) 
    {
        _contexts = contexts;
        _bullets = contexts.bullets.GetGroup(BulletsMatcher.Velocity);
    }

    public void Execute()
    {
        foreach(var e in _bullets.GetEntities()) 
        {
            var pos = e.position.value;
            e.ReplacePosition(pos + e.velocity.value);
        }
    }
}
