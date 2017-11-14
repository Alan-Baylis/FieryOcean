using Entitas;
using UnityEngine;

public class CollisionEmitter : MonoBehaviour
{
    public string targetTag1;
    public string targetTag2;



    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Length != 0)
        {
            if (collision.gameObject.CompareTag(targetTag1) || collision.gameObject.CompareTag(targetTag2))
            {
                var link = gameObject.GetEntityLink();
                //var targetLink = collision.gameObject.GetEntityLink();
                
                Contexts.sharedInstance.input.CreateEntity()
                    .AddCollision(link.entity, null /*targetLink.entity*/, ShipSectors.ShipSector.None );
            }
        }
    }
}
