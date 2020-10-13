using System;
using System.Collections.Generic;
using UnityEngine;

namespace zxGameMath.verletObj
{
    /// <summary>
    /// 韦尔莱积分物体
    /// </summary>
    public class VerletObj
    {
        #region 结构

        public Int32 ParticleCount = 0;

        // 质点集合
        public List<VParticle> Particles = new List<VParticle>();
        
        // 显示质点
        public List<GameObject> ShowParticles = new List<GameObject>();
        
        // 显示棍棒
        public List<LineRenderer> Sticks = new List<LineRenderer>();

        #endregion

        /// <summary>
        /// 韦尔莱积分
        /// </summary>
        public virtual void Verlet(Single delTime)
        {
            foreach (VParticle particle in Particles) {
                Vector3 newPosition = particle.CurPos + (particle.CurPos - particle.OldPos) + delTime * delTime * VerletManager.Instance.Forcedir;
                particle.OldPos = particle.CurPos;
                particle.CurPos = newPosition;
            }
        }

        /// <summary>
        /// 位移约束
        /// </summary>
        public virtual void SolveConstrain()
        {
            foreach (VParticle particle in Particles) {
                Vector3 diff = particle.CurPos - particle.OldPos;
                Vector3 velocity = diff / diff.magnitude;
                if (particle.CurPos.y <= 0) {        // TODO 待调整、查看原因
                    particle.CurPos = particle.OldPos;
                }
            }
        }

        /// <summary>
        /// 同步展示节点与计算节点的数据
        /// </summary>
        public virtual void SyncVerletParticles()
        {
            for (Int32 i = 0; i < Particles.Count; i++) {
                ShowParticles[i].transform.position = Particles[i].CurPos;
            }
        }
        
        /// <summary>
        /// 规范Verlet质点的位置
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        public Boolean CheckIfParticleInRound(VParticle particle)
        {
            // 目前设定半径内范围运动
            return particle.CurPos.magnitude <= 8;
        }
        
        /// <summary>
        /// Unity的更新
        /// </summary>
        public virtual void Update(Single delTime)
        {
            Verlet(delTime);
            SolveConstrain();
            SyncVerletParticles();
        }
    }
}