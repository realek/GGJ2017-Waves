using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject gridUnitPrefab;
    [SerializeField]
    private int m_numberOfUnits = 1;
    public int numberOfUnits
    {
        get
        {
            return m_numberOfUnits;
        }
    }

    [SerializeField]
    private int m_interpolationSize = 1;
    [SerializeField]
    private float m_gridUnitDistance = 1.0f;

    [SerializeField]
    private float m_interval;

    [SerializeField]
    private float m_minAmplitudeInterval = 10;
    [SerializeField]
    private float m_maxAmplitudeInterval = 20;

    [SerializeField, Range(0.001f, 1.0f)]
    private float m_dampingFactor = 0.98f;

    private GridUnit[][] m_units;
    [SerializeField]
    private GridUnit m_SelectedUnit;
    public GridUnit SelectedUnit
    {
        get
        {
            return m_SelectedUnit;
        }
    }
    private float m_currentInterval;

    // Use this for initialization
    void Start ()
    {
        GenerateGrid();
    }

    private void Update ()
    {
        if (m_currentInterval > 0)
        {
            m_currentInterval -= Time.deltaTime;
        }
        else
        {
            m_units[Random.Range(0, m_numberOfUnits)][Random.Range(0, m_numberOfUnits)].AddAmplitude(Random.Range(m_minAmplitudeInterval, m_maxAmplitudeInterval));
        }
        SelectGridUnit();
        for (int i = 0; i < m_units.Length; ++i)
        {
            for (int j = 0; j < m_units[i].Length; ++j)
            {
                InterpolateUnit(i, j);
            }
        }
        for (int i = 0; i < m_units.Length; ++i)
        {
            for (int j = 0; j < m_units[i].Length; ++j)
            {
                DampenUnit(m_units[i][j]);
            }
        }

    }

    private void FixedUpdate ()
    {

    }

    private void DampenUnit (GridUnit unit)
    {
        unit.posY *= m_dampingFactor;
    }

    private void InterpolateUnit (int i, int j)
    {
        float value = 0;
        float numberOfUnitsInterpolated = 0;
        for (int k = i - m_interpolationSize; k <= i + m_interpolationSize; ++k)
        {
            for (int l = j - m_interpolationSize; l <= j + m_interpolationSize; ++l)
            {
                if (k != i || l != j)
                {
                    if (IsInsideBounds(k, 0, m_numberOfUnits) && IsInsideBounds(l, 0, m_numberOfUnits))
                    {
                        ++numberOfUnitsInterpolated;
                        value += m_units[k][l].posY;
                    }
                }
            }
        }
        float calculatedAmplitude = value / numberOfUnitsInterpolated;
        if (calculatedAmplitude > Mathf.Epsilon)
        {
            m_units[i][j].posY = calculatedAmplitude;
        }
    }

    /// <summary>
    /// Tests if value is between minBound(inclusive) and maxBound(inclusive).
    /// </summary>
    /// <param name="value"></param>
    /// <param name="minBound"></param>
    /// <param name="maxBound"></param>
    /// <returns></returns>
    public bool IsInsideBounds (int value, int minBound, int maxBound)
    {
        if (value >= minBound && value < maxBound)
        {
            return true;
        }
        return false;
    }

    public void GenerateGrid ()
    {
        m_units = new GridUnit[m_numberOfUnits][];
        bool extentscmp = false;
        float cmpUnitOffset = 0.0f;

        //fill the grid with units
        for (int i = 0; i < m_numberOfUnits; ++i)
        {
            m_units[i] = new GridUnit[m_numberOfUnits];
            for (int j = 0; j < m_numberOfUnits; ++j)
            {
                GameObject currentUnit = Instantiate(gridUnitPrefab);

                if (!extentscmp)
                {
                    Collider c = currentUnit.GetComponent<Collider>();
                    if (c == null)
                    {
                        throw new MissingComponentException("Missing collider!");
                    }
                    cmpUnitOffset = ((c.bounds.extents.x + c.bounds.extents.z) / 2) + (m_gridUnitDistance * 2);
                }
                float zOffset = i * cmpUnitOffset;
                float xOffset = 0;
                if (j != 0)
                {
                    xOffset += j * cmpUnitOffset;
                }

                currentUnit.transform.position = transform.position + new Vector3(xOffset, 0, zOffset);
                currentUnit.transform.parent = transform;
                currentUnit.name = "GridUnit(x:" + j + ", y:" + i + ")";
                GridUnit currentGridUnit = currentUnit.GetComponent<GridUnit>();
                currentGridUnit.parent = this;
                m_units[i][j] = currentGridUnit;
            }
        }
    }

    void SelectGridUnit ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (m_SelectedUnit != null && m_SelectedUnit.gameObject == hit.collider.gameObject)
                return;
            m_SelectedUnit = hit.collider.gameObject.GetComponent<GridUnit>();
            //Debug.Log("Selected Unit" + hit.collider.name);
        }
        else
        {
            m_SelectedUnit = null;
        }
    }
}

