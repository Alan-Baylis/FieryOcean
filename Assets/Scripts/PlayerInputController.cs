using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using Apex;
using Apex.Units;
using UnityEngine.UI;
using Apex.WorldGeometry;
using Apex.Services;
using System;

public sealed class PlayerInputController : MonoBehaviour {
    public enum speedTypes { FullSpeed = 0, HalfSpeed, Stop, Reversal }
    enum InputActions { accelerate, firing, selectTurret }

    public Dictionary<speedTypes, float> speedMap;
    public UltimateJoystick joystick;

    [Header("Speed settings")]
    public float fullSpeed = 3f;
    public float halfSpeed = 1.5f;
    public float revers = -2f;
    public GameObject startPosition;
    public Slider slider;

    Dictionary<InputActions, Action<PlayerInputController>> _actionsDictonory;
    Queue<Action<PlayerInputController>> _actionQueue;

    public void SetActions(Action<PlayerInputController> accelerate, Action<PlayerInputController> firing, Action<PlayerInputController> selectTurret)
    {
        _actionsDictonory.Add(InputActions.accelerate, accelerate);
        _actionsDictonory.Add(InputActions.firing, firing);
        _actionsDictonory.Add(InputActions.selectTurret, selectTurret);
    }

    void Start()
    {      
    }
    
    void Awake()
    {
        _actionQueue = new Queue<Action<PlayerInputController>>();
        _actionsDictonory = new Dictionary<InputActions, Action<PlayerInputController>>();

        speedMap = new Dictionary<speedTypes, float>()
        {
            { speedTypes.FullSpeed, fullSpeed},
            { speedTypes.HalfSpeed, halfSpeed },
            { speedTypes.Stop, 0f },
            { speedTypes.Reversal, revers }
        };

        currentSpeed = speedTypes.Stop;
        _inputAccelerate = 0;
        _IsSpeedChanged = false;
    }

    private void InputSpeedEventHandler(speedTypes inputSpeed)
    {
        _IsSpeedChanged = false;

        if (inputSpeed != currentSpeed) {
            _inputAccelerate = speedMap[inputSpeed];
            currentSpeed = inputSpeed;
            _actionQueue.Enqueue(_actionsDictonory[InputActions.accelerate]);
        }
    }

    public Queue<Action<PlayerInputController>> GetQueue()  {  return _actionQueue;  }

    private bool _IsSpeedChanged;
    public bool IsSpeedChanged { get { return _IsSpeedChanged; } }
    private speedTypes currentSpeed;
    private float _inputAccelerate;
    public float accelerate { get { return _inputAccelerate; } }
    public void FullSpeed()
    {
        if (currentSpeed == speedTypes.HalfSpeed) InputSpeedEventHandler(speedTypes.FullSpeed);
    }

    public void HalfSpeed()
    {
        if (currentSpeed == speedTypes.FullSpeed || currentSpeed == speedTypes.Stop)  InputSpeedEventHandler(speedTypes.HalfSpeed);
    }

    public void Stop()
    {
        // if (currentSpeed != speedTypes.FullSpeed )
        InputSpeedEventHandler(speedTypes.Stop);
    }

    public void Reversal()
    {
        if (currentSpeed == speedTypes.Stop) InputSpeedEventHandler(speedTypes.Reversal);
    }

    public Vector3 Position()
    {
        MeshRenderer mr = startPosition.GetComponent<MeshRenderer>();

        float distance =Vector3.Distance( mr.bounds.max , mr.bounds.min);
        return startPosition.transform.position + new Vector3(-distance, 0f, distance);
    }

    //public bool fire = false;

    public void Fire()
    {
        _actionQueue.Enqueue(_actionsDictonory[InputActions.firing]);
    }

    //public bool isfire()
    //{
    //    if (fire) {
    //        fire = false;
    //        return true;
    //    }
    //    else
    //        return false;
    //}

    //void SetDestination(Vector3 destination, bool append)
    //{
    //    RaycastHit hit;
    //    if (Apex.Services.UnityServices.mainCamera.ScreenToLayerHit(destination, Layers.terrain, 1000.0f, out hit))
    //    {
    //        var destinationBlock = hit.collider.GetComponent<InvalidDestinationComponent>();
    //        if (destinationBlock != null)
    //        {
    //            if (destinationBlock.entireTransform)
    //            {
    //                return;
    //            }

    //            if (destinationBlock.onlySubArea.Contains(hit.point))
    //            {
    //                return;
    //            }
    //        }

           
    //    }
    //}

}
