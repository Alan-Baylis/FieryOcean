//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BulletsEntity {

    static readonly OutOfScreenComponent outOfScreenComponent = new OutOfScreenComponent();

    public bool isOutOfScreen {
        get { return HasComponent(BulletsComponentsLookup.OutOfScreen); }
        set {
            if (value != isOutOfScreen) {
                if (value) {
                    AddComponent(BulletsComponentsLookup.OutOfScreen, outOfScreenComponent);
                } else {
                    RemoveComponent(BulletsComponentsLookup.OutOfScreen);
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
public partial class BulletsEntity : IOutOfScreen { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BulletsMatcher {

    static Entitas.IMatcher<BulletsEntity> _matcherOutOfScreen;

    public static Entitas.IMatcher<BulletsEntity> OutOfScreen {
        get {
            if (_matcherOutOfScreen == null) {
                var matcher = (Entitas.Matcher<BulletsEntity>)Entitas.Matcher<BulletsEntity>.AllOf(BulletsComponentsLookup.OutOfScreen);
                matcher.componentNames = BulletsComponentsLookup.componentNames;
                _matcherOutOfScreen = matcher;
            }

            return _matcherOutOfScreen;
        }
    }
}
