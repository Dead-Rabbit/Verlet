using UnityEngine;

namespace zxVehicle.plane
{
    public class ZXWheelManager : MonoBehaviour
    {
        [System.Serializable]
        public struct WheelInfo
        {
            public Transform visualwheel;
            public WheelCollider wheelcollider;
        }
        
        [Header("轮子")]
        public float motor = 10000;
        public float steer = 3;
        public float brake = 40;
        public WheelInfo FL;
        public WheelInfo FR;

        public void Run(float _InputHorizontal, float _InputVert)
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
    }
}