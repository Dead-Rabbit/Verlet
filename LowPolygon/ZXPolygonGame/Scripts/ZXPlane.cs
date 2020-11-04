using System;
using UnityEngine;

namespace zxVerticle
{
    [RequireComponent(typeof(Rigidbody))]
    public class ZXPlane : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public GameObject centerOfMass;
        
        // 输入
        private float _InputVert = 0;
        private float _InputHorizontal = 0;

        #region 轮子
        
        [System.Serializable]
        public struct WheelInfo
        {
            public Transform visualwheel;
            public WheelCollider wheelcollider;
        }
        
        [Header("轮子")]
        public float motor = 800;
        public float steer = 50;
        public float brake = 440;
        public WheelInfo FL;
        public WheelInfo FR;

        #endregion

        #region 机翼

        [Header("机翼")] 
        public GameObject[] leftWingInfos;
        public GameObject leftWingPlane;
        public GameObject[] rightWingInfos;
        public GameObject rightWingPlane;

        #endregion

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            //  设置中心点
            if (null != centerOfMass) {
                _rigidbody.centerOfMass = centerOfMass.transform.position;
            }
            // 初始化机翼
            InitZXPlanWing();
        }

        #region 初始化

        private void InitZXPlanWing()
        {
            
        }

        #endregion

        private void FixedUpdate()
        {
            _InputVert = Input.GetAxis("Vertical");
            _InputHorizontal = Input.GetAxis("Horizontal");
            
            // 轮子
            UpdateWheels();
            
            // 机翼
            UpdateWings();
        }
        
        /// <summary>
        /// 更新轮子
        /// </summary>
        private void UpdateWheels()
        {
            FL.wheelcollider.steerAngle = _InputHorizontal * steer;
            FR.wheelcollider.steerAngle = _InputHorizontal * steer;
            FL.wheelcollider.motorTorque = _InputVert * motor;
            FR.wheelcollider.motorTorque = _InputVert * motor;
            if (Input.GetButton("Fire1") == true) {
                FL.wheelcollider.brakeTorque = brake;
                FR.wheelcollider.brakeTorque = brake;
            } else {
                FL.wheelcollider.brakeTorque = 0;
                FR.wheelcollider.brakeTorque = 0;
            }

            Vector3 pos;
            Quaternion rot;
            FL.wheelcollider.GetWorldPose(out pos, out rot);
            FL.visualwheel.position = pos;
            FL.visualwheel.rotation = rot;
            FR.wheelcollider.GetWorldPose(out pos, out rot);
            FR.visualwheel.position = pos;
            FR.visualwheel.rotation = rot;
        }

        /// <summary>
        /// 更新机翼
        /// </summary>
        private void UpdateWings()
        {
            
        }
    }
}