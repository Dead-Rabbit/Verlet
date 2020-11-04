using System;
using System.Collections.Generic;
using UnityEngine;

namespace zxVehicle.plane
{
    public class ZXPlaneWingManager : MonoBehaviour
    {
        private struct WingPlateInfo
        {
            public Transform plate;
            public Transform plateAxle;
            public WingPlateInfo(Transform plate, Transform plateAxle)
            {
                this.plate = plate;
                this.plateAxle = plateAxle;
            }
        }
        
        // 旋转板
        public GameObject[] leftPlates;
        public GameObject[] rightPlates;
        public GameObject[] leftPlateAxles;
        public GameObject[] rightPlateAxles;
        private List<WingPlateInfo> _leftWingPlateInfos = new List<WingPlateInfo>();
        private List<WingPlateInfo> _rightWingPlateInfos = new List<WingPlateInfo>();

        private float _preInputHorizontal = 0;

        void Awake()
        {
            for (Int32 i = 0; i < leftPlates.Length; i++) {
                GameObject plate = leftPlates[i];
                if (null == plate) continue;
                
                if (i >= leftPlateAxles.Length) continue;
                GameObject axle = leftPlateAxles[i];
                if (null == axle) continue;
                
                _leftWingPlateInfos.Add(new WingPlateInfo(plate.transform, axle.transform));
            }
            
            for (Int32 i = 0; i < rightPlates.Length; i++) {
                GameObject plate = rightPlates[i];
                if (null == plate) continue;
                
                if (i >= rightPlateAxles.Length) continue;
                GameObject axle = rightPlateAxles[i];
                if (null == axle) continue;
                
                _rightWingPlateInfos.Add(new WingPlateInfo(plate.transform, axle.transform));
            }
        }

        public void Run(float inputHorizontal)
        {
            #region 输入与控制
            
            // 测试
            float diffOfHorizontal = (inputHorizontal - _preInputHorizontal) * 10;
            _preInputHorizontal = inputHorizontal;
            foreach (WingPlateInfo plateInfo in _leftWingPlateInfos) {
                plateInfo.plate.Rotate(-plateInfo.plateAxle.transform.right, diffOfHorizontal);
            }
            foreach (WingPlateInfo plateInfo in _rightWingPlateInfos) {
                plateInfo.plate.Rotate(plateInfo.plateAxle.transform.right, diffOfHorizontal);
            }
            
            #endregion

            #region 飞行效果

            

            #endregion
        }
    }
}