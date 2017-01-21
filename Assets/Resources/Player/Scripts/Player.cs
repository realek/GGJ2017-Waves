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
    private Transform m_navplane;

    private void Awake()
    {
        m_shipAgent = m_ship.GetComponent<NavMeshAgent>();
    }
    private void Update ()
    {


        if (Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
            {
                Vector3 projectedPos = hit.point;
                projectedPos.y = m_navplane.position.y;
                m_shipAgent.SetDestination(projectedPos);
                
            }
        }
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    if (selectedAsteroidButton != null)
        //    {
        //        if (selectedAsteroidButton.canFire && targetGrid.SelectedUnit != null)
        //        {
        //            selectedAsteroidButton.Fire();
        //            Rigidbody meteor = Instantiate(selectedAsteroidButton.asteroid.gameObject).GetComponent<Rigidbody>();
        //            meteor.transform.position = gameObject.transform.position;
        //            var dir = targetGrid.SelectedUnit.transform.position - meteor.transform.position;
        //            meteor.AddForce(dir.normalized * selectedAsteroidButton.asteroid.velocity, ForceMode.VelocityChange);
        //        }
        //    }
        //}


    }
}
