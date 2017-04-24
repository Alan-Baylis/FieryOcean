using UnityEngine;
public interface IPlayerController : IViewController {
    Rigidbody rigidbody { get; set; }
    Transform transform { get; set; }
    ShipDirectional shipDirectional { get; set; }
  
}


public class PlayerViewController : ViewController, IPlayerController {
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
        set{ShipDirectional shipDirectional = GetComponent<ShipDirectional>();shipDirectional=value;}
        get{ return GetComponent<ShipDirectional>();  }
    }
}
