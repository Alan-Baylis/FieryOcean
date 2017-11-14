using System;
using Forge3D;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPlayerController : IViewController {
    Rigidbody rigidbody { get; set; }
    Transform transform { get; set; }
    ShipDirectional shipDirectional { get; set; }
    void AddTurret(PlayerViewController.TurretShip t);

    PlayerViewController.TurretShip GetTurret(UInt32 ID);
    Dictionary<UInt32, PlayerViewController.TurretShip>.ValueCollection GetTurrets();
}

public class PlayerViewController : ViewController, IPlayerController {
    private Dictionary<UInt32, TurretShip> turrets;
    public PlayerViewController()
    {
        turrets = new Dictionary<UInt32, TurretShip>();
    }

    public class TurretShip
    {
        public F3DTurret turret { get; private set; }
        public TrajectoryPredictor3D trajectoryPredictor { get; private set; }
        public UInt32 ID { private set; get; }

        public TurretShip(UInt32 Id, F3DTurret turret, TrajectoryPredictor3D trajectoryPredictor)
        {
            this.ID = Id;
            this.turret = turret;
            this.trajectoryPredictor = trajectoryPredictor;
        }

        public void Update(out CannonParams p)
        {
            turret.UpdateCustom(out p);
            trajectoryPredictor.TrajectoryPredict();
        }

        public void Update()
        {
            CannonParams p;
            turret.UpdateCustom(out p);
            trajectoryPredictor.TrajectoryPredict();
        }

        public bool trajectoryEnable { get { return turret.IsAiming; } set { turret.IsAiming = value; } }
    }

    public virtual Rigidbody rigidbody
    {
        set  { Rigidbody rb = GetComponent<Rigidbody>();  rb = value; }
        get { return GetComponent<Rigidbody>(); }
    }

    public virtual Transform transform
    {
        set { Transform tr = GetComponent<Transform>(); tr = value; }
        get { return GetComponent<Transform>();  }
    }

    public virtual ShipDirectional shipDirectional
    {
        set { ShipDirectional shipDirectional = GetComponent<ShipDirectional>(); shipDirectional = value; }
        get { return GetComponent<ShipDirectional>();  }
    }

    public void AddTurret(TurretShip t)
    {
        turrets[t.ID]= t;
    }

    public TurretShip GetTurret(UInt32 ID)
    {
        return turrets[ID];
    }

    public Dictionary<UInt32, TurretShip>.ValueCollection GetTurrets()
    {
        return turrets.Values;
    }
}
