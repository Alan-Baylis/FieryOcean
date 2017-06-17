using Entitas;
using System.Collections.Generic;
using System;
using UnityEngine;

public sealed class ProcessMoveInputSystem : ReactiveSystem //IMultiReactiveSystem
{
     public ProcessMoveInputSystem(Contexts contexts) : base(contexts.core) {
    //    _pools = pools;
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(/*Matcher.AllOf(InputMatcher.MoveInput, CoreMatcher.Rigidbody).OnEntityAdded();*/  InputMatcher.MoveInput);
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }

    //public TriggerOnEvent[] triggers { get { return new TriggerOnEvent[] { InputMatcher.MoveInput.OnEntityAdded() /*, CoreMatcher.Rigidbody.OnEntityAdded()*/ }; } }

    Contexts _pools;

    protected override void Execute(List<Entity> entities) {
        var input = entities[entities.Count - 1];
        var ownerId = input.inputOwner.playerId;

        var e = _pools.core.GetEntityWithPlayerId(ownerId);

        // TODO Speed Shoud be configurable
        //e.ReplaceVelocity(Vector3.zero);
        e.ReplaceForse(Vector3.left,input.moveInput.accelerate);
    }
}
