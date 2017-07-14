using Entitas;

public sealed class BulletOutOfScreenSystem : IExecuteSystem {

    IGroup<BulletsEntity> _bullets;

    // TODO Entitas 0.36.0 Migration (constructor)
    //public void SetPools(Contexts pools) {
    //    _bullets = pools.bullets.GetGroup(Matcher.AllOf(BulletsMatcher.Bullet, BulletsMatcher.Position));
    //}

    public void Execute() {
        foreach(var e in _bullets.GetEntities()) {

            // TODO Define OutOfScreen Y position
            // TODO When OutOfScreen at the bottom

            //if(e.position.value.y > 20f) {
            //    e.isOutOfScreen = true;
           // }
        }
    }
}
