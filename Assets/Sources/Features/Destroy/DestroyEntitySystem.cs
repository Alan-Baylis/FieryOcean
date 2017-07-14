using System;
using System.Collections.Generic;
using Entitas;

public sealed class DestroyEntitySystem : ReactiveSystem<GameEntity> {

    public DestroyEntitySystem(Contexts contexts) : base(contexts.game)
    { }

    protected override void Execute(List<GameEntity> entities) {
        foreach(var e in entities) {
            e.Destroy();
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return new Collector<GameEntity>(
           new[] {
                context.GetGroup(GameMatcher.OutOfScreen),
                context.GetGroup(GameMatcher.Destroy)
           },
           new[] {
                GroupEvent.Added,
                GroupEvent.Added
           });
    }
}
