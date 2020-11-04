using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zxVehicle.plane
{
    public class ZXPlaneCamera : MonoBehaviour
    {
        public GameObject targetCamera;
        
        private Camera _camera;
        
        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            if (null != targetCamera) {
                _camera.transform.position = targetCamera.transform.position;
                _camera.transform.rotation = targetCamera.transform.rotation;
            }
        }
    }
}
