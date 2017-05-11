using UnityEngine;
using KBEngine;

namespace Entitas.Unity.Serialization.Blueprints {

    public partial class Blueprints {

        public Entity ApplyPlayer1(Entity entity, Vector3 position) {
            return entity.ApplyBlueprint(Player1)
                .AddPosition(position)
                ////.AddPlayerPosition(position)
                .AddForse(Vector3.zero, 0)
                .AddServerImpOfUnit(null);
                ////.AddWhoIAm(WhoIAm.IAm.PLAYER);
        }

        /*public Entity ApplyBullet(Entity entity, Vector3 position, Vector3 velocity, ObjectPool<GameObject> gameObjectPool) {
            return entity.ApplyBlueprint(Bullet)
                .AddPosition(position)
                .AddVelocity(velocity)
                .AddViewObjectPool(gameObjectPool);
        }*/
        public Entity ApplyEnemy(Entity entity, Vector3 position) {
            return entity.ApplyBlueprint(Enemy)
                         .AddPosition(position);
                         //.AddEnemyPosition(position);
                         //.AddWhoIAm(WhoIAm.IAm.ENEMY);
        }

        public Entity AddUnit(Entity entity, KBEngine.Entity server_entity)
        {
            return entity.ApplyBlueprint(RemotePlayer)
                         .AddPosition(server_entity.position)
                         .AddServerImpOfUnit(server_entity);
                         
                         
            //.AddEnemyPosition(position);
            //.AddWhoIAm(WhoIAm.IAm.ENEMY);
        }

    }
}
