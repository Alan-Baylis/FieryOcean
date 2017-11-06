using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonParams
{
    public uint ID;
    public Vector3 target;
    public Vector3 shipPosition;
    public float firingAngel;
    public Transform fireAnchor;
    public Transform swivel;
    public float gravity;
    public float elapseTime;
    public float vX;
    public float vY;
    public float speed;

    public CannonParams(uint ID)
    {
        this.ID = ID;
    }

    protected CannonParams(CannonParams other)
    {
        this.ID = other.ID;
        this.shipPosition = other.shipPosition;
        this.target = other.target;
        this.firingAngel = other.firingAngel;
        this.fireAnchor = other.fireAnchor;
        this.gravity = other.gravity;
        this.elapseTime = other.elapseTime;
        this.vX = other.vX;
        this.vY = other.vY;
        this.speed = other.speed;
        this.swivel = other.swivel;
    }

    public CannonParams Copy()
    {
        return new CannonParams(this);
    }
}
