using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    private float m_TTL;


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, m_TTL);
    }

}
