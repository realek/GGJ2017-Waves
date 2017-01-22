using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTLDestroy : MonoBehaviour {

    private ParticleSystem m_particleSys;
	// Use this for initialization
	void Start () {

        m_particleSys = GetComponent<ParticleSystem>();
        Destroy(gameObject, m_particleSys.main.duration + 1);
	}
}
