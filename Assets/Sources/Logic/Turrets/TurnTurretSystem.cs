using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public sealed class TurnTurretSystem : ReactiveSystem<InputEntity>
{
    private GameContext _gameContext;

    public TurnTurretSystem(Contexts contexts):base(contexts.input)
    {
        _gameContext = contexts.game;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (InputEntity e in entities)
        {
            foreach (GameEntity g in _gameContext.GetEntities())
            {
                if (g.hasPlayerView)
                {
                    g.playerView.controller.GetTurret(e.selectTurret.turretId).trajectoryEnable = e.selectTurret.Enable;
                }
            }
        }
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasSelectTurret;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return new Collector<InputEntity>(
          new[] { context.GetGroup(InputMatcher.SelectTurret)},
          new[] { GroupEvent.AddedOrRemoved });
    }
}
