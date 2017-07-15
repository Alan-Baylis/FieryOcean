//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BulletsEntity {

    static readonly DestroyComponent destroyComponent = new DestroyComponent();

    public bool flagDestroy {
        get { return HasComponent(BulletsComponentsLookup.Destroy); }
        set {
            if (value != flagDestroy) {
                if (value) {
                    AddComponent(BulletsComponentsLookup.Destroy, destroyComponent);
                } else {
                    RemoveComponent(BulletsComponentsLookup.Destroy);
                }
            }
        }
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
public partial class BulletsEntity : IDestroy { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BulletsMatcher {

    static Entitas.IMatcher<BulletsEntity> _matcherDestroy;

    public static Entitas.IMatcher<BulletsEntity> Destroy {
        get {
            if (_matcherDestroy == null) {
                var matcher = (Entitas.Matcher<BulletsEntity>)Entitas.Matcher<BulletsEntity>.AllOf(BulletsComponentsLookup.Destroy);
                matcher.componentNames = BulletsComponentsLookup.componentNames;
                _matcherDestroy = matcher;
            }

            return _matcherDestroy;
        }
    }
}