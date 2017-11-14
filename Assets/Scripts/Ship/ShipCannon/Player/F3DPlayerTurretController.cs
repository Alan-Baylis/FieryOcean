using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

namespace Forge3D
{
    public class F3DPlayerTurretController : MonoBehaviour
    {
        public F3DTurret turret;
        public Transform aimingPointTransform;
        public Transform ocean;
        public LayerMask[] layersToHit;

        private const int SIZE = 2;
        private RaycastHit[] hit;// = new RaycastHit[2];
        private RaycastHit curRay;

        private Func<bool[], bool> ckLayers;
        private Action<RaycastHit[]> SetEmptyRays;
        private Func<RaycastHit[], RaycastHit> GetHighestRaycast;

        private bool[] chkLayer;

        void Update()
        {
            GetPointOnGroud();
        }
        
        void OnValidate()
        {
            if (layersToHit.Length != SIZE)
            {
                Array.Resize(ref layersToHit, SIZE);
                Array.Resize(ref hit, SIZE);
                Array.Resize(ref chkLayer, SIZE);
            }
        }

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
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

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
                    aimingPointTransform.position = new Vector3(curRay.point.x,50,curRay.point.z); //TODO

                    Debug.Log("Ray: " + curRay.point.ToString());
                }
            }
        }
    }
}