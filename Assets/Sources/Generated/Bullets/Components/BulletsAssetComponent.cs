//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BulletsEntity {

    public AssetComponent asset { get { return (AssetComponent)GetComponent(BulletsComponentsLookup.Asset); } }
    public bool hasAsset { get { return HasComponent(BulletsComponentsLookup.Asset); } }

    public void AddAsset(string newName, string newCanvasActions, string newProjectorAim, string newProjectAimCross) {
        var index = BulletsComponentsLookup.Asset;
        var component = CreateComponent<AssetComponent>(index);
        component.name = newName;
        component.canvasActions = newCanvasActions;
        component.projectorAim = newProjectorAim;
        component.projectAimCross = newProjectAimCross;
        AddComponent(index, component);
    }

    public void ReplaceAsset(string newName, string newCanvasActions, string newProjectorAim, string newProjectAimCross) {
        var index = BulletsComponentsLookup.Asset;
        var component = CreateComponent<AssetComponent>(index);
        component.name = newName;
        component.canvasActions = newCanvasActions;
        component.projectorAim = newProjectorAim;
        component.projectAimCross = newProjectAimCross;
        ReplaceComponent(index, component);
    }

    public void RemoveAsset() {
        RemoveComponent(BulletsComponentsLookup.Asset);
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
public partial class BulletsEntity : IAsset { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BulletsMatcher {

    static Entitas.IMatcher<BulletsEntity> _matcherAsset;

    public static Entitas.IMatcher<BulletsEntity> Asset {
        get {
            if (_matcherAsset == null) {
                var matcher = (Entitas.Matcher<BulletsEntity>)Entitas.Matcher<BulletsEntity>.AllOf(BulletsComponentsLookup.Asset);
                matcher.componentNames = BulletsComponentsLookup.componentNames;
                _matcherAsset = matcher;
            }

            return _matcherAsset;
        }
    }
}
