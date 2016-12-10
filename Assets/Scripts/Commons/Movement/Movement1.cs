using UnityEngine;
using System.Collections.Generic;

public class Movement1
{
    float angel = 0.1f;
    float joystickMistake = 0.1f;
    float applyForseMistake = 0.5f;
    TransformSlowAccelerate slowAccelerate;
    TransformRotateSlow trRotateSlow;
    Dictionary<PlayerControlController.speedTypes, float> speedMap;

    public Movement1(Dictionary<PlayerControlController.speedTypes, float> speedMap)
    {
        slowAccelerate = new TransformSlowAccelerate();
        this.speedMap = speedMap;
    }

    public void Move(Vector3 shipDirect, Rigidbody rb, Vector2 jpystickPos, float lastAc, Transform tr)
    {
        if (lastAc == speedMap[PlayerControlController.speedTypes.HalfSpeed])
        {
            // float delta = _speedMap[PlayerControlController.speedTypes.HalfSpeed] - player.playerView.controller.rigidbody.velocity.magnitude;

            /* if (Mathf.Abs(delta) > applyForseMistake)
             {
                 direct.y = 0;

                 if (delta > 0)
                     player.playerView.controller.rigidbody.AddForce(direct.normalized*0.01f);
                 else
                     player.playerView.controller.rigidbody.AddForce(player.playerView.controller.shipDirectional.GetShipDirectional().normalized * -0.01f);
             }
             */
            
           PlayerRotation(shipDirect, rb, jpystickPos);

            float nextAc = slowAccelerate.CalcNextAccelerate(lastAc, rb.velocity.magnitude);
            rb.velocity = -shipDirect.normalized * nextAc; // TODO direction not correct
        }

        if (lastAc == speedMap[PlayerControlController.speedTypes.Stop])
        {
            float delta = rb.velocity.magnitude;

            Vector3 vector = tr.position;

            vector.x *= -1;
            vector.z *= -1;
            vector.y = 0;

            if (Mathf.Abs(delta) > applyForseMistake)
                rb.AddForce(-rb.velocity);
        }

        if (lastAc == speedMap[PlayerControlController.speedTypes.Reversal])
        {
            float delta = rb.velocity.magnitude - speedMap[PlayerControlController.speedTypes.Reversal];

            if (Mathf.Abs(delta) > applyForseMistake)
                rb.AddForce(shipDirect.normalized * delta);
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
        float curAcSelected; // acceleration
        float curAcApplied;
        float scaleCoefficient = 2f;

        public float CalcNextAccelerate(float curAcSelected, float velocity)
        {
            if (curAcSelected != this.curAcSelected)
                Reset(curAcSelected, velocity);

            return CalcNextAccelerateNT(velocity);
        }

        float CalcNextAccelerateNT(float velocity)
        {
            if (velocity < curAcSelected)
                curAcApplied = curAcApplied + Time.deltaTime * scaleCoefficient;
            if(velocity>curAcApplied)
                curAcApplied = curAcApplied - Time.deltaTime * scaleCoefficient;

            return curAcApplied;
        }

        void Reset(float ac, float velocity)
        {
            curAcSelected = ac;
            curAcApplied = velocity;
        }
    }
}
