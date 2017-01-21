using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AsteroidButton selectedAsteroidButton;
    [SerializeField]
    private GameObject ship;
    [SerializeField]
    private Grid targetGrid;
    public float verticalShipOffset = 20.0f;
    private void Update ()
    {


        if (Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
            {
                
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
