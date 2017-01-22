using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject resourcePrefab;
    [SerializeField]
    public float crystalCh;
    [SerializeField]
    public float metalCh;
    [SerializeField]
    public float rockCh;
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
        transform.localScale = new Vector3(m_scale, m_scale, m_scale);
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

            int numberOfOres = Random.Range(1, 4);
            var res = new GridResource[numberOfOres];


            for (int i = 0; i < res.Length; i++)
            {
                float ch = Random.value;
                res[i] = Instantiate(resourcePrefab).GetComponent<GridResource>();
                if (ch <= crystalCh)
                    res[i].type = GridResources.Crystal;
                else if (ch > crystalCh && ch <= crystalCh + metalCh)
                    res[i].type = GridResources.Metal;
                else if (ch > crystalCh + metalCh && ch <= crystalCh + metalCh + rockCh)
                    res[i].type = GridResources.Rock;

                res[i].transform.position = collision.contacts[Random.Range(0, collision.contacts.Length)].point + (Random.insideUnitSphere * 2);
                res[i].transform.SetParent(Grid.currentInstance.transform);
            }

                collided = true;
        }

    }
}
