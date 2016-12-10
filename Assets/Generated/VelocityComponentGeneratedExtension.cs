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

        public VelocityComponent velocity { get { return (VelocityComponent)GetComponent(BulletsComponentIds.Velocity); } }
        public bool hasVelocity { get { return HasComponent(BulletsComponentIds.Velocity); } }

        public Entity AddVelocity(UnityEngine.Vector3 newValue) {
            var component = CreateComponent<VelocityComponent>(BulletsComponentIds.Velocity);
            component.value = newValue;
            return AddComponent(BulletsComponentIds.Velocity, component);
        }

        public Entity ReplaceVelocity(UnityEngine.Vector3 newValue) {
            var component = CreateComponent<VelocityComponent>(BulletsComponentIds.Velocity);
            component.value = newValue;
            ReplaceComponent(BulletsComponentIds.Velocity, component);
            return this;
        }

        public Entity RemoveVelocity() {
            return RemoveComponent(BulletsComponentIds.Velocity);
        }
    }
}

    public partial class BulletsMatcher {

        static IMatcher _matcherVelocity;

        public static IMatcher Velocity {
            get {
                if(_matcherVelocity == null) {
                    var matcher = (Matcher)Matcher.AllOf(BulletsComponentIds.Velocity);
                    matcher.componentNames = BulletsComponentIds.componentNames;
                    _matcherVelocity = matcher;
                }

                return _matcherVelocity;
            }
        }
    }

    public partial class CoreMatcher {

        static IMatcher _matcherVelocity;

        public static IMatcher Velocity {
            get {
                if(_matcherVelocity == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Velocity);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherVelocity = matcher;
                }

                return _matcherVelocity;
            }
        }
    }
