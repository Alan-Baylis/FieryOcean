using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonParams
{
    public CannonParams(uint ID, Vector3 target)
    {
        this.ID = ID;
        this.target = target;
    }

    public uint ID;
    public Vector3 target;

}
