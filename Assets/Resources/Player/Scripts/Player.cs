using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public LayerMask tractorLayer;
    public AsteroidButton selectedAsteroidButton;
    private List<GridResource> m_tractored;
    [SerializeField]
    private GameObject m_ship;
    private NavMeshAgent m_shipAgent;
    [SerializeField]
    private Grid targetGrid;
    [SerializeField]
    private Transform m_navplane;
    [SerializeField]
    private float m_asteroidHeightSpawn = 40;

    public float tractorRadius = 2;

    public GameObject win;
    public GameObject lose;

    [SerializeField]
    private float m_score;

    public float score
    {
        get
        {
            return m_score;
        }
    }
    [SerializeField]
    private float maxLevelScore;

    public Slider scoreSlider;
    public Text scoreText;

    AudioSource m_source;

    public float currentScorePercentage
    {
        get
        {
            return m_score / maxLevelScore;
        }
    }

    private void Awake ()
    {
        m_source = GetComponent<AudioSource>();
        m_shipAgent = m_ship.GetComponent<NavMeshAgent>();
        m_tractored = new List<GridResource>();
        m_score = 0.05f * maxLevelScore;
    }

    private void Update ()
    {
        if (currentScorePercentage >= 1 || currentScorePercentage <= 0)
        {
            if (selectedAsteroidButton != null)
            {
                selectedAsteroidButton.SelectButton();
            }
            selectedAsteroidButton = null;

            if (currentScorePercentage >= 1)
            {
                if (!win.gameObject.activeSelf)
                {
                    win.gameObject.SetActive(true);
                }
            }
            else
            {
                if (currentScorePercentage <= 0)
                {
                    if (!lose.gameObject.activeSelf)
                    {
                        lose.gameObject.SetActive(true);
                    }
                }
            }
        }
        if (scoreSlider)
        {
            scoreSlider.value = currentScorePercentage;
        }
        if (scoreText)
        {
            scoreText.text = "Resources: " + m_score + "/" + maxLevelScore;
        }

        // Move Ship
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Vector3 projectedPos = hit.point;
                projectedPos.y = m_navplane.position.y;
                if (projectedPos != m_shipAgent.destination)
                    m_shipAgent.SetDestination(projectedPos);
            }
        }

        //Shoot Asteroid
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (selectedAsteroidButton != null)
            {
                if (selectedAsteroidButton.canFire && targetGrid.SelectedUnit != null)
                {
                    if (score - selectedAsteroidButton.cost >= 0)
                    {
                        AddScore(-selectedAsteroidButton.cost);
                        selectedAsteroidButton.Fire();
                        Rigidbody meteor = Instantiate(selectedAsteroidButton.asteroid.gameObject).GetComponent<Rigidbody>();
                        meteor.transform.position = RandomAsteroidPosition();
                        var dir = targetGrid.SelectedUnit.transform.position - meteor.transform.position;
                        meteor.AddForce(dir.normalized * selectedAsteroidButton.asteroid.velocity, ForceMode.VelocityChange);
                    }
                }
            }
        }

        ProcessTractored();
    }

    private void FixedUpdate ()
    {
        TractorBeam();
    }

    public Vector3 RandomAsteroidPosition ()
    {
        Vector2 circle = Random.insideUnitCircle;
        return new Vector3(targetGrid.numberOfUnits * circle.x, m_asteroidHeightSpawn, targetGrid.numberOfUnits * circle.y) + targetGrid.gridMiddlePoint;
    }

    private void OnDrawGizmosSelected ()
    {
        for (int i = 0; i < 10000; ++i)
        {
            Vector3 pos = RandomAsteroidPosition();
            Gizmos.DrawLine(pos, pos + Vector3.up * 0.5f);
            Gizmos.color = Color.red;
        }
    }

    public void AddScore (float value)
    {
        m_score += value;
    }

    public void ReleaseAsteroidButton()
    {
        if(selectedAsteroidButton!=null)
        {
            selectedAsteroidButton.selectedImage.gameObject.SetActive(false);
            selectedAsteroidButton = null;
        }

    }

    private void TractorBeam ()
    {
        RaycastHit hit;
        Debug.DrawLine(m_ship.transform.position, m_ship.transform.position + (Vector3.down * 30), Color.red);
        if (Physics.SphereCast(m_ship.transform.position, tractorRadius, Vector3.down, out hit, 30, tractorLayer))
        {
            if (m_tractored.Count > 0)
            {
                bool found = false;
                for (int i = 0; i < m_tractored.Count; i++)
                {
                    if (m_tractored[i].gameObject == hit.collider.gameObject)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    m_tractored.Add(hit.collider.gameObject.GetComponent<GridResource>());
            }
            else
                m_tractored.Add(hit.collider.gameObject.GetComponent<GridResource>());
        }
    }

    private void ProcessTractored ()
    {
        if (m_tractored.Count > 0)
            for (int i = 0; i < m_tractored.Count; i++)
            {

                if (PointInsideSphere(m_tractored[i].transform.position, m_ship.transform.position, 1.5f))
                {
                    if (currentScorePercentage > 0)
                    {
                        AddScore(m_tractored[i].ScoreValue);
                    }
                    m_tractored[i].supressed = true;
                    m_source.Play();
                    Destroy(m_tractored[i].gameObject);
                    m_tractored.Remove(m_tractored[i]);
                }
                else
                {
                    m_tractored[i].transform.position = Vector3.Lerp(m_tractored[i].transform.position,
                        m_ship.transform.position, Time.deltaTime * 2);
                }
            }
    }


    private bool PointInsideSphere (Vector3 p, Vector3 sc, float radius)
    {
        float d = (p - sc).sqrMagnitude;
        if (d < radius * radius)
        {
            return true;
        }
        return false;
    }

}
