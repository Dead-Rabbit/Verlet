using System;
using System.Collections.Generic;
using UnityEngine;

namespace zxVehicle.plane
{
    public class ZXPlaneWingManager : MonoBehaviour
    {
        private ZXPlane plane;
        
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

        [Range(0, 10)]
        public float simulateSpeed = 0;
        
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
            // 找到对应飞机
            plane = transform.parent.GetComponent<ZXPlane>();
            if (null == plane) {
                Debug.LogError("机翼未找到宿主飞机");
                return;
            }
            
            for (Int32 i = 0; i < leftPlates.Length; i++) {
                GameObject plate = leftPlates[i];
                if (null == plate) continue;
                
                if (i >= leftPlateAxles.Length) continue;
                GameObject axle = leftPlateAxles[i];
                if (null == axle) continue;
                
                _leftWingPlateInfos.Add(new WingPlateInfo(plate.transform, axle.transform.right));
            }
            
            for (Int32 i = 0; i < rightPlates.Length; i++) {
                GameObject plate = rightPlates[i];
                if (null == plate) continue;
                
                if (i >= rightPlateAxles.Length) continue;
                GameObject axle = rightPlateAxles[i];
                if (null == axle) continue;
                
                _rightWingPlateInfos.Add(new WingPlateInfo(plate.transform, axle.transform.right));
            }
        }

        public void Run(float inputHorizontal)
        {
            #region 输入与控制
            
            // 测试
            float diffOfHorizontal = (inputHorizontal - _preInputHorizontal) * 10;
            _preInputHorizontal = inputHorizontal;
            foreach (WingPlateInfo plateInfo in _leftWingPlateInfos) {
                plateInfo.plate.Rotate(-plateInfo.plateAxle, diffOfHorizontal);
            }
            foreach (WingPlateInfo plateInfo in _rightWingPlateInfos) {
                plateInfo.plate.Rotate(plateInfo.plateAxle, diffOfHorizontal);
            }
            
            #endregion

            #region 飞行效果

            // 模拟速度
            Vector3 velocity = plane.transform.forward * simulateSpeed;
            // 计算飞行给面板带来的力
            foreach (WingPlateInfo plateInfo in _leftWingPlateInfos) {
                // plateInfo.plate.up
            }

            #endregion
        }
    }
}