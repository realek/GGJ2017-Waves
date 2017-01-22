using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidButton : MonoBehaviour
{
    public Player player;
    public Asteroid asteroid;
    public Image timerImage;
    public Image selectedImage;

    [SerializeField]
    private float fireRate;

    /// <summary>
    /// Returns 0 to 1 values.
    /// </summary>
    public float fireRatePercentage
    {
        get
        {
            return m_timeUntilFire / fireRate;
        }
    }

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
        timerImage.fillAmount = fireRatePercentage;
    }

    public void Fire ()
    {
        m_timeUntilFire = fireRate;
    }

    public void SelectButton ()
    {
        if (player.selectedAsteroidButton!=null)
        {
            player.selectedAsteroidButton.selectedImage.gameObject.SetActive(false);
        }

        if (player.selectedAsteroidButton == this)
        {
            player.selectedAsteroidButton = null;
        }
        else
        {
            player.selectedAsteroidButton = this;
            selectedImage.gameObject.SetActive(true);
        }
    }
}
