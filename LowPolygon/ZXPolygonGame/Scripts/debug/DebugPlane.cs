using System;
using UnityEngine;

namespace zxVehicle.debug
{
    public class DebugPlane : MonoBehaviour
    {
        public GameObject[] planeWingPlanes;

        private void OnDrawGizmos()
        {
            // 飞机方向
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
            
            // 查看翅膀
            // if (null != planeWingPlanes && planeWingPlanes.Length > 0) {
            //     foreach (GameObject wingPlane in planeWingPlanes) {
            //         if (null == wingPlane) continue;
            //         
            //         Gizmos.color = Color.yellow;
            //         Gizmos.DrawRay(new Ray(wingPlane.transform.position, wingPlane.transform.right));
            //         Gizmos.DrawRay(new Ray(wingPlane.transform.position, wingPlane.transform.forward));
            //     }
            // }

            Gizmos.color = Color.green;
            for (Int32 i = 0; i < 3; i++) {
                Transform axle = transform.Find("Wings/Right/Axle" + (i + 1));
                if (null != axle) {
                    Gizmos.DrawRay(new Ray(axle.transform.position, axle.transform.right));
                }
            }
            for (Int32 i = 0; i < 3; i++) {
                Transform axle = transform.Find("Wings/Left/Axle" + (i + 1));
                if (null != axle) {
                    Gizmos.DrawRay(new Ray(axle.transform.position, axle.transform.right));
                }
            }
        }
    }
}