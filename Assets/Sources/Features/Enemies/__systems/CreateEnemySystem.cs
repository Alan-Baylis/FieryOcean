using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public sealed class CreateEnemySystem : ReactiveSystem //IInitializeSystem, ReactiveSystem
{
    private Vector3 _position;
    Contexts _pools;
    public CreateEnemySystem(Contexts contexts) : base(contexts.core) {
        //_pools = pools;
        _pools.blueprints.blueprints.instance.ApplyEnemy(_pools.core.CreateEntity(), _position);
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.UnitAdd);
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

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

    protected override void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            _pools.blueprints.blueprints.instance.AddUnit(_pools.core.CreateEntity(), entity.unitAdd.entity);
        }
    }
}

