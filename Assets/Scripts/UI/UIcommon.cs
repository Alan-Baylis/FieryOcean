using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcommon : MonoBehaviour {

    public static UIcommon inst;
    public string labelMsg = "";
    private Color labelColor = Color.green;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        inst = this;
      // Application.LoadLevel("login");
    }

    public void err(string s)
    {
        labelColor = Color.red;
        labelMsg = s;
    }

    public void info(string s)
    {
        labelColor = Color.green;
        labelMsg = s;

    }
}
