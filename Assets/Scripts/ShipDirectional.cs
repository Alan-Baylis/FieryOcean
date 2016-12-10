using UnityEngine;
using System.Collections;

public class ShipDirectional : MonoBehaviour {

    public Transform stern;
    public Transform bow;

    public Vector3 GetShipDirectional()  {
        return stern.transform.position - bow.transform.position;

    }
}
