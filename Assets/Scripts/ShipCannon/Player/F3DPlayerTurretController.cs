using UnityEngine;
using System.Collections;

namespace Forge3D
{
    public class F3DPlayerTurretController : MonoBehaviour
    {
        RaycastHit hitInfo; // Raycast structure
        public F3DTurret turret;
        bool isFiring; // Is turret currently in firing state
        //public F3DFXController fxController;
        public Transform aimingPointTransform;
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

        void GetPointOnGroud()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Debug.DrawRay(ray.origin, ray.direction * 400f, Color.red);
                //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 400f);
                //Gizmos.DrawRay(ray);
                int layer = LayerMask.NameToLayer("Terrain");

                if (Physics.Raycast(ray, out hit, 5000, LayerMask.GetMask("Terrain")))
                {
                    turret.SetNewTarget(hit.point);
                    aimingPointTransform.position = hit.point;
                    Debug.Log(hit.point);
                }
            }
        }
    }
}