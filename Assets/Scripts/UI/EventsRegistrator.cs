using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class EventsRegistrator : MonoBehaviour {

    public static EventsRegistrator inst;
    public SelAvatars sel_avatars;
    Login login;

    public class SelAvatars
    {
        public Dictionary<UInt64, Dictionary<string, object>> ui_avatarList = null;

        public void SetUIAvatarList(Dictionary<UInt64, Dictionary<string, object>> ui_avatarList)
        {
            this.ui_avatarList = ui_avatarList;
        }

        // // Use this for initialization
        public SelAvatars()
        {
            // select-avatars(register by scripts)
            KBEngine.Event.registerOut("onReqAvatarList", this, "onReqAvatarList");
            KBEngine.Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
            KBEngine.Event.registerOut("onRemoveAvatar", this, "onRemoveAvatar");

        }

        public void onReqAvatarList(Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            ui_avatarList = avatarList;
        }

        public void onCreateAvatarResult(Byte retcode, object info, Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            if (retcode != 0)
            {
                UIcommon.inst.err("Error creating avatar, errcode=" + retcode);
                return;
            }

            onReqAvatarList(avatarList);
        }

        public void onRemoveAvatar(UInt64 dbid, Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            if (dbid == 0)
            {
                UIcommon.inst.err("Delete the avatar error!");
                return;
            }

            onReqAvatarList(avatarList);
        }
    }

   // private string labelMsg = "";
   
   // private Color labelColor;
    //private string stringAccount = "";


    public class Login
    {
        private string stringAccount="";

        public void InstallEvents()
        {
            // login
            KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
            KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
            KBEngine.Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
            KBEngine.Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
            KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
            KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
            KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");
            KBEngine.Event.registerOut("Loginapp_importClientMessages", this, "Loginapp_importClientMessages");
            KBEngine.Event.registerOut("Baseapp_importClientMessages", this, "Baseapp_importClientMessages");
            KBEngine.Event.registerOut("Baseapp_importClientEntityDef", this, "Baseapp_importClientEntityDef");
        }
        public void onCreateAccountResult(UInt16 retcode, byte[] datas)
        {
            if (retcode != 0)
            {
                UIcommon.inst.err("createAccount is error! err=" + KBEngineApp.app.serverErr(retcode));
                return;
            }

            if (KBEngineApp.validEmail(stringAccount))
            {
                UIcommon.inst.info("createAccount is successfully, Please activate your Email!");
            }
            else
            {
                UIcommon.inst.info("createAccount is successfully!");
            }
        }

        public void onLoginFailed(UInt16 failedcode)
        {
            if (failedcode == 20)
            {
                UIcommon.inst.err("login is failed, err=" + KBEngineApp.app.serverErr(failedcode) + ", " + System.Text.Encoding.ASCII.GetString(KBEngineApp.app.serverdatas()));
            }
            else
            {
                UIcommon.inst.err("login is failed, err=" + KBEngineApp.app.serverErr(failedcode));
            }
        }

        public void onVersionNotMatch(string verInfo, string serVerInfo)
        {
            UIcommon.inst.err("");
        }
        public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
        {
            UIcommon.inst.err("");
        }

        public void onLoginBaseappFailed(UInt16 failedcode)
        {
            UIcommon.inst.err("loginBaseapp is failed, err=" + KBEngineApp.app.serverErr(failedcode));
        }

        public void onLoginSuccessfully(UInt64 rndUUID, Int32 eid, Account accountEntity)
        {

            UIcommon.inst.info("login is successfully!");
            //ui_state = 1;

            Application.LoadLevel("selavatars");
        }
        public void onLoginBaseapp()
        {
            UIcommon.inst.info("connect to loginBaseapp, please wait...");
        }

        public void Loginapp_importClientMessages()
        {
            UIcommon.inst.info("Loginapp_importClientMessages ...");
        }

        public void Baseapp_importClientMessages()
        {
            UIcommon.inst.info("Baseapp_importClientMessages ...");
        }

        public void Baseapp_importClientEntityDef()
        {
            UIcommon.inst.info("importClientEntityDef ...");
        }

    }

    public class Common
    {
        public Common()
        {
            // common
            KBEngine.Event.registerOut("onKicked", this, "onKicked");
            KBEngine.Event.registerOut("onDisableConnect", this, "onDisableConnect");
            KBEngine.Event.registerOut("onConnectStatus", this, "onConnectStatus");
        }
        public void onKicked(UInt16 failedcode)
        {
            UIcommon.inst.err("kick, disconnect!, reason=" + KBEngineApp.app.serverErr(failedcode));
            Application.LoadLevel("login");
            //ui_state = 0;
        }

        public void onDisableConnect()
        {

        }

        public void onConnectStatus(bool success)
        {
            if (!success)
                UIcommon.inst.err("connect(" + KBEngineApp.app.getInitArgs().ip + ":" + KBEngineApp.app.getInitArgs().port + ") is error!");
            else
                UIcommon.inst.info("connect successfully, please wait...");
        }
    }

    // Use this for initialization
    void Start()
    {
        installEvents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        inst = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    void installEvents()
    {
        sel_avatars = new SelAvatars();
        login = new Login();
        login.InstallEvents();
        sel_avatars = new SelAvatars();
    }
    
}
