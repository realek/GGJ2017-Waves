using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject resourcePrefab;
    [SerializeField]
    public float rockCh;
    [SerializeField]
    public float metalCh;
    [SerializeField]
    public float crystalCh;
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

    public List<GameObject> cParticles = new List<GameObject>();

    public Player player;
    public AsteroidButton asteroidButton;


    private void Awake ()
    {
        Invoke("CustomDestroy", 5f);
        transform.Rotate(Random.insideUnitSphere * 360);
        transform.localScale = new Vector3(m_scale, m_scale, m_scale);
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (!collided)
        {
            Debug.Log("Asteroid Collided!");
            Invoke("CustomDestroy", 0.2f);
            Camera.main.transform.parent.GetComponent<CameraShake>().shakeAmplitude += scale * 1.5f;

            foreach (GameObject g in cParticles)
            {
                var sys = Instantiate(g).GetComponent<ParticleSystem>();
                sys.transform.position = transform.position;
            }

            int numberOfOres = Random.Range(0, 6);
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
            Debug.Log("Asteroid Completed Generating Resources!");

            collided = true;
        }
    }

    private void CustomDestroy ()
    {
        if (!collided)
        {
            player.AddScore(asteroidButton.cost);
            Debug.Log("Refunding...");
        }
        Debug.Log("Asteroid Destroyed!");
        Destroy(gameObject);
    }
}
