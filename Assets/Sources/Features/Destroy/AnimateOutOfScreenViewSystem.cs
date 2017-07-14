using System;
using System.Collections.Generic;
using Entitas;

public sealed class AnimateOutOfScreenViewSystem : ReactiveSystem<GameEntity> {
    public AnimateOutOfScreenViewSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var controller = e.view.controller;
            controller.Hide(false);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.OutOfScreen.Added());
    }
}


