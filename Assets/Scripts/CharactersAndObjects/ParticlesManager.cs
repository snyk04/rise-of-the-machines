using UnityEngine;

namespace CharactersAndObjects
{
    public class ParticlesManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particles;

        public void EmitAllParticles(RaycastHit hitInfo)
        {
            foreach (ParticleSystem particle in particles)
            {
                EmitSpecifiedParticle(particle, hitInfo, 1);
            }
        }

        private void EmitSpecifiedParticle(ParticleSystem particle, RaycastHit hitInfo, int count)
        {
            particle.transform.position = hitInfo.point;
            particle.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
            particle.transform.Rotate(Vector3.right, 90);
            particle.Emit(count);
        }
    }
}
