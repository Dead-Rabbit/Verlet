using System;
using System.Collections.Generic;
using UnityEngine;

namespace zxVehicle.plane
{
    public class ZXPlaneWingManager : MonoBehaviour
    {
        #region 飞机相关

        private ZXPlane _plane;
        private Rigidbody _planeRigidbody;

        #endregion
        
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

        [Range(0, 10000)] public float controlPlatePower = 0;
        [Range(0, 10000)] public float floatPlatePower = 0;
        
        // 旋转板
        public GameObject[] leftControlPlates;
        public GameObject[] rightControlPlates;
        public GameObject[] leftPlateAxles;
        public GameObject[] rightPlateAxles;
        public GameObject[] planePanelWing;
        private List<WingPlateInfo> _leftControlWingPlateInfos = new List<WingPlateInfo>();
        private List<WingPlateInfo> _righControltWingPlateInfos = new List<WingPlateInfo>();

        private float _preInputHorizontal = 0;

        void Awake()
        {
            _plane = transform.parent.GetComponent<ZXPlane>();
            if (null == _plane) {
                Debug.LogError("机翼未找到宿主飞机");
                return;
            }
            _planeRigidbody = _plane.GetComponent<Rigidbody>();
            
            for (Int32 i = 0; i < leftControlPlates.Length; i++) {
                GameObject plate = leftControlPlates[i];
                if (null == plate) continue;
                
                if (i >= leftPlateAxles.Length) continue;
                GameObject axle = leftPlateAxles[i];
                if (null == axle) continue;
                
                _leftControlWingPlateInfos.Add(new WingPlateInfo(plate.transform, axle.transform.right));
            }
            
            for (Int32 i = 0; i < rightControlPlates.Length; i++) {
                GameObject plate = rightControlPlates[i];
                if (null == plate) continue;
                
                if (i >= rightPlateAxles.Length) continue;
                GameObject axle = rightPlateAxles[i];
                if (null == axle) continue;
                
                _righControltWingPlateInfos.Add(new WingPlateInfo(plate.transform, axle.transform.right));
            }
        }

        public void Run(float inputHorizontal)
        {
            #region 输入与控制
            
            // 测试
            float diffOfHorizontal = (inputHorizontal - _preInputHorizontal) * 10;
            _preInputHorizontal = inputHorizontal;
            foreach (WingPlateInfo plateInfo in _leftControlWingPlateInfos) {
                plateInfo.plate.Rotate(-plateInfo.plateAxle, diffOfHorizontal);
            }
            foreach (WingPlateInfo plateInfo in _righControltWingPlateInfos) {
                plateInfo.plate.Rotate(plateInfo.plateAxle, diffOfHorizontal);
            }
            
            #endregion

            #region 飞行效果

            // 模拟速度
            // TODO 设定控制方向 - z方向减弱控制，加强y方向的控制
            // TODO 受力根据速度大小来确定力度
            Vector3 controlForce = _planeRigidbody.velocity.normalized * controlPlatePower;
            // 计算飞行给面板带来的力
            foreach (WingPlateInfo plateInfo in _leftControlWingPlateInfos) {
                Transform plate = plateInfo.plate;
                _planeRigidbody.AddForceAtPosition(controlForce, plate.position);
            }
            foreach (WingPlateInfo plateInfo in _righControltWingPlateInfos) {
                Transform plate = plateInfo.plate;
                _planeRigidbody.AddForceAtPosition(controlForce, plate.position);
            }

            Vector3 floatForce = _planeRigidbody.velocity.normalized * floatPlatePower;
            foreach (GameObject plate in planePanelWing) {
                _planeRigidbody.AddForceAtPosition(floatForce, plate.transform.position);
            }

            #endregion
        }
    }
}