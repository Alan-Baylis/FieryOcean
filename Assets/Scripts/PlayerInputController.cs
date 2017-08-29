using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using Apex;
using Apex.Units;
using UnityEngine.UI;

public sealed class PlayerInputController : MonoBehaviour {
    public enum speedTypes { FullSpeed = 0, HalfSpeed, Stop, Reversal }
    public Dictionary<speedTypes, float> speedMap;
    public UltimateJoystick joystick;

    [Header("Speed settings")]
    public float fullSpeed = 3f;
    public float halfSpeed = 1.5f;
    public float revers = -2f;
    public Transform startPosition;
    public Slider slider;
    void Start()
    { }
    
    void Awake()
    {
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
            _IsSpeedChanged = true;
        }
    }

    private bool _IsSpeedChanged;
    public bool IsSpeedChanged { get { return _IsSpeedChanged; } }
    private speedTypes currentSpeed;
    private float _inputAccelerate;
    public float accelerate { get { return _inputAccelerate; } }
    public void FullSpeed()
    {
        if (currentSpeed == speedTypes.HalfSpeed)
            InputSpeedEventHandler(speedTypes.FullSpeed);
    }

    public void HalfSpeed()
    {
        if (currentSpeed == speedTypes.FullSpeed || currentSpeed == speedTypes.Stop)
            InputSpeedEventHandler(speedTypes.HalfSpeed);
    }

    public void Stop()
    {
        // if (currentSpeed != speedTypes.FullSpeed )
        InputSpeedEventHandler(speedTypes.Stop);
    }

    public void Reversal()
    {
        if (currentSpeed == speedTypes.Stop)
            InputSpeedEventHandler(speedTypes.Reversal);
    }

    public Vector3 Position()
    {
        return startPosition.position;
    }

    public bool fire = false;

    public void Fire()
    {
        fire = true; 
    }

    public bool IsFire()
    {
        if (fire) {
            fire = false;
            return true;
        }
        else
            return false;
    }


    
}
