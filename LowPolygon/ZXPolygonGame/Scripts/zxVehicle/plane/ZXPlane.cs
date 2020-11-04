using System.Collections.Generic;
using UnityEngine;

namespace zxVehicle.plane
{
    [RequireComponent(typeof(Rigidbody))]
    public class ZXPlane : ZXVehicle
    {
        private Rigidbody _rigidbody;
        public GameObject centerOfMass;
        
        // 输入
        private float _inputVert = 0;
        private float _inputHorizontal = 0;

        #region 轮子
        
        public ZXWheelManager wheelManager;

        #endregion

        #region 机翼

        public ZXPlaneWingManager planeWingManager;
        
        private struct WingInfo
        {
            public GameObject wing;
            public GameObject wingAxle;
        }
        private List<WingInfo> wingAxles = new List<WingInfo>();

        #endregion

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            //  设置中心点
            if (null != centerOfMass) {
                _rigidbody.centerOfMass = centerOfMass.transform.position;
            }
        }
        
        private void FixedUpdate()
        {
            _inputVert = Input.GetAxis("Vertical");
            _inputHorizontal = Input.GetAxis("Horizontal");
            wheelManager.Run(_inputHorizontal, _inputVert);        // 轮子
            planeWingManager.Run(_inputHorizontal);    // 机翼
        }
    }
}