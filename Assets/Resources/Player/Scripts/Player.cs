using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AsteroidButton selectedAsteroidButton;
    public Grid targetGrid;

    private void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (selectedAsteroidButton != null)
            {
                if (selectedAsteroidButton.canFire && targetGrid.SelectedUnit != null)
                {
                    selectedAsteroidButton.Fire();
                    Rigidbody meteor = Instantiate(selectedAsteroidButton.asteroid.gameObject).GetComponent<Rigidbody>();
                    meteor.transform.position = gameObject.transform.position;
                    var dir = targetGrid.SelectedUnit.transform.position - meteor.transform.position;
                    meteor.AddForce(dir.normalized * selectedAsteroidButton.asteroid.velocity, ForceMode.VelocityChange);
                }
            }
        }
    }
}
