using System;
using UnityEngine;

namespace zxGameMath.verletObj
{
    /// <summary>
    /// 棍状韦尔莱
    /// </summary>
    public class VerletStick : VerletObj
    {
        // 质子
        private Int32 particleNum = 4;
        private Single distanceBetweenTwoParticle = 1f;
        
        // 棍子

        public VerletStick(Vector3 stickPosition)
        {
            
            // 两个质点 + 连接的棍子
            for (Int32 i = 0; i < particleNum; i++) {
                Particles.Add(new VParticle(stickPosition));
            }
            
            // Particles.Add(new VParticle(stickPosition));
            // Particles.Add(new VParticle(stickPosition + new Vector3(0, -distanceBetweenTwoParticle, 0)));
            // Particles.Add(new VParticle(stickPosition + new Vector3(distanceBetweenTwoParticle, -distanceBetweenTwoParticle, distanceBetweenTwoParticle)));
            // Particles.Add(new VParticle(stickPosition + new Vector3(distanceBetweenTwoParticle, distanceBetweenTwoParticle)));

            for (Int32 i = 0; i < Particles.Count; i++) {
                ShowParticles.Add(GameObject.Instantiate(VerletManager.Instance.particleObj));
            }
            
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
                delta = new Vector3(-distanceBetweenTwoParticle, -distanceBetweenTwoParticle, -distanceBetweenTwoParticle).normalized;
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

            if (!CheckIfParticleInRound(particleA)) 
                particleA.CurPos = particleA.OldPos;
            
            if (!CheckIfParticleInRound(particleB)) 
                particleB.CurPos = particleB.OldPos;
        }

        public override void SolveConstrain()
        {
            SloveDistance(Particles[0], Particles[1]);
            SloveDistance(Particles[1], Particles[2]);
            SloveDistance(Particles[2], Particles[3]);
            SloveDistance(Particles[0], Particles[3]);
        }
    }
}