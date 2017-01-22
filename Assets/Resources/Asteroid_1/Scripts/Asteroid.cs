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

        if(!collided)
        {
            Destroy(gameObject, 0.1f);

            foreach (GameObject g in cParticles)
            {
                var sys = Instantiate(g).GetComponent<ParticleSystem>();
                sys.transform.position = transform.position;
            }
            collided = true;
        }


        /*
         * bool rock, metal, crystal;
                rock = metal = crystal = false;
                if (m_units[i][j].transform.position.y < transform.position.y - m_crystalSpawnTH)
                    crystal = true;
                else if (m_units[i][j].transform.position.y < transform.position.y - m_metalSpawnTH)
                    metal = true;
                else if (m_units[i][j].transform.position.y < transform.position.y - m_rockSpawnTH)
                    rock = true;

                if(rock || metal || crystal)
                {
                    GridResource res = Instantiate(gridResourcePrefab).GetComponent<GridResource>();

                    if (rock)
                        res.type = GridResources.Rock;
                    else if (metal)
                        res.type = GridResources.Metal;
                    else if (crystal)
                        res.type = GridResources.Crystal;

                    res.transform.position = m_units[i][j].transform.position;
                    res.transform.SetParent(transform);
                }
         */

    }
}
