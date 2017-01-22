using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTLDestroy : MonoBehaviour {

    [SerializeField]
    List<AudioClip> m_sounds;

    private ParticleSystem m_particleSys;
	// Use this for initialization
	void Awake () {

        AudioSource source = GetComponent<AudioSource>();
        source.clip = m_sounds[Random.Range(0, m_sounds.Count)];
        m_particleSys = GetComponent<ParticleSystem>();
        source.Play();
        Destroy(gameObject, m_particleSys.main.duration + 1);
	}

}
