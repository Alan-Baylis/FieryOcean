using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Position : MonoBehaviour {
    public class EntityMy
    {
        public string className;
    }

    public Dictionary<Int32, EntityMy> entities = new Dictionary<Int32, EntityMy>();
    public Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
    // Use this for initialization
    void Start () {
        position.Set(11, 2, 0);

        EntityMy e0 = new EntityMy();
        e0.className = "1";
        entities.Add(0, e0);

        EntityMy e1,e3;
        e3 = new EntityMy();
        if (entities.TryGetValue(0, out e1))
        {
            e1.className = "2";
            Debug.Log("e0 classNmae: "+e0.className);
            Debug.Log("e1 " + e1.GetHashCode().ToString() + " e3  " + e3.GetHashCode().ToString());
        }

}

// Update is called once per frame
void Update () {
        //gameObject.
	}
}
