using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    private float m_TTL;
    public GameObject particleSystemExplosionSmall;
    public GameObject particleSystemExplosionBig;


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, m_TTL);
       var p1 = Instantiate(particleSystemExplosionSmall).GetComponent<ParticleSystem>();
        p1.transform.position = gameObject.transform.position;
        p1.Play();
        var p2 = Instantiate(particleSystemExplosionBig).GetComponent<ParticleSystem>();
        p2.transform.position = gameObject.transform.position;
        p2.Play();

    }

}
