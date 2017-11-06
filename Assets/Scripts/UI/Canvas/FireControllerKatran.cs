using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class FireControllerKatran : MonoBehaviour {

    Dictionary<int, bool> turnCheckdic;
    Action<Dictionary<int, bool>,int> select = (Dictionary<int, bool> d, int id)=> { if (d[id]) d[id] = false; else d[id] = true; };

    void Awake()
    {
        turnCheckdic = new Dictionary<int, bool>() { { 1, false }, {2, false } };
    }

    public Int32 ForwardCannonId = 1;
    public Int32 BackwardCannonId = 2;
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
