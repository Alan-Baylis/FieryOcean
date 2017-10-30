using UnityEngine;
using System.Collections;
using System;

namespace Forge3D
{
    public class F3DPlayerTurretController : MonoBehaviour
    {
        RaycastHit hitInfo; // Raycast structure
        public F3DTurret turret;
        bool isFiring; // Is turret currently in firing state
        //public F3DFXController fxController;
        public Transform aimingPointTransform;
        public Transform ocean;
      
        void Update()
        {
            GetPointOnGroud();
            //CheckForTurn();
            CheckForFire();
        }

        void CheckForFire()
        {
            // Fire turret
            if (!isFiring && Input.GetKeyDown(KeyCode.Mouse0))
            {
                isFiring = true;
                //fxController.Fire();
            }

            // Stop firing
            if (isFiring && Input.GetKeyUp(KeyCode.Mouse0))
            {
                isFiring = false;
                //fxController.Stop();
            }
        }

        void CheckForTurn()
        {
            // Construct a ray pointing from screen mouse position into world space
            if (Input.GetMouseButton(0))
            {
                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Raycast
                if (Physics.Raycast(cameraRay, out hitInfo, 500f))
                {
                    turret.SetNewTarget(hitInfo.point);
                    aimingPointTransform.position = hitInfo.point;
                    //Debug.Log(hitInfo.point);
                }
            }
        }

        private const int SIZE = 2;
        RaycastHit[] hit;// = new RaycastHit[2];
        RaycastHit curRay;
        void OnValidate()
        {
            if (layersToHit.Length != SIZE)
            {
                Array.Resize(ref layersToHit, SIZE);
                Array.Resize(ref hit, SIZE);
                Array.Resize(ref chkLayer, SIZE);
            }
        }

        Func<bool[] , bool> ckLayers;
        Action<RaycastHit[]> SetEmptyRays;
        Func<RaycastHit[], RaycastHit> GetHighestRaycast;

        //Layers that will stop the trajectory
        public LayerMask[] layersToHit;
        bool[] chkLayer;

        void Start()
        {
            hit = new RaycastHit[SIZE];
            chkLayer = new bool[SIZE];
            SetEmptyRays = (RaycastHit[] rayHit) => { for (int i=0;i<rayHit.Length;i++) { rayHit[i].point = Vector3.zero; } };
            ckLayers = (bool[] chkLayer) => { foreach (bool chk in chkLayer) { if (chk) { return true; } } return false; };
            GetHighestRaycast = (RaycastHit[] rayHit) => { RaycastHit outRay = new RaycastHit(); int index = 1; if (rayHit.Length > 0) { outRay = rayHit[0]; for (index=1; index < rayHit.Length; index++) { if (rayHit[index].point.y > outRay.point.y) outRay = rayHit[index]; } } /*Debug.Log( "Controller collider: " + outRay.collider.ToString());*/  return outRay; };
        }

        void GetPointOnGroud()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Debug.DrawRay(ray.origin, ray.direction * 400f, Color.red);
                //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 400f);
                //Gizmos.DrawRay(ray);

                SetEmptyRays(hit);

                for (int i = 0; i < chkLayer.Length; i++)
                {
                    chkLayer[i] = Physics.Raycast(ray, out hit[i], 5000, layersToHit[i]);
                }

                if (ckLayers(chkLayer))
                {
                    curRay = GetHighestRaycast(hit);
                    turret.SetNewTarget(curRay.point);
                    aimingPointTransform.position = curRay.point;
                    Debug.Log("Ray: " + curRay.point.ToString());
                }
            }
        }
    }
}