using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public sealed class CreateEnemySystem : ReactiveSystem<GameEntity> {
    private Vector3 _position;
    Contexts _pools;
    public CreateEnemySystem(Contexts contexts, Vector3[] positions) : base( contexts.game) {
        //_pools = contexts.blueprints;
        //contexts.blueprints.blueprints.instance.ApplyEnemy(contexts.game.CreateEntity(), _position);

        foreach(Vector3 v in positions)
            contexts.game.CreateEnemy(v);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.UnitAdd);
    }

    protected override bool Filter(GameEntity entity) {

        return true;
    }

    //void IInitializeSystem.Initialize()
    //{
    //    _pools.blueprints.blueprints.instance.ApplyEnemy(_pools.core.CreateEntity(),_position);
    //}

    public void SetPosition(Vector3[] positions)
    {
        _position = positions[0];
    }

    //public CreateEnemySystem(Vector3[] positions)
    //{
    //    _position = positions[0];
    //}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            _pools.game.CreateRemotePlayer(_position, entity.unitAdd.entity);
            //_pools.blueprints.blueprints.instance.AddUnit(_pools.game.CreateEntity(), entity.unitAdd.entity);
        }
    }
}

