using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public sealed class DestroyEnemySystem : ReactiveSystem<GameEntity> {
    public DestroyEnemySystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.Destroy();
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
       return context.CreateCollector(GameMatcher.DestroyUnit.Added());
    }
}

