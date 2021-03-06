//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BulletsEntity {

    public BulletComponent bullet { get { return (BulletComponent)GetComponent(BulletsComponentsLookup.Bullet); } }
    public bool hasBullet { get { return HasComponent(BulletsComponentsLookup.Bullet); } }

    public void AddBullet(float newFiringAngle, float newGravity, float newSpeed, UnityEngine.Vector3 newPosition, UnityEngine.Vector3 newTarget, float newElapseTime, float newFlightDuration, float newVx, float newVy, bool newFlagDestroy) {
        var index = BulletsComponentsLookup.Bullet;
        var component = CreateComponent<BulletComponent>(index);
        component.firingAngle = newFiringAngle;
        component.gravity = newGravity;
        component.speed = newSpeed;
        component.position = newPosition;
        component.target = newTarget;
        component.elapseTime = newElapseTime;
        component.flightDuration = newFlightDuration;
        component.Vx = newVx;
        component.Vy = newVy;
        component.flagDestroy = newFlagDestroy;
        AddComponent(index, component);
    }

    public void ReplaceBullet(float newFiringAngle, float newGravity, float newSpeed, UnityEngine.Vector3 newPosition, UnityEngine.Vector3 newTarget, float newElapseTime, float newFlightDuration, float newVx, float newVy, bool newFlagDestroy) {
        var index = BulletsComponentsLookup.Bullet;
        var component = CreateComponent<BulletComponent>(index);
        component.firingAngle = newFiringAngle;
        component.gravity = newGravity;
        component.speed = newSpeed;
        component.position = newPosition;
        component.target = newTarget;
        component.elapseTime = newElapseTime;
        component.flightDuration = newFlightDuration;
        component.Vx = newVx;
        component.Vy = newVy;
        component.flagDestroy = newFlagDestroy;
        ReplaceComponent(index, component);
    }

    public void RemoveBullet() {
        RemoveComponent(BulletsComponentsLookup.Bullet);
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

    static Entitas.IMatcher<BulletsEntity> _matcherBullet;

    public static Entitas.IMatcher<BulletsEntity> Bullet {
        get {
            if (_matcherBullet == null) {
                var matcher = (Entitas.Matcher<BulletsEntity>)Entitas.Matcher<BulletsEntity>.AllOf(BulletsComponentsLookup.Bullet);
                matcher.componentNames = BulletsComponentsLookup.componentNames;
                _matcherBullet = matcher;
            }

            return _matcherBullet;
        }
    }
}
