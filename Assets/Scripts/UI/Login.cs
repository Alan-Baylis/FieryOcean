using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    
    public InputField Inputlogin;
    public InputField Inputpassword;

    private Color labelColor = Color.green;

    private Dictionary<UInt64, Dictionary<string, object>> ui_avatarList = null;

    private string stringAvatarName = "";
    private bool startCreateAvatar = false;

    private UInt64 selAvatarDBID = 0;

    public void login()
    {
        Debug.Log("stringAccount:" + Inputlogin.text);
        Debug.Log("stringPasswd:" + Inputpassword.text);

        if (Inputlogin.text.Length > 0 && Inputpassword.text.Length > 5)
        {
            UIcommon.inst.info("connect to server...");
            KBEngine.Event.fireIn("login", Inputlogin.text, Inputpassword.text, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
        }
        else
        {
            UIcommon.inst.err("account or password is error, length < 6!");
        }
    }

    public void createAccount()
    {
        Debug.Log("stringAccount:" + Inputlogin.text);
        Debug.Log("stringPasswd:" + Inputpassword.text);

        if (Inputlogin.text.Length > 0 && Inputpassword.text.Length > 5)
        {
            UIcommon.inst.info("connect to server...");

            KBEngine.Event.fireIn("createAccount", Inputlogin.text, Inputpassword.text, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
        }
        else
        {
            UIcommon.inst.err("account or password is error, length < 6!");
        }
    }
}
