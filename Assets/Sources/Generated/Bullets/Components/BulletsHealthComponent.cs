//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BulletsEntity {

    public HealthComponent health { get { return (HealthComponent)GetComponent(BulletsComponentsLookup.Health); } }
    public bool hasHealth { get { return HasComponent(BulletsComponentsLookup.Health); } }

    public void AddHealth(int newValue) {
        var index = BulletsComponentsLookup.Health;
        var component = CreateComponent<HealthComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceHealth(int newValue) {
        var index = BulletsComponentsLookup.Health;
        var component = CreateComponent<HealthComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveHealth() {
        RemoveComponent(BulletsComponentsLookup.Health);
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
public partial class BulletsEntity : IHealth { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BulletsMatcher {

    static Entitas.IMatcher<BulletsEntity> _matcherHealth;

    public static Entitas.IMatcher<BulletsEntity> Health {
        get {
            if (_matcherHealth == null) {
                var matcher = (Entitas.Matcher<BulletsEntity>)Entitas.Matcher<BulletsEntity>.AllOf(BulletsComponentsLookup.Health);
                matcher.componentNames = BulletsComponentsLookup.componentNames;
                _matcherHealth = matcher;
            }

            return _matcherHealth;
        }
    }
}