using UnityEngine;
using System.Collections;

public class ShipDirectional : MonoBehaviour {

    public Transform stern;
    public Transform bow;

    public Vector3 GetShipDirectional()  {
        return bow.transform.position - stern.transform.position;
    }
}
