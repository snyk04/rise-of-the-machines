using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;

    public void EmitAllParticles(Vector3 point)
    {
        // TODO: Пока колхоз, но мне кажется, что есть возможность сделать это так как это сделано в комментах, но оно почему-то не пашет :(

        //var param = new ParticleSystem.EmitParams();
        //param.position = point;
        //particle.Emit(param, 1);
        //particle1.Emit(param, 1);
        
        foreach (ParticleSystem particle in particles)
        {
            EmitSpecifiedParticle(particle, point, 1);
        }
    }

    private void EmitSpecifiedParticle(ParticleSystem particle, Vector3 position, int count)
    {
        particle.transform.position = position;
        particle.Emit(count);
    }
}
