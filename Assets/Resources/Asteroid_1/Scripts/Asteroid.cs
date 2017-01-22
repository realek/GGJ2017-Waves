using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject resourcePrefab;
    [SerializeField]
    private float crystalCh;
    [SerializeField]
    private float metalCh;
    [SerializeField]
    private float rockCh;
    private bool collided = false;
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

    public List<GameObject> cParticles = new List<GameObject>();

    private Rigidbody rb;

    private void Awake ()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter (Collision collision)
    {

        if (!collided)
        {
            Destroy(gameObject, 0.1f);

            foreach (GameObject g in cParticles)
            {
                var sys = Instantiate(g).GetComponent<ParticleSystem>();
                sys.transform.position = transform.position;
            }

            float ch = Random.value;
            bool rock, metal, crystal;
            rock = metal = crystal = false;

            if (ch <= crystalCh)
                crystal = true;
            else if (ch > crystalCh && ch <= crystalCh + metalCh)
                metal = true;
            else if (ch > crystalCh + metalCh && ch <= crystalCh + metalCh + rockCh)
                rock = true;

            if (rock || metal || crystal)
            {
                int size = Random.Range(1, 4);
                var res = new GridResource[size];

                for(int i = 0; i<res.Length;i++)
                {
                    res[i] = Instantiate(resourcePrefab).GetComponent<GridResource>();
                    if (rock)
                        res[i].type = GridResources.Rock;
                    else if (metal)
                        res[i].type = GridResources.Metal;
                    else if (crystal)
                        res[i].type = GridResources.Crystal;
                    res[i].transform.position = collision.contacts[Random.Range(0,collision.contacts.Length)].point + (Random.insideUnitSphere * 2);
                    res[i].transform.SetParent(Grid.currentInstance.transform);
                }

            }

                collided = true;
        }

    }
}
