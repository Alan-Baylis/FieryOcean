using UnityEngine;
using System.Collections.Generic;

public class Movement1
{
    float angel = 0.1f;
    float joystickMistake = 0.1f;
    float applyForseMistake = 0.5f;
    TransformSlowAccelerate slowAccelerate;
    TransformRotateSlow trRotateSlow;
    Dictionary<PlayerInputController.speedTypes, float> speedMap;

    public Movement1(Dictionary<PlayerInputController.speedTypes, float> speedMap)
    {
        slowAccelerate = new TransformSlowAccelerate();
        this.speedMap = speedMap;
    }

    public void Move(Vector3 shipDirect, Rigidbody rb, Vector2 jpystickPos, float lastAc, Transform tr)
    {
        if (lastAc == speedMap[PlayerInputController.speedTypes.HalfSpeed])
        {
            PlayerRotation(shipDirect, rb, jpystickPos);

            float nextAc = slowAccelerate.CalcNextAccelerate(lastAc, rb.velocity.magnitude);
            rb.velocity = shipDirect.normalized * nextAc; // TODO direction not correct
        }

        if (lastAc == speedMap[PlayerInputController.speedTypes.Stop])
        {
            //float delta = rb.velocity.magnitude;

            //Vector3 vector = tr.position;

            // vector.x *= -1;
            // vector.z *= -1;
            //vector.y = 0;

            if (Vector3.Angle(rb.velocity, shipDirect) > 90)
                rb.velocity = shipDirect.normalized * slowAccelerate.CalcNextAccelerate(lastAc, -rb.velocity.magnitude);
            else
                rb.velocity = shipDirect.normalized * slowAccelerate.CalcNextAccelerate(lastAc, rb.velocity.magnitude);
        }

        if (lastAc == speedMap[PlayerInputController.speedTypes.Reversal])
        {
            /*float delta = rb.velocity.magnitude - speedMap[PlayerInputController.speedTypes.Reversal];

            if (Mathf.Abs(delta) > applyForseMistake)
                rb.AddForce(shipDirect.normalized * delta);
                */

            PlayerRotation(shipDirect, rb, jpystickPos);

            float nextAc = slowAccelerate.CalcNextAccelerate(lastAc, -rb.velocity.magnitude);
            rb.velocity = shipDirect.normalized * nextAc; // TODO direction not correct
        }
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
