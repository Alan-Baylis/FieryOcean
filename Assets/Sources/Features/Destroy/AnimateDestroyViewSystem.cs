using System;
using System.Collections.Generic;
using Entitas;

public sealed class AnimateDestroyViewSystem : ReactiveSystem<GameEntity> { 

    readonly GameContext _context;
    public AnimateDestroyViewSystem(Contexts contexts) :base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach(var e in entities) {
            //var controller = e.view.controller;
            //controller.Hide(true);
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
                context.GetGroup(GameMatcher.View),
                context.GetGroup(GameMatcher.Destroy)
            }, 
            new[] {
                GroupEvent.AddedOrRemoved,
                GroupEvent.Added
            });
    }
}
