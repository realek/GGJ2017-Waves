using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float m_scale;
    public float scale
    {
        get
        {
            return m_scale;
        }
    }

    [SerializeField]
    private float m_velocity;
    public float velocity
    {
        get
        {
            return m_velocity;
        }
    }

    [SerializeField]
    private float m_density;
    public float density
    {
        get
        {
            return m_density;
        }
    }

    public float multiplier
    {
        get
        {
            return scale / Mathf.Sqrt(2);
        }
    }

    public float impactForce
    {
        get
        {
            return velocity * rb.mass * density;
        }
    }

    public List<GameObject> particles = new List<GameObject>();

    private Rigidbody rb;

    private void Awake ()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter (Collision collision)
    {
        //Destroy(gameObject);

    }
}
