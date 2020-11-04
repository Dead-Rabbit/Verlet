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
            Gizmos.color = Color.yellow;
            if (null != planeWingPlanes && planeWingPlanes.Length > 0) {
                foreach (GameObject wingPlane in planeWingPlanes) {
                    if (null == wingPlane) continue;
                    Gizmos.DrawRay(new Ray(wingPlane.transform.position, wingPlane.transform.up));
                }
            }
        }
    }
}