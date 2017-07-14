using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CreatePlayerSystem : ReactiveSystem {
    private Vector3 _position;
    Contexts _pools;

    // TODO Entitas 0.36.0 Migration (constructor)
   
    public CreatePlayerSystem(Contexts pools) : base(pools.core)
    {
        _pools = pools;
    }

    public void SetPosition(Vector3 playerStartPosition)
    {
        _position = playerStartPosition;
    }

    public void Initialize() {
        _pools.blueprints.blueprints.instance
              .ApplyPlayer1(_pools.core.CreateEntity(), _position);
    }

    protected override Collector GetTrigger(Context context)
    {
        return context.CreateCollector(CoreMatcher.Player);
    }

    protected override bool Filter(Entity entity)
    {
        return false;
    }

    protected override void Execute(List<Entity> entities)
    {
        //throw new NotImplementedException();
    }
}
