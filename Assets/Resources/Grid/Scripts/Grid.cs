using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GameObject gridUnitPrefab;
    [SerializeField]
    private int m_numberofUnitRows = 1;
    [SerializeField]
    private int m_numberofUnitsPerRow = 1;
    [SerializeField]
    private float m_gridUnitDistance = 1.0f;
    [SerializeField]
    private float m_affectedAreaDropoff = 0.0f;
    private List<GameObject[]> m_units;

	// Use this for initialization
	void Start () {

        m_units = new List<GameObject[]>();
        bool extentscmp = false;
        float cmpUnitOffset = 0.0f;

        //fill the grid with units
        for(int i = 0; i < m_numberofUnitRows; i++)
        {
            m_units.Add(new GameObject[m_numberofUnitsPerRow]);
            for(int j = 0; j < m_numberofUnitsPerRow; j++)
            {
                GameObject currentUnit = Instantiate(gridUnitPrefab);
                
                if(!extentscmp)
                {
                    Collider c = currentUnit.GetComponent<Collider>();
                    if (c == null)
                        throw new MissingComponentException("Missing collider");
                    cmpUnitOffset = c.bounds.extents.x + (m_gridUnitDistance * 2);
                }
                float zOffset = i * cmpUnitOffset;
                float xOffset = 0;
                if (j != 0)
                    xOffset += j * cmpUnitOffset;


                currentUnit.transform.position = transform.position + new Vector3(xOffset, 0, zOffset);
                m_units[i][j] = currentUnit;
            }


        }
		
	}


}
