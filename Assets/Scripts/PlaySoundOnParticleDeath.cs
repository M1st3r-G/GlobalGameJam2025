using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PlaySoundOnParticleDeath : MonoBehaviour
{
    [SerializeField] private AudioClip soundToPlay;
    [SerializeField] private float volume = 1.0f;

    private ParticleSystem m_particleSystem;
    private ParticleSystem.Particle[] m_particles;

    private void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
        m_particles = new ParticleSystem.Particle[m_particleSystem.main.maxParticles];
    }

    public void FixedUpdate()
    {
        int particleCount = m_particleSystem.GetParticles(m_particles);
        for (int i = 0; i < particleCount; i++)
        {
            if (!(m_particles[i].remainingLifetime <= Time.fixedDeltaTime)) continue;
            
            Vector3 particlePosition = transform.TransformPoint(m_particles[i].position);
            particlePosition.z = 0;
            AudioSource.PlayClipAtPoint(soundToPlay, particlePosition, volume);
        }
    }
}
