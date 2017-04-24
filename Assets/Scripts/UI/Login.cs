using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class Login : MonoBehaviour {

    private string stringAccount = "";
    private string stringPasswd = "";
    private Color labelColor = Color.green;

    private Dictionary<UInt64, Dictionary<string, object>> ui_avatarList = null;

    private string stringAvatarName = "";
    private bool startCreateAvatar = false;

    private UInt64 selAvatarDBID = 0;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void OnGUI()
    {
        onLoginUI();
    }

    void onLoginUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 30, 200, 30), "Login"))
        {
            Debug.Log("stringAccount:" + stringAccount);
            Debug.Log("stringPasswd:" + stringPasswd);

            if (stringAccount.Length > 0 && stringPasswd.Length > 5)
            {
                login();
            }
            else
            {
                UIcommon.inst.err("account or password is error, length < 6!");
            }
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 70, 200, 30), "CreateAccount"))
        {
            Debug.Log("stringAccount:" + stringAccount);
            Debug.Log("stringPasswd:" + stringPasswd);

            if (stringAccount.Length > 0 && stringPasswd.Length > 5)
            {
                createAccount();
            }
            else
            {
                UIcommon.inst.err("account or password is error, length < 6!");
            }
        }

        stringAccount = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 30), stringAccount, 20);
        stringPasswd = GUI.PasswordField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 30), stringPasswd, '*');

        GUI.Label(new Rect((Screen.width / 2) - 100, 40, 400, 100), UIcommon.inst.labelMsg);
    }


    public void login()
    {
        UIcommon.inst.info("connect to server...");
        KBEngine.Event.fireIn("login", stringAccount, stringPasswd, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
    }

    public void createAccount()
    {
        UIcommon.inst.info("connect to server...");

        KBEngine.Event.fireIn("createAccount", stringAccount, stringPasswd, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
    }
}
