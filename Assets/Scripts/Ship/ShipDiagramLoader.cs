using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDiagramLoader : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        GameObject shipDiagram = Assets.Instantiate<GameObject>(Sources.StrategicPart.PlayerSingleton.shipDiagramPrefab);
        shipDiagram.transform.SetParent(transform, false);

        RectTransform rt = shipDiagram.GetComponent<RectTransform>();//.position = new Vector3(590, 13, 0);
        rt.anchorMin = new Vector2(0.5f,0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.localPosition = new Vector3(590, 13, 0);
    }
}
