using Entitas;
using System.Collections.Generic;
using System;
using UnityEngine;

public sealed class ProcessMoveInputSystem : ISetPools, IMultiReactiveSystem
{
    // public TriggerOnEvent trigger { get { return /*Matcher.AllOf(InputMatcher.MoveInput, CoreMatcher.Rigidbody).OnEntityAdded();*/  InputMatcher.MoveInput.OnEntityAdded(); } }

    public TriggerOnEvent[] triggers { get { return new TriggerOnEvent[] { InputMatcher.MoveInput.OnEntityAdded() /*, CoreMatcher.Rigidbody.OnEntityAdded()*/ }; } }

    Pools _pools;

    public void SetPools(Pools pools) {
        _pools = pools;
    }

    public void Execute(List<Entity> entities) {
        var input = entities[entities.Count - 1];
        var ownerId = input.inputOwner.playerId;

        var e = _pools.core.GetEntityWithPlayerId(ownerId);

        // TODO Speed Shoud be configurable
        //e.ReplaceVelocity(Vector3.zero);
        e.ReplaceForse(Vector3.left,input.moveInput.accelerate);
    }
}
