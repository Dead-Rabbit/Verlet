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

        // [Range(0, 100)] public float controlPlatePower = 0;
        // [Range(0, 1000)] public float floatPlatePowerFront = 0;
        // [Range(0, 1000)] public float floatPlatePowerBack = 0;
        public float flyMinSpeed = 1f;
        
        // 旋转板
        public GameObject[] leftControlPlates;
        public GameObject[] rightControlPlates;
        public GameObject[] leftPlateAxles;
        public GameObject[] rightPlateAxles;
        public GameObject[] planePanelWing;
        private List<WingPlateInfo> _leftControlWingPlateInfos = new List<WingPlateInfo>();
        private List<WingPlateInfo> _righControltWingPlateInfos = new List<WingPlateInfo>();

        private float _preInputHorizontal = 0;

        #region Debug

        private Ray[] _DebugForceRay = new Ray[2];

        #endregion

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

            // _planeRigidbody.AddForce(new Vector3(0, _planeRigidbody.mass * 9.81f, 0));
            // SimulatePlaneFly();
            
            #endregion
        }

        private void SimulatePlaneFly()
        {
            if (_planeRigidbody.velocity.magnitude > flyMinSpeed) {
                _plane.transform.forward = new Vector3(0, 0, 1);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            for (Int32 i = 0; i < 2; i++) {
                Gizmos.DrawRay(_DebugForceRay[i]);
            }
        }
    }
}