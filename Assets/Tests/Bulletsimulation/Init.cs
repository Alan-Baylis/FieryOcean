using Forge3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

    public F3DTurret turret;
    public Transform ship;
    // Use this for initialization
    void Start () {

        turret.mashtab = 100f;
        //t.simulate_coefficient = simulate_coefficient;
        turret.gravity = 9f;
        turret.ocean = GameObject.FindGameObjectWithTag("Ocean").transform;
        turret.CustomAwake();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("ship pos: " + ship.position.ToString());
	}
}
