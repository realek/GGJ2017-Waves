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

    [Range(0, 1)]
    public float unlockThreshold;

    public float cost;

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
            if (m_timeUntilFire >= 0 && !GetComponent<Button>().interactable)
            {
                return false;
            }
            return true;
        }
    }

    [SerializeField]
    private float m_timeUntilFire;

    void Update ()
    {
        transform.GetChild(0).GetComponent<Text>().text = "Cost: " + cost + "\r\n" + asteroid.rockCh * 100 + "/" + asteroid.metalCh * 100 + "/" + asteroid.crystalCh * 100 + "%";
        Button btn = GetComponent<Button>();
        timerImage.fillAmount = fireRatePercentage;



        if (unlockThreshold <= player.currentScorePercentage)
        {
            if (!btn.interactable)
            {
                btn.interactable = true;
            }
        }
        else
        {
            if (btn.interactable)
            {
                btn.interactable = false;
                if (player.selectedAsteroidButton == this)
                {
                    player.selectedAsteroidButton = null;
                    selectedImage.gameObject.SetActive(false);
                }
            }
        }

        if (player.currentScorePercentage <= 0 || player.currentScorePercentage >= 1)
        {
            btn.interactable = false;
        }

        if (m_timeUntilFire > 0)
        {
            m_timeUntilFire -= Time.deltaTime;
        }
    }

    public void Fire ()
    {
        m_timeUntilFire = fireRate;
    }

    public void SelectButton ()
    {
        if (player.selectedAsteroidButton != null)
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
