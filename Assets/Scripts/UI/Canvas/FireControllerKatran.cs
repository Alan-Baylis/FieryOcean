using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class FireControllerKatran : MonoBehaviour {

    Dictionary<UInt32, bool> turnCheckdic;
    Action<Dictionary<UInt32, bool>,UInt32> select = (Dictionary<UInt32, bool> d, UInt32 id)=> { if (d[id]) d[id] = false; else d[id] = true; };

    void Awake()
    {
        turnCheckdic = new Dictionary<UInt32, bool>() { { 1, false }, {2, false } };
    }

    public UInt32 ForwardCannonId = 1;
    public UInt32 BackwardCannonId = 2;
    //public Int32 Board01CannonId = 3;
    //public Int32 Board02CannonId = 4;

    public void FireForward()
    {
        select(turnCheckdic, ForwardCannonId);
        Contexts.sharedInstance.input.CreateEntity().AddSelectTurret(ForwardCannonId, turnCheckdic[ForwardCannonId]);
    }

    public void FireBackward()
    {
        select(turnCheckdic, ForwardCannonId);
        Contexts.sharedInstance.input.CreateEntity().AddSelectTurret(BackwardCannonId, turnCheckdic[BackwardCannonId]);
    }

    //public void FireBoard01()
    //{

    //}

    //public void FireBoard02()
    //{

    //}
}
