//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public SelectTurretComponent selectTurret { get { return (SelectTurretComponent)GetComponent(InputComponentsLookup.SelectTurret); } }
    public bool hasSelectTurret { get { return HasComponent(InputComponentsLookup.SelectTurret); } }

    public void AddSelectTurret(uint newTurretId, bool newEnable) {
        var index = InputComponentsLookup.SelectTurret;
        var component = CreateComponent<SelectTurretComponent>(index);
        component.turretId = newTurretId;
        component.Enable = newEnable;
        AddComponent(index, component);
    }

    public void ReplaceSelectTurret(uint newTurretId, bool newEnable) {
        var index = InputComponentsLookup.SelectTurret;
        var component = CreateComponent<SelectTurretComponent>(index);
        component.turretId = newTurretId;
        component.Enable = newEnable;
        ReplaceComponent(index, component);
    }

    public void RemoveSelectTurret() {
        RemoveComponent(InputComponentsLookup.SelectTurret);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherSelectTurret;

    public static Entitas.IMatcher<InputEntity> SelectTurret {
        get {
            if (_matcherSelectTurret == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.SelectTurret);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherSelectTurret = matcher;
            }

            return _matcherSelectTurret;
        }
    }
}