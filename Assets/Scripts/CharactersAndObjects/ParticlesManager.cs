using UnityEngine;

namespace CharactersAndObjects
{
    public class ParticlesManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particles;

        public void EmitAllParticles(Vector3 particlePosition, Vector3 particleDirection)
        {
            foreach (ParticleSystem particle in particles)
            {
                EmitSpecifiedParticle(particle, particlePosition, particleDirection, 1);
            }
        }

        private void EmitSpecifiedParticle(ParticleSystem particle, Vector3 particlePosition, Vector3 particleDirection, int count)
        {
            particle.transform.position = particlePosition;
            particle.transform.rotation = Quaternion.LookRotation(particleDirection);
            particle.transform.Rotate(Vector3.right, 90);
            particle.Emit(count);
        }
    }
}
