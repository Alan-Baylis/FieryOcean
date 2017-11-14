using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionEmmiterShip : MonoBehaviour {
    public string[] targetTags = new string[_SIZE];
    private const int _SIZE = 2;

    public ShipSectors.ShipSector sector = ShipSectors.ShipSector.None;

    void OnValidate()
    {
        if (targetTags.Length != _SIZE)
        {
            Array.Resize(ref targetTags, _SIZE);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Length != 0)
        {
            foreach (string s in targetTags)
            {
                if (collision.gameObject.CompareTag(s))
                {
                    var link = gameObject.GetEntityLinkParet();
                    var targetLink = collision.gameObject.GetEntityLink();

                    Contexts.sharedInstance.input.CreateEntity()
                        .AddCollision(link.entity, targetLink.entity,sector);
                }
            }
        }
    }
}
