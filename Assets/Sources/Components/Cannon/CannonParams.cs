using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonParams
{
    public CannonParams(uint ID, Vector3 target, Vector3 shipPosition)
    {
        this.ID = ID;
        this.target = target;
        this.shipPosition = shipPosition;
    }

    public uint ID;
    public Vector3 target;
    public Vector3 shipPosition;

}
