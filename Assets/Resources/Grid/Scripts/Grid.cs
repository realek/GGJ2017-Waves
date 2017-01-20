using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject gridUnitPrefab;
    [SerializeField]
    private int m_numberOfRowUnits = 1;
    [SerializeField]
    private int m_numberOfColumnUnits = 1;
    [SerializeField]
    private float m_gridUnitDistance = 1.0f;
    [SerializeField]
    private float m_affectedAreaDropoff = 0.0f;
    private GridUnit[][] m_units;

    [Range(0.01f, 100)]
    [SerializeField]
    private float m_cyclesPerSecond = 1;

    [SerializeField]
    private float m_dampingFactor = 0.98f;

    public float angle;
    [SerializeField]
    GridUnit m_SelectedUnit;
    // Use this for initialization
    void Start ()
    {

        m_units = new GridUnit[m_numberOfRowUnits][];
        bool extentscmp = false;
        float cmpUnitOffset = 0.0f;

        //fill the grid with units
        for (int i = 0; i < m_numberOfRowUnits; ++i)
        {
            m_units[i] = new GridUnit[m_numberOfColumnUnits];
            for (int j = 0; j < m_numberOfColumnUnits; ++j)
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

    private void Update ()
    {
        angle += 360 * m_cyclesPerSecond * Time.deltaTime;
        angle %= 360;

        for (int i = 0; i < m_units.Length; ++i)
        {
            for (int j = 0; j < m_units[i].Length; ++j)
            {
                InterpolateUnit(i, j);
                DampenUnit(m_units[i][j]);
            }
        }
    }

    private void FixedUpdate()
    {
        SelectGridUnit();
    }
    private void DampenUnit (GridUnit unit)
    {
        unit.amplitude *= m_dampingFactor;
    }

    private void InterpolateUnit (int i, int j)
    {
        float value = 0;
        float numberOfUnitsInterpolated = 0;
        for (int k = i - 1; k <= i + 1; ++k)
        {
            for (int l = j - 1; l <= j + 1; ++l)
            {
                if (k != i || l != j)
                {
                    if (IsInsideBounds(k, 0, m_numberOfRowUnits) && IsInsideBounds(l, 0, m_numberOfColumnUnits))
                    {
                        ++numberOfUnitsInterpolated;
                        value = m_units[k][l].amplitude;
                    }
                }
            }
        }
        float calculatedAmplitude = value / numberOfUnitsInterpolated;
        if (calculatedAmplitude > Mathf.Epsilon)
        {
            m_units[i][j].amplitude += calculatedAmplitude * Time.deltaTime;
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


    void SelectGridUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (m_SelectedUnit != null && m_SelectedUnit.gameObject == hit.collider.gameObject)
                return;
            m_SelectedUnit = hit.collider.gameObject.GetComponent<GridUnit>();
            Debug.Log("Selected Unit" + hit.collider.name);
        }
        else
            m_SelectedUnit = null;
    }
}
