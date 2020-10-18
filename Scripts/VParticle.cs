using System;
using UnityEngine;

namespace zxGameMath
{
    /// <summary>
    /// 记录韦尔莱积分法中的单位位置
    /// </summary>
    public class VParticle
    {
        public Vector3 OldPos = Vector3.zero;
        public Vector3 CurPos = Vector3.zero;

        public Boolean beFree = true;
        
        public Vector3 addForce = Vector3.zero;
        
//        public Vector3 target

        public VParticle()
        {
            
        }

        public VParticle(Vector3 pos)
        {
            CurPos = pos;
            OldPos = pos;
        }
    }
}