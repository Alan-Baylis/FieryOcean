using UnityEngine;
using System.Collections;
using System;

namespace Forge3D
{
    public class F3DTurret : MonoBehaviour
    {
        [HideInInspector]
        public bool destroyIt = false;

        public GameObject MountOrigin;
        public GameObject Mount;
        public GameObject SwivelOrigin;
        public GameObject Swivel;

        private Vector3 defaultDir;
        private Quaternion defaultRot;

        private Transform headTransformOrigin;
        private Transform headTransform;
        private Transform barrelTransformOrigin;
        private Transform barrelTransform;

        public float HeadingTrackingSpeed = 2f;
        public float ElevationTrackingSpeed = 2f;

        private Vector3 targetPos;
        [HideInInspector]
        public Vector3 headingVetor;
        [HideInInspector]
        public float curHeadingAngle = 0f;
        [HideInInspector]
        public float curElevationAngle = 0f;

        public Transform transformFire;
        public float step = 20f;
        public Vector2 HeadingLimit;
        public Vector2 ElevationLimit;
        public float barrelHeightAboveOcean;

        public bool smoothControlling = false;

        public bool DebugDraw = false;
        private bool fullAccess = false;
        public Animator[] Animators;

        public Transform anchorFire;
        private ThrowSimulation bulletCalculations;

        void Awake()
        {
            bulletCalculations = GetComponent<ThrowSimulation>();

            headTransformOrigin = SwivelOrigin.GetComponent<Transform>();
            headTransform = Swivel.GetComponent<Transform>();
            barrelTransformOrigin = MountOrigin.GetComponent<Transform>();
            barrelTransform = Mount.GetComponent<Transform>();
        }

        public void PlayAnimation()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetTrigger("FireTrigger");
        }

