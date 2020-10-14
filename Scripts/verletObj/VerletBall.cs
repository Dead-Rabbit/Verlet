using System;
using UnityEngine;

namespace zxGameMath.verletObj
{
    public class VerletBall : VerletObj
    {
        public VerletBall(Vector3 ballPosition)
        {
            Particles.Add(new VParticle(ballPosition));
            ShowParticles.Add(GameObject.Instantiate(VerletManager.Instance.particleObj));
        }
    }
}