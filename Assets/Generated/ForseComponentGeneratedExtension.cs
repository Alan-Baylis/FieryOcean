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

        public ForseComponent forse { get { return (ForseComponent)GetComponent(BulletsComponentIds.Forse); } }
        public bool hasForse { get { return HasComponent(BulletsComponentIds.Forse); } }

        public Entity AddForse(UnityEngine.Vector3 newForce, float newAccelerate) {
            var component = CreateComponent<ForseComponent>(BulletsComponentIds.Forse);
            component.force = newForce;
            component.accelerate = newAccelerate;
            return AddComponent(BulletsComponentIds.Forse, component);
        }

        public Entity ReplaceForse(UnityEngine.Vector3 newForce, float newAccelerate) {
            var component = CreateComponent<ForseComponent>(BulletsComponentIds.Forse);
            component.force = newForce;
            component.accelerate = newAccelerate;
            ReplaceComponent(BulletsComponentIds.Forse, component);
            return this;
        }

        public Entity RemoveForse() {
            return RemoveComponent(BulletsComponentIds.Forse);
        }
    }
}

    public partial class BulletsMatcher {

        static IMatcher _matcherForse;

        public static IMatcher Forse {
            get {
                if(_matcherForse == null) {
                    var matcher = (Matcher)Matcher.AllOf(BulletsComponentIds.Forse);
                    matcher.componentNames = BulletsComponentIds.componentNames;
                    _matcherForse = matcher;
                }

                return _matcherForse;
            }
        }
    }

    public partial class CoreMatcher {

        static IMatcher _matcherForse;

        public static IMatcher Forse {
            get {
                if(_matcherForse == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Forse);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherForse = matcher;
                }

                return _matcherForse;
            }
        }
    }
