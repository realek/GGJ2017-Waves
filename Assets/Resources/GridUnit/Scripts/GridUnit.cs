using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GridUnit : MonoBehaviour
{
    public float posY
    {
        get
        {
            return transform.position.y;
        }
        set
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
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

    public void AddAmplitude (float amount)
    {
        posY += amount;
    }
}
