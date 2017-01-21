using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidButton : MonoBehaviour
{
    public Player player;
    public Asteroid asteroid;

    [SerializeField]
    private float fireRate;

    public bool canFire
    {
        get
        {
            if (m_timeUntilFire < 0)
            {
                return true;
            }
            return false;
        }
    }

    [SerializeField]
    private float m_timeUntilFire;

    void Update ()
    {
        if (!canFire)
        {
            m_timeUntilFire -= Time.deltaTime;
        }
        else
        {
        }
    }

    public void Fire ()
    {
        m_timeUntilFire = fireRate;
    }

    public void SelectButton ()
    {
        if (player.selectedAsteroidButton == this)
        {
            player.selectedAsteroidButton = null;
        }
        else
        {
            player.selectedAsteroidButton = this;
        }
    }
}
