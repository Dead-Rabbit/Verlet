using System;
using System.Collections.Generic;
using UnityEngine;
using zxGameMath.verletObj;

namespace zxGameMath
{
    public class VerletManager : MonoBehaviour
    {
        private static VerletManager _instance;

        public static VerletManager Instance {
			get{
				return _instance;
			}
		}

        #region 预设

        public GameObject particleObj;    // 质点物体
        public GameObject stickObj;       // 棍状物体

        #endregion
        
        // 重力
        public Vector3 Forcedir = new Vector3(0, -1f, 0);
        
        // 韦尔莱物体
        List<VerletObj> _verletObjs = new List<VerletObj>();

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            // 创建圆球
            // _verletObjs.Add(new VerletBall(new Vector3(0, 0, 0)));
            
            // 创建棍状
            _verletObjs.Add(new VerletStick(new Vector3(0, 0, 0)));
        }

        private void Update()
        {
            foreach (VerletObj verletObj in _verletObjs) {
                verletObj.Update(Time.deltaTime);
            }
        }
    }
}