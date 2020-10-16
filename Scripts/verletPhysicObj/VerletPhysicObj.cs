using System;
using System.Collections.Generic;
using UnityEngine;

namespace zxGameMath.verletObj
{
    public class VerletPhysicObj
    {
        private GameObject verletStickObj;
        
        // 质子距离
        private Single distanceBetweenTwoParticle = 1f;
        
        // 质点集合
        public List<VParticle> Particles = new List<VParticle>();
        
        // 显示质点
        public List<GameObject> ShowParticles = new List<GameObject>();

        // 棍子
        private LineRenderer stickRender;

        public VerletPhysicObj(Vector3 stickPosition)
        {
            verletStickObj = new GameObject("棍子");
            
            // 两个质点 + 连接的棍子
            for (Int32 i = 0; i < 2; i++)
            {
                Vector3 pos = stickPosition + i * 0.1f * new Vector3(1, 1, 1);
                Particles.Add(new VParticle(pos));
            }

            // 点GameObject
            for (Int32 i = 0; i < Particles.Count; i++) {
                GameObject showParticleObj = GameObject.Instantiate(VerletManager.Instance.particleObj, verletStickObj.transform);
                showParticleObj.transform.localPosition = Particles[i].CurPos;
                ShowParticles.Add(showParticleObj);
            }
            
            stickRender = verletStickObj.AddComponent<LineRenderer>();
            stickRender.material = VerletManager.Instance.stickMaterial;
            stickRender.startWidth = 0.1f;
            stickRender.endWidth = 0.1f;
            stickRender.startColor = Color.red;
            stickRender.endColor = Color.red;
        }

        /// <summary>
        /// 韦尔莱积分
        /// </summary>
        public virtual void Verlet(Single delTime)
        {
            foreach (VParticle particle in Particles) {
                if (particle.beFree) {
                    Vector3 newPosition = particle.CurPos + (particle.CurPos - particle.OldPos) + delTime * delTime * VerletManager.Instance.Forcedir;
                    particle.OldPos = particle.CurPos;
                    particle.CurPos = newPosition;
                }
            }
        }

        public void SyncVerletParticles()
        {
            // 同步棍子
            List<Vector3> points = new List<Vector3>();
            for (Int32 i = 0; i < Particles.Count; i++)
            {
                points.Add(ShowParticles[i].transform.position);
            }
            stickRender.SetPositions(points.ToArray());
        }

        // 计算两个棍子之间的距离
        private void SloveDistance(VParticle particleA, VParticle particleB)
        {
            Vector3 delta = particleB.CurPos - particleA.CurPos;
            Single CurrentDistance = delta.magnitude;
            Single ErrorFactor = distanceBetweenTwoParticle;
            if (CurrentDistance > 0) {
                ErrorFactor = (CurrentDistance - distanceBetweenTwoParticle) / CurrentDistance;
            }
            else {
                ErrorFactor = distanceBetweenTwoParticle;
            }

            if (particleA.beFree && particleB.beFree) {
                particleA.CurPos += ErrorFactor * 0.5f * delta;
                particleB.CurPos -= ErrorFactor * 0.5f * delta;
            } else if (particleA.beFree) {
                particleA.CurPos += ErrorFactor * delta;
            } else if (particleB.beFree) {
                particleB.CurPos -= ErrorFactor * delta;
            }

            // if (!CheckIfParticleInRound(particleA)) 
            //     particleA.CurPos = particleA.OldPos;
            //
            // if (!CheckIfParticleInRound(particleB)) 
            //     particleB.CurPos = particleB.OldPos;
        }

        public void SolveConstrain()
        {
            SloveDistance(Particles[0], Particles[1]);
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