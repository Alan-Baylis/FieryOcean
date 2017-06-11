using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

public class SelAvatars : MonoBehaviour {

    private string stringAvatarName = "";
    private bool startCreateAvatar = false;

    private UInt64 selAvatarDBID = 0;
    public Dictionary<UInt64, Dictionary<string, object>> ui_avatarList = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        onSelAvatarUI();
    }

    int buttonHeight = 30;

    void onSelAvatarUI()
    {
        ui_avatarList = EventsRegistrator.inst.sel_avatars.ui_avatarList;
        if (startCreateAvatar == false && GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height/2 - buttonHeight*1.5f, 200, buttonHeight), "RemoveAvatar"))
        {
            if (selAvatarDBID == 0)
            {
                UIcommon.inst.err("Please select a Avatar!");
            }
            else
            {
                UIcommon.inst.info("Please wait...");

                if (ui_avatarList != null && ui_avatarList.Count > 0)
                {
                    Dictionary<string, object> avatarinfo = ui_avatarList[selAvatarDBID];
                    KBEngine.Event.fireIn("reqRemoveAvatar", (string)avatarinfo["name"]);
                }
            }
        }

        if (startCreateAvatar == false && GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height/2 + buttonHeight*1.5f, 200, buttonHeight), "CreateAvatar"))
        {
            startCreateAvatar = !startCreateAvatar;
        }

        if (startCreateAvatar == false && GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height/2, 200, buttonHeight), "EnterGame"))
        {
            if (selAvatarDBID == 0)
            {
                UIcommon.inst.err("Please select a Avatar!");
            }
            else
            {
                UIcommon.inst.info("Please wait...");
                PlayerPrefs.SetFloat("selAvatarDBID", selAvatarDBID);

                Application.LoadLevel("battle");
                //ui_state = 2;
            }
        }

        if (startCreateAvatar)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height/2 + 40, 200, buttonHeight), "CreateAvatar-OK"))
            {
                if (stringAvatarName.Length > 1)
                {
                    startCreateAvatar = !startCreateAvatar;
                    KBEngine.Event.fireIn("reqCreateAvatar", (Byte)1, stringAvatarName);
                }
                else
                {
                    UIcommon.inst.err("avatar name is null");
                }
            }

            stringAvatarName = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height/2, 200, 30), stringAvatarName, 20);
        }

        if (ui_avatarList != null && EventsRegistrator.inst.sel_avatars.ui_avatarList.Count > 0)
        {
            int idx = 0;
            foreach (UInt64 dbid in EventsRegistrator.inst.sel_avatars.ui_avatarList.Keys)
            {
                Dictionary<string, object> info = ui_avatarList[dbid];
                //	Byte roleType = (Byte)info["roleType"];
                string name = (string)info["name"];
                //	UInt16 level = (UInt16)info["level"];
                UInt64 idbid = (UInt64)info["dbid"];

                idx++;

                Color color = GUI.contentColor;
                if (selAvatarDBID == idbid)
                {
                    GUI.contentColor = Color.red;
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + Screen.height / 3 - 35 * idx, 200, 30), name))
                {
                    Debug.Log("selAvatar:" + name);
                    selAvatarDBID = idbid;
                }

                GUI.contentColor = color;
            }
        }
        else
        {
            if (KBEngineApp.app.entity_type == "Account")
            {
                KBEngine.Account account = (KBEngine.Account)KBEngineApp.app.player();
                if (account != null)
                    ui_avatarList = new Dictionary<ulong, Dictionary<string, object>>(account.avatars);
            }
        }

        GUI.Label(new Rect((Screen.width / 2) - (Screen.width / 4), (Screen.height / 2) - (Screen.height / 4), 400, 100), UIcommon.inst.labelMsg);

    }
}
