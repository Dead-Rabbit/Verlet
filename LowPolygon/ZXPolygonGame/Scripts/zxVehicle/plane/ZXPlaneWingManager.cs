using System.Collections.Generic;
using UnityEngine;

namespace zxVehicle.plane
{
    public class ZXPlaneWingManager : MonoBehaviour
    {
        private struct WingPlateInfo
        {
            public Transform plate;
            public Vector3 plateAxle;
            public WingPlateInfo(Transform plate, Vector3 plateAxle)
            {
                this.plate = plate;
                this.plateAxle = plateAxle;
            }
        }
        
        // 旋转板
        public GameObject[] leftPlates;
        public GameObject[] rightPlates;
        private List<WingPlateInfo> _leftWingPlateInfos = new List<WingPlateInfo>();
        private List<WingPlateInfo> _rightWingPlateInfos = new List<WingPlateInfo>();

        void Awake()
        {
            foreach (GameObject leftPlate in leftPlates) {
                if (null == leftPlate) continue;
                
                Transform axle = leftPlate.transform.Find("Axle");
                if (null == axle) continue;

                _leftWingPlateInfos.Add(new WingPlateInfo(leftPlate.transform, -axle.transform.right));
            }
            
            foreach (GameObject rightPlate in rightPlates) {
                if (null == rightPlate) continue;
                
                Transform axle = rightPlate.transform.Find("Axle");
                if (null == axle) continue;
                
                _rightWingPlateInfos.Add(new WingPlateInfo(rightPlate.transform, axle.transform.right));
            }
        }

        public void Run(float _InputHorizontal, float _InputVert)
        {
            #region 输入与控制

            foreach (WingPlateInfo plateInfo in _leftWingPlateInfos) {
                plateInfo.plate.Rotate(plateInfo.plateAxle, _InputHorizontal * 10);
            }
            
            #endregion

            #region 飞行效果

            

            #endregion
        }
    }
}