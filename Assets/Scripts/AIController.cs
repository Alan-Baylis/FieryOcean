using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.PathFinding;
using Apex.Services;
using System;
using Apex;
using Apex.Units;

public class AIController : MonoBehaviour {
    private GameObject _go;
    // Update is called once per frame

    public void RecalculatePath(Vector3 pos)
    {
        //if(Vector3.Distance(lastPosition , target.position) > RecalculatePathDistance)
        this.GetUnitFacade().MoveTo(pos, false);
       // StartCoroutine(DoStuff());
    }

    /*IEnumerator DoStuff()
    {        
        yield return new WaitForSeconds(pathUpdateInterval);
        //do stuff
    }*/

    //private Vector3 lastPosition;
    private IUnitFacade unitFacade;
    private Apex.Input.InputController inputController = new Apex.Input.InputController();
    //public Transform target;

    [Header("Path settings")]
    [Tooltip("The amount of seconds after which the  enemy will check for update path to player (seconds).")]
    public float pathUpdateInterval = 1;

    [InspectorName("The delta for recalculation of a path")]
    public float RecalculatePathDistance=5;

    void Start()
    {
        //target.position = new Vector3(66.9f,7.31f,-40.48f);
        //lastPosition = target.position;
        unitFacade = this.GetUnitFacade();
        
        //MoveTo3(unitFacade);
        
        //this.GetUnitFacade().MoveTo(target.position, false);
    }

    //private void MoveTo1()
    //{
    //    var unit = this.GetUnitFacade();

    //    Action<PathResult> callback = (result) =>
    //    {
    //        if (!(result.status == PathingStatus.Complete || result.status == PathingStatus.CompletePartial))
    //            return;

    //        unit.MoveAlong(result.path);
    //    };

    //    var req = new CallbackPathRequest(callback)
    //    {
    //        from = unit.position,
    //        to = this.target.position,
    //        requesterProperties = unit,
    //        pathFinderOptions = unit.pathFinderOptions
    //    };
        
    //    GameServices.pathService.QueueRequest(req,0);
    //}

    //private void MoveTo2()
    //{
    //    var unit = this.GetUnitFacade();
    //    Path p = new Path(target.position);
    //    unit.MoveAlong(p);
    //}

    //private void MoveTo3(IUnitFacade unitFacade)
    //{
    //    unitFacade.MoveTo(target.position, false);
    //}
}
