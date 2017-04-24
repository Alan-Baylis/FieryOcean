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

        public WhoAMi whoAMi { get { return (WhoAMi)GetComponent(CoreComponentIds.WhoAMi); } }
        public bool hasWhoAMi { get { return HasComponent(CoreComponentIds.WhoAMi); } }

        public Entity AddWhoAMi(int newValue) {
            var component = CreateComponent<WhoAMi>(CoreComponentIds.WhoAMi);
            component.value = newValue;
            return AddComponent(CoreComponentIds.WhoAMi, component);
        }

        public Entity ReplaceWhoAMi(int newValue) {
            var component = CreateComponent<WhoAMi>(CoreComponentIds.WhoAMi);
            component.value = newValue;
            ReplaceComponent(CoreComponentIds.WhoAMi, component);
            return this;
        }

        public Entity RemoveWhoAMi() {
            return RemoveComponent(CoreComponentIds.WhoAMi);
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherWhoAMi;

        public static IMatcher WhoAMi {
            get {
                if(_matcherWhoAMi == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.WhoAMi);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherWhoAMi = matcher;
                }

                return _matcherWhoAMi;
            }
        }
    }
