using System;
using UnityEngine;

namespace zxVehicle.plane
{
    public class ZXPlaneWingManager : MonoBehaviour
    {
        private ZXVehicle owner;

        public ZXPlaneWingManager(ZXVehicle owner)
        {
            this.owner = owner;
        }
        
        // 旋转板
        public GameObject[] leftPlates;
        public GameObject[] rightPlates;

        public void Run(float _InputHorizontal, float _InputVert)
        {
            
        }
    }
}