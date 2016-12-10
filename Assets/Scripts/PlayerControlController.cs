using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;


public class PlayerControlController : MonoBehaviour {

    public enum speedTypes{FullSpeed=0,HalfSpeed,Stop,Reversal }
    public Dictionary<speedTypes, float> speedMap;
    public UltimateJoystick joystick;
    void Start()
    {
       
    }
    void Awake()
    {
        speedMap = new Dictionary<speedTypes, float>();
        speedMap.Add(speedTypes.FullSpeed, 3f);
        speedMap.Add(speedTypes.HalfSpeed, 2f);
        speedMap.Add(speedTypes.Stop, 0f);
        speedMap.Add(speedTypes.Reversal, -2f);

        currentSpeedType = speedTypes.Stop;

    }
    public speedTypes currentSpeedType;

    public float inputAccelerate
    {
        get { return _inputAccelerate; }
        set { _inputAccelerate = value; }
    }

    private float _inputAccelerate = 0;

    public void FullSpeed()
    {
        if (currentSpeedType == speedTypes.Reversal || currentSpeedType == speedTypes.Stop || currentSpeedType == speedTypes.FullSpeed) 
            return; // Error

        currentSpeedType = speedTypes.FullSpeed;
        _inputAccelerate = speedMap[speedTypes.FullSpeed];
    }

    public void HalfSpeed()
    {
        if (currentSpeedType == speedTypes.Reversal || currentSpeedType == speedTypes.HalfSpeed )
            return; // Error
      
        _inputAccelerate = speedMap[speedTypes.HalfSpeed];
        currentSpeedType = speedTypes.HalfSpeed;
    }

    public void Stop()
    {
        if (currentSpeedType == speedTypes.FullSpeed || currentSpeedType == speedTypes.Stop)
            return;
       
        _inputAccelerate = -speedMap[speedTypes.Stop];
        currentSpeedType = speedTypes.Stop;
    }
}