        public void PlayAnimationLoop()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetBool("FireLoopBool", true);
        }

        public void StopAnimation()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetBool("FireLoopBool", false);
        }

        // Use this for initialization
        void Start()
        {
            targetPos = headTransform.transform.position + headTransform.transform.forward * 100f;
            defaultDir = Swivel.transform.forward;
            defaultRot = Quaternion.FromToRotation(transform.forward, defaultDir);
            //defaultRot = Quaternion.FromToRotation(transform.forward, transform.forward);
            if (HeadingLimit.y - HeadingLimit.x >= 359.9f)
                fullAccess = true;

            StopAnimation();
        }

        // Autotrack
        public void SetNewTarget(Vector3 _targetPos)
        {
            targetPos = _targetPos;
        }

        // Angle between mount and target
        public float GetAngleToTarget()
        {
            return Vector3.Angle(Mount.transform.forward, targetPos - Mount.transform.position);
        }

        public class CannonMath
        {
            private static float gravity = 10;
            public static Vector3 GetNeededBarrelDirectional(Transform barrelTransform, Transform swivel, Vector3 anchorFire, Vector3 targetPos, float distance, float velocity_exp2, float barrelHeightAboveOcean)
            {
                float sin2Alpha = distance * gravity / velocity_exp2;
                float angleOfSinInDegrees = (Mathf.Asin(sin2Alpha) * Mathf.Rad2Deg) / 2;
                // Vector from barrel to target
                Vector3 elevationVector = Vector3.Normalize(F3DMath.ProjectVectorOnPlane(swivel.right, targetPos - barrelTransform.position));
                // Draw
                Debug.DrawLine(elevationVector, elevationVector * 150, Color.green);
                // Angel of slope
                float _elevationAngle = F3DMath.SignedVectorAngle(swivel.forward, elevationVector, swivel.right);
                //Calc barrel angel
                float angel = CalcFireAngel(_elevationAngle, velocity_exp2, distance, targetPos.y - barrelHeightAboveOcean);

                Vector3 v = Quaternion.AngleAxis(-angel, swivel.right) * anchorFire;
                //Draw
                Debug.DrawLine(v, v * 200, Color.magenta);

                return v;
            }
            private static float CalcFireAngel(float angel, float velocityExp2, float distance, float height)
            {
                float _b = -distance;
                float a = CalcA(angel, velocityExp2, distance);
                float c = (gravity * distance * distance) / (2 * velocityExp2) + height;

                float D = CalcDiscriminant(_b, a, c);
                //float tabAlpha1 = (float)((-_b + Math.Sqrt(D)) / (2f * a));
                float tabAlpha2 = (float)((-_b - Math.Sqrt(D)) / (2f * a));
                //float angel1 = Mathf.Atan(tabAlpha1) * Mathf.Rad2Deg;
                return Mathf.Atan(tabAlpha2) * Mathf.Rad2Deg;
            }

            private static float CalcA(float angel, float velocity, float distance) { return (gravity * distance * distance) / (2f * velocity); }
            private static float CalcDiscriminant(float b, float a, float c) { return b * b - 4f * a * c; }
        }


        void Update()
        {


            Debug.DrawRay(Vector3.zero, targetPos, Color.black, 0.5f);

            Ray r = new Ray();

            if (!smoothControlling)
            {
                if (barrelTransform != null)  {
                    NoSmoothRotateController(barrelTransformOrigin, barrelTransform, headTransformOrigin, headTransform);
                }
            }
            else
            {
                SmoothRotateController();
            }

            // Scene debug draw
            if (DebugDraw)
            {
                Debug.DrawLine(barrelTransform.position, barrelTransform.position + barrelTransform.forward * Vector3.Distance(barrelTransform.position, targetPos), Color.red);
                Vector3 relative = transformFire.InverseTransformDirection(0, 0, 1);
            }
        }

        private void NoSmoothRotateController(Transform barrelOrigin, Transform barrel, Transform headOrigin, Transform head)
        {
            /////// Heading
            headingVetor = Vector3.Normalize(F3DMath.ProjectVectorOnPlane(head.up, targetPos - head.position));
            float headingAngle = F3DMath.SignedVectorAngle(head.forward, headingVetor, head.up);
            float turretDefaultToTargetAngle = F3DMath.SignedVectorAngle(defaultRot * head.forward, headingVetor, head.up);
            float turretHeading = F3DMath.SignedVectorAngle(defaultRot * head.forward, head.forward, head.up);

            float headingStep = HeadingTrackingSpeed * Time.deltaTime;

            // Heading step and correction
            // Full rotation
            if (HeadingLimit.x <= -180f && HeadingLimit.y >= 180f)
                headingStep *= Mathf.Sign(headingAngle);
            else // Limited rotation
                headingStep *= Mathf.Sign(turretDefaultToTargetAngle - turretHeading);

            // Hard stop on reach no overshooting
            if (Mathf.Abs(headingStep) > Mathf.Abs(headingAngle))
                headingStep = headingAngle;

            // Heading limits
            if (curHeadingAngle + headingStep > HeadingLimit.x && curHeadingAngle + headingStep < HeadingLimit.y || HeadingLimit.x <= -180f && HeadingLimit.y >= 180f || fullAccess)
            {
                //ProjectileHelper.ComputeElevationToHitTargetWithSpeed(0, targetPos - headTransform.position, -9.8f, 10f, false, out headAngel);

                curHeadingAngle += headingStep;
                head.rotation = head.rotation * Quaternion.Euler(0f, headingStep, 0f);
                headOrigin.rotation = headOrigin.rotation * Quaternion.Euler(-headingStep, 0f, 0f);
                //headTransformOrigin.rotation = headTransformOrigin.rotation * Quaternion.Euler(headAngel, 0f, 0f);
            }

            /////// Elevation
            Vector3 tmp = targetPos - barrel.position;
            //Debug.DrawLine(tmp, tmp * 100, Color.blue);

            float target_Distance = Vector3.Distance(anchorFire.position, targetPos);

            if (ThrowSimulation.maxDistance > target_Distance)
            {
                //get barrel vector to fire tafget
                Vector3 elevationVector = CannonMath.GetNeededBarrelDirectional(barrel, head, head.forward, targetPos, target_Distance, bulletCalculations.projectile_Velocity_exp2, barrelHeightAboveOcean);

                // determine directional of rotation on angel by sign
                float _elevationAngle = F3DMath.SignedVectorAngle(barrel.forward, elevationVector, head.right);

                // Elevation step and correction
                float elevationStep = Mathf.Sign(_elevationAngle) * ElevationTrackingSpeed * Time.deltaTime;

                if (Mathf.Abs(elevationStep) > Mathf.Abs(_elevationAngle))
                    elevationStep = _elevationAngle;

                // Elevation limits
                if (curElevationAngle + elevationStep < ElevationLimit.y && curElevationAngle + elevationStep > ElevationLimit.x)
                {
                    curElevationAngle += elevationStep;
                    barrelOrigin.rotation = barrelOrigin.rotation * Quaternion.Euler(0f, 0f, elevationStep);
                    barrel.rotation = barrel.rotation * Quaternion.Euler(elevationStep, 0f, 0f);
                }
            }

        }

        private void SmoothRotateController()
        {
            Transform barrelX = barrelTransform;
            Transform barrelY = Swivel.transform;

            //finding position for turning just for X axis (down-up)
            Vector3 targetX = targetPos - barrelX.transform.position;
            Quaternion targetRotationX = Quaternion.LookRotation(targetX, headTransform.up);

            barrelX.transform.rotation = Quaternion.Slerp(barrelX.transform.rotation, targetRotationX, HeadingTrackingSpeed * Time.deltaTime);
            barrelX.transform.localEulerAngles = new Vector3(barrelX.transform.localEulerAngles.x, 0f, 0f);

            //checking for turning up too much
            if (barrelX.transform.localEulerAngles.x >= 180f && barrelX.transform.localEulerAngles.x < (360f - ElevationLimit.y))
            {
                barrelX.transform.localEulerAngles = new Vector3(360f - ElevationLimit.y, 0f, 0f);
            }

            //down
            else if (barrelX.transform.localEulerAngles.x < 180f && barrelX.transform.localEulerAngles.x > -ElevationLimit.x)
            {
                barrelX.transform.localEulerAngles = new Vector3(-ElevationLimit.x, 0f, 0f);
            }

            //finding position for turning just for Y axis
            Vector3 targetY = targetPos;
            targetY.y = barrelY.position.y;

            Quaternion targetRotationY = Quaternion.LookRotation(targetY - barrelY.position, barrelY.transform.up);

            barrelY.transform.rotation = Quaternion.Slerp(barrelY.transform.rotation, targetRotationY, ElevationTrackingSpeed * Time.deltaTime);
            barrelY.transform.localEulerAngles = new Vector3(0f, barrelY.transform.localEulerAngles.y, 0f);

            if (!fullAccess)
            {
                //checking for turning left
                if (barrelY.transform.localEulerAngles.y >= 180f && barrelY.transform.localEulerAngles.y < (360f - HeadingLimit.y))
                {
                    barrelY.transform.localEulerAngles = new Vector3(0f, 360f - HeadingLimit.y, 0f);
                }

                //right
                else if (barrelY.transform.localEulerAngles.y < 180f && barrelY.transform.localEulerAngles.y > -HeadingLimit.x)
                {
                    barrelY.transform.localEulerAngles = new Vector3(0f, -HeadingLimit.x, 0f);
                }
            }
        }
    }
}