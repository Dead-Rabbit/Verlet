using UnityEngine;

namespace zxVerticle
{
    public class ZXPlane : MonoBehaviour
    {
        [System.Serializable]
        public struct WheelInfo
        {
            public Transform visualwheel;
            public WheelCollider wheelcollider;
        }

        public float motor = 800;
        public float steer = 50;
        public float brake = 440;
        public WheelInfo FL;
        public WheelInfo FR;

        private void FixedUpdate()
        {
            //steer and accelerate car (wasd, arrows, leftanalog gamepad)
            float vert = Input.GetAxis("Vertical");  //-1..0..1
            float horz = Input.GetAxis("Horizontal");
            FL.wheelcollider.steerAngle = horz * steer;
            FR.wheelcollider.steerAngle = horz * steer;
            
            FL.wheelcollider.motorTorque = vert * motor;
            FR.wheelcollider.motorTorque = vert * motor;

            //brake car
            if (Input.GetButton("Fire1") == true) //leftctrl, mouseleftbutton, gamepad A
            {
                FL.wheelcollider.brakeTorque = brake;
                FR.wheelcollider.brakeTorque = brake;
            }
            else
            {
                FL.wheelcollider.brakeTorque = 0;
                FR.wheelcollider.brakeTorque = 0;
            }

            UpdateVisualWheels();
        }
        private void UpdateVisualWheels()
        {
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