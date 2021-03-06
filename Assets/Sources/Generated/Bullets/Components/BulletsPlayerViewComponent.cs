//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BulletsEntity {

    public PlayerViewComponent playerView { get { return (PlayerViewComponent)GetComponent(BulletsComponentsLookup.PlayerView); } }
    public bool hasPlayerView { get { return HasComponent(BulletsComponentsLookup.PlayerView); } }

    public void AddPlayerView(IPlayerController newController) {
        var index = BulletsComponentsLookup.PlayerView;
        var component = CreateComponent<PlayerViewComponent>(index);
        component.controller = newController;
        AddComponent(index, component);
    }

    public void ReplacePlayerView(IPlayerController newController) {
        var index = BulletsComponentsLookup.PlayerView;
        var component = CreateComponent<PlayerViewComponent>(index);
        component.controller = newController;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerView() {
        RemoveComponent(BulletsComponentsLookup.PlayerView);
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
public partial class BulletsEntity : IPlayerView { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BulletsMatcher {

    static Entitas.IMatcher<BulletsEntity> _matcherPlayerView;

    public static Entitas.IMatcher<BulletsEntity> PlayerView {
        get {
            if (_matcherPlayerView == null) {
                var matcher = (Entitas.Matcher<BulletsEntity>)Entitas.Matcher<BulletsEntity>.AllOf(BulletsComponentsLookup.PlayerView);
                matcher.componentNames = BulletsComponentsLookup.componentNames;
                _matcherPlayerView = matcher;
            }

            return _matcherPlayerView;
        }
    }
}
