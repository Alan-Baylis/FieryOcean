using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public partial class AddViewFromObjectPoolSystem : ReactiveSystem<BulletsEntity>
{
    //public TriggerOnEvent trigger { get { return BulletsMatcher.ViewObjectPool.OnEntityAdded(); } }
    //public IMatcher ensureComponents { get { return BulletsMatcher.ViewObjectPool; } }

    Contexts _pools;
    Transform _container;

    public AddViewFromObjectPoolSystem(Contexts contexts) : base(contexts.bullets)
    {
        _pools = contexts;
    }

    public void Initialize() {
        _container = new GameObject(" bullets Views (Pooled)").transform;
    }

    protected override void Execute(List<BulletsEntity> entities)
    {
        foreach (var e in entities) {
            var gameObject = e.viewObjectPool.pool.Get();
            gameObject.SetActive(true);
            gameObject.transform.SetParent(_container, false);
            gameObject.Link(e, _pools.bullets);
            e.AddView(gameObject.GetComponent<IViewController>());
        }
    }

    protected override ICollector<BulletsEntity> GetTrigger(IContext<BulletsEntity> context)   {
        return context.CreateCollector(BulletsMatcher.ViewObjectPool.Added());
    }

    protected override bool Filter(BulletsEntity entity)    {
        return entity.hasViewObjectPool;
    }
}
