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

    List<PlayerViewController.TurretShip> GetTurrets();
}

public class PlayerViewController : ViewController, IPlayerController {
    private List<TurretShip> turrets;
    public PlayerViewController()
    {
        turrets = new List<TurretShip>();
    }

    public class TurretShip
    {
        public F3DTurret turret;
        public TrajectoryPredictor3D trajectoryPredictor;

        public void Update(out CannonParams p)
        {
            turret.UpdateCustom(out p);
            trajectoryPredictor.UpdateCustom();
        }
    }

    public virtual Rigidbody rigidbody
    {
        set
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb = value;
        }
        get { return GetComponent<Rigidbody>(); }
    }

    public virtual Transform transform
    {
        set { Transform tr = GetComponent<Transform>(); tr = value; }
        get { return GetComponent<Transform>();  }
    }

    public virtual ShipDirectional shipDirectional
    {
        set
        {
            ShipDirectional shipDirectional = GetComponent<ShipDirectional>();
            shipDirectional = value;
        }

        get { return GetComponent<ShipDirectional>();  }
    }

    public void AddTurret(TurretShip t)
    {
        turrets.Add(t);
    }

    public List<TurretShip> GetTurrets()
    {
        return turrets;
    }

}
