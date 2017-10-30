//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TurretComponent turret { get { return (TurretComponent)GetComponent(GameComponentsLookup.Turret); } }
    public bool hasTurret { get { return HasComponent(GameComponentsLookup.Turret); } }

    public void AddTurret(float newSpeed, UnityEngine.Vector3 newSwivelRotation, UnityEngine.Vector3 newBarrelRotation) {
        var index = GameComponentsLookup.Turret;
        var component = CreateComponent<TurretComponent>(index);
        component.speed = newSpeed;
        component.swivelRotation = newSwivelRotation;
        component.barrelRotation = newBarrelRotation;
        AddComponent(index, component);
    }

    public void ReplaceTurret(float newSpeed, UnityEngine.Vector3 newSwivelRotation, UnityEngine.Vector3 newBarrelRotation) {
        var index = GameComponentsLookup.Turret;
        var component = CreateComponent<TurretComponent>(index);
        component.speed = newSpeed;
        component.swivelRotation = newSwivelRotation;
        component.barrelRotation = newBarrelRotation;
        ReplaceComponent(index, component);
    }

    public void RemoveTurret() {
        RemoveComponent(GameComponentsLookup.Turret);
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

    static Entitas.IMatcher<GameEntity> _matcherTurret;

    public static Entitas.IMatcher<GameEntity> Turret {
        get {
            if (_matcherTurret == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Turret);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTurret = matcher;
            }

            return _matcherTurret;
        }
    }
}
