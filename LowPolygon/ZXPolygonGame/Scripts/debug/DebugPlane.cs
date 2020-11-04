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
            if (null != planeWingPlanes && planeWingPlanes.Length > 0) {
                foreach (GameObject wingPlane in planeWingPlanes) {
                    if (null == wingPlane) continue;
                    
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawRay(new Ray(wingPlane.transform.position, wingPlane.transform.right));
                    Gizmos.DrawRay(new Ray(wingPlane.transform.position, wingPlane.transform.forward));

                    Transform axle = wingPlane.transform.Find("Axle");
                    if (null != axle) {
                        Gizmos.color = Color.green;
                        Gizmos.DrawRay(new Ray(axle.transform.position, axle.transform.right));
                    }
                }
            }
        }
    }
}