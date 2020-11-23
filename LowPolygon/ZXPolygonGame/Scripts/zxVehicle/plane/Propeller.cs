using System;
using UnityEngine;

namespace zxVehicle.plane
{
    public class Propeller : MonoBehaviour
    {
        public ZXPlane plane;
        private Rigidbody _planeRigidbody;
        
        [Header("旋转")]
        // speed 0 ~ 30
        public Single maxForwardRotationSpeed = 30;    // 最大向前旋转速度
        public Single maxBackRotationSpeed = -10;       // 最大向后旋转速度
        private Single _rotationSpeed = 0.1f;          // 当前旋转速度
        public Single rotationSpeedCauseForce = 10f;
        public Single reduceSpeedPerRate = 0.01f;
        
        public Single addRotationSpeed = 0.1f;
        
        private Single propellerRotationZ = 0;

        private void Start()
        {
            _planeRigidbody = plane.GetComponent<Rigidbody>();
            if (null == _planeRigidbody) {
                Debug.LogError("飞机没有绑定 刚体组件");
            }
        }

        public void PropellerUpdate()
        {
            propellerRotationZ += _rotationSpeed;
            transform.localRotation = Quaternion.Euler(0, 0, propellerRotationZ);
            
            // 旋转损失
            if (_rotationSpeed > 0) {
                _rotationSpeed -= reduceSpeedPerRate;
            }
            else {
                _rotationSpeed += reduceSpeedPerRate;
            }

            // 螺旋桨给予的力
            _planeRigidbody.AddForceAtPosition(transform.TransformVector(new Vector3(0, 0, 1) * GetPlanePropellerForce()), 
                                                transform.position, 
                                                ForceMode.Force);
        }

        private Single GetPlanePropellerForce()
        {
            return _rotationSpeed * rotationSpeedCauseForce;
        }

        public void ControlPropellerRotationSpeed(Single control)
        {
            _rotationSpeed += control * addRotationSpeed;
            if (control > 0) {
            }
            else {
                _rotationSpeed += control * addRotationSpeed;
            }

            if (_rotationSpeed > maxForwardRotationSpeed) {
                _rotationSpeed = maxForwardRotationSpeed;
            } else if (_rotationSpeed < maxBackRotationSpeed) {
                _rotationSpeed = maxBackRotationSpeed;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + transform.TransformVector(new Vector3(0, 0, 1) * _rotationSpeed));
        }
    }
}