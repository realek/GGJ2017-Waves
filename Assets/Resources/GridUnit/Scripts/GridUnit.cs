using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GridUnit : MonoBehaviour
{
    [SerializeField]
    private float m_amplitude = 1;
    public float amplitude
    {
        get
        {
            return m_amplitude;
        }
        set
        {
            m_amplitude = value;
        }
    }


    private Rigidbody m_rb;

    public Grid parent;

    public float angle
    {
        get
        {
            return parent.angle;
        }
    }

    void Awake ()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update ()
    {
        float calculateY = amplitude * Mathf.Sin(angle * Mathf.Deg2Rad)+amplitude/3*Mathf.Sin((angle+44)*Mathf.Deg2Rad);
        transform.position = new Vector3(transform.position.x, calculateY, transform.position.z);
    }

    public void AddAmplitude (float amount)
    {
        amplitude += amount;
    }
}
