//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AimComponent aim { get { return (AimComponent)GetComponent(GameComponentsLookup.Aim); } }
    public bool hasAim { get { return HasComponent(GameComponentsLookup.Aim); } }

    public void AddAim(UnityEngine.Vector3 newValue) {
        var index = GameComponentsLookup.Aim;
        var component = CreateComponent<AimComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAim(UnityEngine.Vector3 newValue) {
        var index = GameComponentsLookup.Aim;
        var component = CreateComponent<AimComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAim() {
        RemoveComponent(GameComponentsLookup.Aim);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity : IAim { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAim;

    public static Entitas.IMatcher<GameEntity> Aim {
        get {
            if (_matcherAim == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Aim);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAim = matcher;
            }

            return _matcherAim;
        }
    }
}