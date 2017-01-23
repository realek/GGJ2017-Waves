using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTLDestroy : MonoBehaviour
{
    public float destroyTime=10;
    [SerializeField]
    List<AudioClip> m_sounds;

    private ParticleSystem m_particleSys;
    void Awake ()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = m_sounds[Random.Range(0, m_sounds.Count)];
        source.Play();
        Destroy(gameObject, destroyTime);
    }

}
