//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly OutOfScreenComponent outOfScreenComponent = new OutOfScreenComponent();

    public bool isOutOfScreen {
        get { return HasComponent(GameComponentsLookup.OutOfScreen); }
        set {
            if (value != isOutOfScreen) {
                if (value) {
                    AddComponent(GameComponentsLookup.OutOfScreen, outOfScreenComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.OutOfScreen);
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
public partial class GameEntity : IOutOfScreen { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherOutOfScreen;

    public static Entitas.IMatcher<GameEntity> OutOfScreen {
        get {
            if (_matcherOutOfScreen == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.OutOfScreen);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherOutOfScreen = matcher;
            }

            return _matcherOutOfScreen;
        }
    }
}
