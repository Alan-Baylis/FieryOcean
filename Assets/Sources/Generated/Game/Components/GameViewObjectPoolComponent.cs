//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ViewObjectPoolComponent viewObjectPool { get { return (ViewObjectPoolComponent)GetComponent(GameComponentsLookup.ViewObjectPool); } }
    public bool hasViewObjectPool { get { return HasComponent(GameComponentsLookup.ViewObjectPool); } }

    public void AddViewObjectPool(Entitas.Utils.ObjectPool<UnityEngine.GameObject> newPool) {
        var index = GameComponentsLookup.ViewObjectPool;
        var component = CreateComponent<ViewObjectPoolComponent>(index);
        component.pool = newPool;
        AddComponent(index, component);
    }

    public void ReplaceViewObjectPool(Entitas.Utils.ObjectPool<UnityEngine.GameObject> newPool) {
        var index = GameComponentsLookup.ViewObjectPool;
        var component = CreateComponent<ViewObjectPoolComponent>(index);
        component.pool = newPool;
        ReplaceComponent(index, component);
    }

    public void RemoveViewObjectPool() {
        RemoveComponent(GameComponentsLookup.ViewObjectPool);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherViewObjectPool;

    public static Entitas.IMatcher<GameEntity> ViewObjectPool {
        get {
            if (_matcherViewObjectPool == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ViewObjectPool);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherViewObjectPool = matcher;
            }

            return _matcherViewObjectPool;
        }
    }
}
