//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BulletsEntity {

    public ViewComponent view { get { return (ViewComponent)GetComponent(BulletsComponentsLookup.View); } }
    public bool hasView { get { return HasComponent(BulletsComponentsLookup.View); } }

    public void AddView(IViewController newController) {
        var index = BulletsComponentsLookup.View;
        var component = CreateComponent<ViewComponent>(index);
        component.controller = newController;
        AddComponent(index, component);
    }

    public void ReplaceView(IViewController newController) {
        var index = BulletsComponentsLookup.View;
        var component = CreateComponent<ViewComponent>(index);
        component.controller = newController;
        ReplaceComponent(index, component);
    }

    public void RemoveView() {
        RemoveComponent(BulletsComponentsLookup.View);
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
public sealed partial class BulletsMatcher {

    static Entitas.IMatcher<BulletsEntity> _matcherView;

    public static Entitas.IMatcher<BulletsEntity> View {
        get {
            if (_matcherView == null) {
                var matcher = (Entitas.Matcher<BulletsEntity>)Entitas.Matcher<BulletsEntity>.AllOf(BulletsComponentsLookup.View);
                matcher.componentNames = BulletsComponentsLookup.componentNames;
                _matcherView = matcher;
            }

            return _matcherView;
        }
    }
}
