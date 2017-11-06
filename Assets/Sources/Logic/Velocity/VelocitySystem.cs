using System;
using Entitas;
using UnityEngine;

public sealed class VelocitySystem : IExecuteSystem
{
    Contexts _contexts;
    readonly IGroup<BulletsEntity> _bullets;

    public VelocitySystem(Contexts contexts) 
    {
        _contexts = contexts;
        _bullets = contexts.bullets.GetGroup(BulletsMatcher.Velocity);
    }

    public void Execute()
    {
        //foreach(var e in _bullets.GetEntities()) 
        //{
        //    if (e.hasView)
        //    {
        //        if (e.bulletLiveTime.elapse_time < e.bullet.flightDuration)
        //        {
        //            e.view.controller.gameObject.transform.Translate(0, (e.bullet.Vy - (e.bullet.gravity * e.bulletLiveTime.elapse_time)) * Time.deltaTime, e.bullet.Vx * Time.deltaTime);
        //            e.ReplaceBulletLiveTime(e.bulletLiveTime.elapse_time + Time.deltaTime);
        //        }
        //    }
        //}
    }
}
