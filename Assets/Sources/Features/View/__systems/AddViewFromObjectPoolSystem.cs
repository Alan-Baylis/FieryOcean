using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class AddViewFromObjectPoolSystem : ReactiveSystem { //IInitializeSystem, ReactiveSystem {

    //TODO
    public AddViewFromObjectPoolSystem(Contexts contexts) : base(contexts.core) {
        //_pool = pool;
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(BulletsMatcher.ViewObjectPool);
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: BulletsMatcher.ViewObjectPool
        // exclude was: 

        //return (ViewObjectPool);
        return true;
    }

    Context _pool;
    Transform _container;

    public void Initialize() {
        _container = new GameObject(/*_pool.metaData.poolName + */ " Views (Pooled)").transform;
    }

    protected override void Execute(List<Entity> entities) {
        foreach(var e in entities) {
            var gameObject = e.viewObjectPool.pool.Get();
            gameObject.SetActive(true);
            gameObject.transform.SetParent(_container, false);
            gameObject.Link(e, _pool);
            e.AddView(gameObject.GetComponent<IViewController>());
        }
    }
}
