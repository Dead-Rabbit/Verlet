using System;
using System.Collections.Generic;
using UnityEngine;

namespace zxGameMath.verletObj
{
    public class VerletHexagon : VerletObj
    {
        private GameObject verletStickObj;
        
        // 质子初始化位置
        private List<Vector3> postionList = new List<Vector3>();
        
        // 质子距离
        private Single r = 2f;
        
        // 棍子
        private LineRenderer stickRender;

        public VerletHexagon(Vector3 stickPosition)
        {
            
            verletStickObj = new GameObject("棍子");

            Single param1 = (Single) (Math.Sqrt(6) / 3);
            Single param2 = (Single) (Math.Sqrt(3) / 3);
            
            postionList.Add(stickPosition);
            postionList.Add(stickPosition + new Vector3(-r * 0.5f, -param1 * r, -param2 * 0.5f * r));
            postionList.Add(stickPosition + new Vector3(r * 0.5f, -param1 * r, -param2 * 0.5f * r));
            postionList.Add(stickPosition + new Vector3(0, -param1 * r, -param2 * r));
            
            // 两个质点 + 连接的棍子
            for (Int32 i = 0; i < postionList.Count; i++)
            {
                Particles.Add(new VParticle(postionList[i]));
                
                GameObject showParticleObj = GameObject.Instantiate(VerletManager.Instance.particleObj, verletStickObj.transform);
                showParticleObj.transform.localPosition = postionList[i];
                ShowParticles.Add(showParticleObj);
            }
                
            stickRender = verletStickObj.AddComponent<LineRenderer>();
            stickRender.material = VerletManager.Instance.stickMaterial;
            stickRender.startWidth = 0.1f;
            stickRender.endWidth = 0.1f;
            stickRender.startColor = Color.red;
            stickRender.endColor = Color.red;
            
            stickRender.positionCount = 12;
        }
        
        // 计算两个棍子之间的距离
        private void SloveDistance(VParticle particleA, VParticle particleB)
        {
            Vector3 delta = particleB.CurPos - particleA.CurPos;
            Single CurrentDistance = delta.magnitude;
            Single ErrorFactor = r;
            ErrorFactor = (CurrentDistance - r) / CurrentDistance;

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
        
        public override void SyncVerletParticles()
        {
            // 同步质子
            base.SyncVerletParticles();
            
            // 同步棍子
            List<Vector3> points = new List<Vector3>();
            for (Int32 i = 0; i < 3; i++)
            {
                points.Add(ShowParticles[0].transform.position);
                points.Add(ShowParticles[i % 3 + 1].transform.position);
                points.Add(ShowParticles[(i + 1) % 3 + 1].transform.position);
                points.Add(ShowParticles[0].transform.position);
            }
            stickRender.SetPositions(points.ToArray());
        }

        public override void SolveConstrain()
        {
            for (Int32 i = 0; i < Particles.Count - 1; i++)
            {
                for (Int32 j = i + 1; j < Particles.Count; j++)
                {
                    SloveDistance(Particles[i], Particles[j]);
                }
            }
        }
    }
}