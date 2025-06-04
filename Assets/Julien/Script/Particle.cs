using System.Collections.Generic;
using Julien.Script.Interface;
using UnityEngine;

namespace Julien.Script
{
    public class Particle : MonoBehaviour, IPlayParticle
    {
        public void PlayParticle()
        {
            List<GameObject> particles = new List<GameObject>();

            for (int i = 0; i < transform.childCount; i++)
            {
                particles.Add(transform.GetChild(i).gameObject);
            }

            foreach (GameObject particle in particles)
            {
                gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
