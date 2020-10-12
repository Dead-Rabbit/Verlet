using System;
using UnityEngine;

namespace zxGameMath.verletObj
{
    public class VerletBall : VerletObj
    {
        public VerletBall(Vector3 ballPosition)
        {
            for (Int32 i = 0; i < 1; i++) {
                Particles.Add(new VParticle(ballPosition));
                ShowParticles.Add(GameObject.Instantiate(VerletManager.Instance.particleObj));
            }
        }
    }
}