using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.PathFinding;
using Apex.Services;
using System;
using Apex;
using Apex.Units;

public class ComputerToPlayerPathRequest : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
       /*if(Vector3.Distance(lastPosition , target.position)>5)
            this.GetUnitFacade().MoveTo(target.position, false);

        StartCoroutine(DoStuff());*/
    }

    IEnumerator DoStuff()
    {        
        yield return new WaitForSeconds(pathUpdateInterval);
        //do stuff
    }

    private Vector3 lastPosition;
    public Transform target;
    private IUnitFacade unitFacade;
    private Apex.Input.InputController inputController = new Apex.Input.InputController();

    [Tooltip("The amount of seconds after which the enemy will update path to player.")]
    public float pathUpdateInterval = 1;

    private void Start()
    {
        /*
            lastPosition = target.position;
            unitFacade = this.GetUnitFacade();
        
            MoveTo3(unitFacade);
        */

        this.GetUnitFacade().MoveTo(target.position, false);
    }

  

    private void MoveTo1()
    {
        var unit = this.GetUnitFacade();

        Action<PathResult> callback = (result) =>
        {
            //Here we treat partial completion the same as full completion, you may want to treat partials differently
            if (!(result.status == PathingStatus.Complete || result.status == PathingStatus.CompletePartial))
            {
                return;
            }

            unit.MoveAlong(result.path);
        };

        var req = new CallbackPathRequest(callback)
        {
            from = unit.position,
            to = this.target.position,
            requesterProperties = unit,
            pathFinderOptions = unit.pathFinderOptions
        };
        
        GameServices.pathService.QueueRequest(req,0);

        

        // System.Threading.Thread.Sleep(1000);
    }

    private void MoveTo2()
    {
        var unit = this.GetUnitFacade();
        Path p = new Path(target.position);
        unit.MoveAlong(p);
    }

    private void MoveTo3(IUnitFacade unitFacade)
    {
        this.GetUnitFacade().MoveTo(target.position, false);
    }
}
