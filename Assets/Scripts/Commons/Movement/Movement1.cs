using UnityEngine;
using System.Collections.Generic;

public class PlayerMovementController
{
    float angel = 0.1f;
    float joystickMistake = 0.1f;
    TransformSlowAccelerate slowAccelerate;
    Dictionary<PlayerInputController.speedTypes, float> speedMap;
    private float _masterY;
    public PlayerMovementController(Dictionary<PlayerInputController.speedTypes, float> speedMap, float masterY)
    {
        slowAccelerate = new TransformSlowAccelerate();
        this.speedMap = speedMap;
        this._masterY = masterY;
    }

    public void SetMasterY(float masterY)
    {
        this._masterY = masterY;
    }

    public Vector3 Move(Vector3 shipDirect, Rigidbody rb, Vector2 jpystickPos, float lastAc)
    {

        if (lastAc == speedMap[PlayerInputController.speedTypes.HalfSpeed])
        {
            PlayerRotation(shipDirect, rb, jpystickPos);

            float nextAc = slowAccelerate.CalcNextAccelerate(lastAc, rb.velocity.magnitude);
            rb.velocity = shipDirect.normalized * nextAc; 
        }

        if (lastAc == speedMap[PlayerInputController.speedTypes.Stop])
        {
            if (Vector3.Angle(rb.velocity, shipDirect) > 90)
                rb.velocity = shipDirect.normalized * slowAccelerate.CalcNextAccelerate(lastAc, -rb.velocity.magnitude);
            else
                rb.velocity = shipDirect.normalized * slowAccelerate.CalcNextAccelerate(lastAc, rb.velocity.magnitude);
        }

        if (lastAc == speedMap[PlayerInputController.speedTypes.Reversal])
        {
            PlayerRotation(shipDirect, rb, jpystickPos);

            float nextAc = slowAccelerate.CalcNextAccelerate(lastAc, -rb.velocity.magnitude);
            rb.velocity = shipDirect.normalized * nextAc;
        }

        rb.position = new Vector3(rb.position.x, _masterY, rb.position.z);
        return rb.position;
    }

    private void PlayerRotation(Vector3 shipDirect, Rigidbody rb, Vector2 jpystickPos)
    {
        float angel = Vector3.Angle(shipDirect, rb.velocity);

        if (angel > 90)
            RotatePlayer(jpystickPos, false, rb);
        else
            RotatePlayer(jpystickPos, true, rb);
    }

    private void RotatePlayer(Vector2 joystickPosition, bool invers, Rigidbody rb)
    {
        Debug.Log(joystickPosition.x);

        if (joystickPosition.x > joystickMistake)
            rb.rotation *= Quaternion.AngleAxis(angel, Vector3.up);

        if (joystickPosition.x < -joystickMistake)
            rb.rotation *= Quaternion.AngleAxis(angel, Vector3.down);
    }

    class TransformRotateSlow
    {
        float maxRotateAngel; 
        float curRotateApplied;
        float scaleCoefficient = 2f;
        float mistake = 0.1f;

        public TransformRotateSlow(float maxRotateAngel)
        {
            this.maxRotateAngel = maxRotateAngel;
        }

        public float Rotate(float joystickPosX)
        {
            if (joystickPosX == 0)
            {
                if (curRotateApplied > 0 + mistake)
                    curRotateApplied -= Time.deltaTime * scaleCoefficient;
                if (curRotateApplied < 0 - mistake)
                    curRotateApplied += Time.deltaTime * scaleCoefficient;
            }
            else
                curRotateApplied = maxRotateAngel;

            return curRotateApplied;
        }
    }

    class TransformSlowAccelerate
    {
        private float _reqiuredSpeed; // acceleration
        private float _currentSpeed;
        private float scaleCoefficient = 2f;

        public float CalcNextAccelerate(float requiredSpeed, float currentSpeed)
        {
            if (requiredSpeed != this._reqiuredSpeed)
                Reset(requiredSpeed, currentSpeed);

            return CalcNextAccelerateNT(currentSpeed);
        }

        float CalcNextAccelerateNT(float currentSpeed)
        {
            if (_reqiuredSpeed > 0) // forward
            {
                if (currentSpeed < _reqiuredSpeed)
                    _currentSpeed = _currentSpeed + Time.deltaTime * scaleCoefficient;
                if (currentSpeed > _reqiuredSpeed)
                    _currentSpeed = _currentSpeed - Time.deltaTime * scaleCoefficient;
            }

            if(_reqiuredSpeed < 0) // revers
            {
                if (currentSpeed > _reqiuredSpeed)
                    _currentSpeed -= Time.deltaTime * scaleCoefficient;
                if (currentSpeed < _reqiuredSpeed)
                    _currentSpeed += Time.deltaTime * scaleCoefficient;

            }

            if (_reqiuredSpeed == 0) // stop
            {
                if (currentSpeed > _reqiuredSpeed)
                    _currentSpeed -= Time.deltaTime * scaleCoefficient;
                if (currentSpeed < _reqiuredSpeed)
                    _currentSpeed += Time.deltaTime * scaleCoefficient;
            }

            return _currentSpeed;
        }

        void Reset(float requiredSpeed, float currentSpeed)
        {
            _reqiuredSpeed = requiredSpeed;
            _currentSpeed = currentSpeed;
        }
    }
}
