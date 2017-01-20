using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject gridUnitPrefab;
    [SerializeField]
    private int m_numberOfUnitRows = 1;
    [SerializeField]
    private int m_numberOfUnitsPerRow = 1;
    [SerializeField]
    private float m_gridUnitDistance = 1.0f;
    [SerializeField]
    private float m_affectedAreaDropoff = 0.0f;
    private GridUnit[][] m_units;

    [Range(0.01f, 100)]
    [SerializeField]
    private float m_cyclesPerSecond = 1;

    public float angle;

    // Use this for initialization
    void Start ()
    {

        m_units = new GridUnit[m_numberOfUnitRows][];
        bool extentscmp = false;
        float cmpUnitOffset = 0.0f;

        //fill the grid with units
        for (int i = 0; i < m_numberOfUnitRows; i++)
        {
            m_units[i]=new GridUnit[m_numberOfUnitsPerRow];
            for (int j = 0; j < m_numberOfUnitsPerRow; j++)
            {
                GameObject currentUnit = Instantiate(gridUnitPrefab);

                if (!extentscmp)
                {
                    Collider c = currentUnit.GetComponent<Collider>();
                    if (c == null)
                    {
                        throw new MissingComponentException("Missing collider");
                    }
                    cmpUnitOffset = ((c.bounds.extents.x+ c.bounds.extents.z)/2) + (m_gridUnitDistance * 2);
                }
                float zOffset = i * cmpUnitOffset;
                float xOffset = 0;
                if (j != 0)
                {
                    xOffset += j * cmpUnitOffset;
                }

                currentUnit.transform.position = transform.position + new Vector3(xOffset, 0, zOffset);
                currentUnit.transform.parent = transform;

                GridUnit currentGridUnit = currentUnit.GetComponent<GridUnit>();
                currentGridUnit.parent = this;
                currentGridUnit.SetCallback(this);
                m_units[i][j] = currentGridUnit;
            }
        }
    }

    private void Update ()
    {
        angle += 360 * m_cyclesPerSecond * Time.deltaTime;

        for (int i = 0; i < m_units.Length; ++i)
        {
            for (int j = 0; j < m_units[i].Length; ++j)
            {

            }
        }
    }

    public void SetSelected(GameObject gridUnit)
    {
        Debug.Log("selected is" + gridUnit.name);
    }

}
