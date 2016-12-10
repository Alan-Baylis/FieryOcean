using UnityEngine;
using System.Collections;

public class joystickController : MonoBehaviour {
    public UltimateJoystick joystick;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(joystick.GetPosition());
    }
}
