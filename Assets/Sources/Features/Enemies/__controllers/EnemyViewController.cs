﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IEnemyController : IViewController
{
    Rigidbody rigidbody { get; }
    Transform transform { get; }
    ShipDirectional shipDirectional { get; }
    AIController AIController { get; }

}

public class EnemyViewController : ViewController, IEnemyController
{
    new public Rigidbody rigidbody { get { return GetComponent<Rigidbody>(); }  }

    new public Transform transform { get { return GetComponent<Transform>(); }  }

    public ShipDirectional shipDirectional { get { return GetComponent<ShipDirectional>(); }  }

    public AIController AIController { get { return GetComponent<AIController>();  }  }
}