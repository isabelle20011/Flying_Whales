using UnityEngine;
using System.Collections;

public class vPickupItem : MonoBehaviour
{
    public AudioSource m_ExplosionAudio;
    public ParticleSystem m_ExplosionParticles;

    void Start()
    {
        m_ExplosionAudio = GetComponentInChildren<AudioSource>();
        m_ExplosionParticles = GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {      
            if (m_ExplosionAudio)
            {
                m_ExplosionAudio.Play();
            }
            if (m_ExplosionParticles)
            {
                m_ExplosionParticles.transform.parent = null;
                m_ExplosionParticles.Play();
                Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
            }
            Destroy(gameObject);
        }
    }
}