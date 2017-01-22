using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public AsteroidButton selectedAsteroidButton;
    [SerializeField]
    private GameObject m_ship;
    private NavMeshAgent m_shipAgent;
    [SerializeField]
    private Grid targetGrid;
    [SerializeField]
    private Transform m_navplane;
    [SerializeField]
    private float m_asteroidHeightSpawn = 40;

    private void Awake ()
    {
        m_shipAgent = m_ship.GetComponent<NavMeshAgent>();
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
    }

    public Vector3 RandomAsteroidPosition ()
    {
        return new Vector3(targetGrid.numberOfUnits * Random.insideUnitCircle.x, m_asteroidHeightSpawn, targetGrid.numberOfUnits * Random.insideUnitCircle.y);
    }
}
