using System;
using UnityEngine;

namespace zxVerticle
{
    [RequireComponent(typeof(Rigidbody))]
    public class ZXPlane : MonoBehaviour
    {
        

        [Header("刚体")]
        private Rigidbody _rigidbody;
        public GameObject centerOfMass;
        private float vert = 0;
        private float horz = 0;

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

        [System.Serializable]
        public struct WingInfo
        {
            public GameObject wing;
            private WheelCollider axle;
        }
        [Header("机翼")] 
        public WingInfo[] leftWingInfos;
        public WingInfo[] rightWingInfos;

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
            vert = Input.GetAxis("Vertical");
            horz = Input.GetAxis("Horizontal");
            
            UpdateWheels();
        }
        private void UpdateWheels()
        {
            FL.wheelcollider.steerAngle = horz * steer;
            FR.wheelcollider.steerAngle = horz * steer;
            FL.wheelcollider.motorTorque = vert * motor;
            FR.wheelcollider.motorTorque = vert * motor;
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
    }
}