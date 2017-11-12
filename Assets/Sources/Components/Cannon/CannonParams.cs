using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonParams
{
    public CannonParams(uint ID)
    {
        this.ID = ID;
    }

    public uint ID;
    public Vector3 target;
    public Vector3 shipPosition;

}
