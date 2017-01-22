using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private float m_score;
    public float score { get { return m_score; } }
    [SerializeField]
    private float maxLevelScore;

    public float currentPercentage
    {
        get
        {
            return m_score / maxLevelScore;
        }
    }

    private void Awake ()
    {
        m_shipAgent = m_ship.GetComponent<NavMeshAgent>();
        m_tractored = new List<GridResource>();
    }
    private void Update ()
    {
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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (selectedAsteroidButton != null)
            {
                if (selectedAsteroidButton.canFire && targetGrid.SelectedUnit != null)
                {
                    selectedAsteroidButton.Fire();
                    Rigidbody meteor = Instantiate(selectedAsteroidButton.asteroid.gameObject).GetComponent<Rigidbody>();
                    meteor.transform.position = RandomAsteroidPosition();
                    var dir = targetGrid.SelectedUnit.transform.position - meteor.transform.position;
                    meteor.AddForce(dir.normalized * selectedAsteroidButton.asteroid.velocity, ForceMode.VelocityChange);
                }

            }
        }

        ProcessTractored();
    }

    private void FixedUpdate()
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

    public void AddScore(int value)
    {
        m_score += value;
    }

    private void TractorBeam()
    {
        RaycastHit hit;
        Debug.DrawLine(m_ship.transform.position, m_ship.transform.position + (Vector3.down * 30), Color.red);
        if (Physics.SphereCast(m_ship.transform.position,1,Vector3.down, out hit,30,tractorLayer))
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
                    m_tractored.Add(hit.collider.gameObject.AddComponent<GridResource>());
            }
            else
                m_tractored.Add(hit.collider.gameObject.AddComponent<GridResource>());
        }
    }

    private void ProcessTractored()
    {
        if(m_tractored.Count>0)
            for (int i = 0; i < m_tractored.Count; i++)
            {
                if (m_tractored[i].transform.position == transform.position)
                {
                    m_score += m_tractored[i].ScoreValue;
                    m_tractored[i].supressed = true;
                    Destroy(m_tractored[i]);
                    m_tractored.Remove(m_tractored[i]);
                }
                else
                {
                    m_tractored[i].transform.position = Vector3.MoveTowards(m_tractored[i].transform.position,
                        transform.position, Time.deltaTime);
                }
            }
    }
}
