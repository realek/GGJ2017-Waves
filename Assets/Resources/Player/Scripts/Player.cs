using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject meteorPrefab;
    public Grid targetGrid;
    [SerializeField]
    private float m_maxScaleFactor;
    [SerializeField]
    private float m_maxForceFactor;
    [SerializeField]
    private float m_baseForce;

    
    private bool m_onCD = false;
    [SerializeField]
    private float m_meteorCD = 1.0f;
    private WaitForSeconds m_CDTick;
    private WaitForEndOfFrame m_EoF;
    // Use this for initialization
    void Awake()
    {

        m_EoF = new WaitForEndOfFrame();
        m_CDTick = new WaitForSeconds(m_meteorCD);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if(Input.GetKey(KeyCode.Space) && !m_onCD && targetGrid.SelectedUnit != null)
            {
                m_onCD = true;
                Rigidbody meteor = Instantiate(meteorPrefab).GetComponent<Rigidbody>();
                meteor.transform.position = gameObject.transform.position;
                var dir = targetGrid.SelectedUnit.transform.position - meteor.transform.position;
                meteor.AddForce(dir.normalized * m_baseForce,ForceMode.Impulse);
            }
            if (m_onCD)
            {
                yield return m_CDTick;
                m_onCD = false;
            }
            else
                yield return m_EoF;
        }
    }
}
