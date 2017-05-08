//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Entitas;

namespace Entitas {

    public partial class Entity {

        public ServerImpOfUnitComponent serverImpOfUnit { get { return (ServerImpOfUnitComponent)GetComponent(CoreComponentIds.ServerImpOfUnit); } }
        public bool hasServerImpOfUnit { get { return HasComponent(CoreComponentIds.ServerImpOfUnit); } }

        public Entity AddServerImpOfUnit(KBEngine.Entity newEntity) {
            var component = CreateComponent<ServerImpOfUnitComponent>(CoreComponentIds.ServerImpOfUnit);
            component.entity = newEntity;
            return AddComponent(CoreComponentIds.ServerImpOfUnit, component);
        }

        public Entity ReplaceServerImpOfUnit(KBEngine.Entity newEntity) {
            var component = CreateComponent<ServerImpOfUnitComponent>(CoreComponentIds.ServerImpOfUnit);
            component.entity = newEntity;
            ReplaceComponent(CoreComponentIds.ServerImpOfUnit, component);
            return this;
        }

        public Entity RemoveServerImpOfUnit() {
            return RemoveComponent(CoreComponentIds.ServerImpOfUnit);
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherServerImpOfUnit;

        public static IMatcher ServerImpOfUnit {
            get {
                if(_matcherServerImpOfUnit == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.ServerImpOfUnit);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherServerImpOfUnit = matcher;
                }

                return _matcherServerImpOfUnit;
            }
        }
    }
