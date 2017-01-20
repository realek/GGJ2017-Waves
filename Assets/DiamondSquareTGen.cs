using UnityEngine;
using System.Collections;

public class DiamondSquareTGen : MonoBehaviour {

    private Terrain m_terrain;
    
    [SerializeField,Tooltip("Must be power of 2")]
    private int m_TGSize;
    private float[,] m_TData;
    private const int MIN_DIAGONAL_LENGTH = 2;
    private const float BASE_RAND_RANGE = 0.25f;
    [SerializeField,Tooltip("Range roughness factor reduction"),Range(1.0f,10.0f)]
    private float m_roughness;
	// Use this for initialization
	void Awake () {

        
        //get terrain componenet because...BAGEL
        m_terrain = GetComponent<Terrain>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generate();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (m_terrain.terrainData.heightmapResolution != m_TGSize)
            {
                m_terrain.terrainData.heightmapResolution = m_TGSize;
            }
            m_terrain.terrainData.SetHeights(0, 0, new float[m_TGSize + 1, m_TGSize + 1]);
        }
	
	}

    void Generate()
    {

        //init arr
        int i, j, latice;
        float midPoint;
        float rand_range = BASE_RAND_RANGE;
        m_TData = new float[m_TGSize + 1, m_TGSize + 1];

        for (latice = m_TGSize; latice >= MIN_DIAGONAL_LENGTH; latice /= 2)
        {
            int halfLatice = latice / 2;
            for (i = 0; i < m_TGSize; i+=latice)
            {
                for (j = 0; j < m_TGSize; j+=latice)
                {
                    float rVal = (Random.value * 2.5f * rand_range) - rand_range;

                    midPoint = m_TData[i, j];
                    midPoint += m_TData[i + latice, j];
                    midPoint += m_TData[i, j + latice];
                    midPoint += m_TData[i + latice, j + latice];
                    midPoint /= 4.0f; // YOLO Float
                    midPoint = Mathf.Clamp01(midPoint + rVal);

                    m_TData[i + halfLatice, j + halfLatice] = midPoint;

                }
            }

            for (i = 0; i <= m_TGSize; i += halfLatice)
            {
                for (j = (i + halfLatice) % latice; j < m_TGSize; j += latice)
                {
                    float rVal = (Random.value * 3f * rand_range) - rand_range;
                    midPoint = m_TData[(i - halfLatice + m_TGSize)%m_TGSize, j];
                    midPoint += m_TData[(i + halfLatice) % m_TGSize, j];
                    midPoint += m_TData[i, (j - halfLatice + m_TGSize)%m_TGSize];
                    midPoint += m_TData[i, (j + halfLatice) % m_TGSize];

                    midPoint /= 4.0f;
                    midPoint = Mathf.Clamp01(midPoint + rVal);
                    m_TData[i, j] = midPoint;

                    if (i == 0)
                        m_TData[m_TGSize, j] = midPoint;
                    if (j == 0) 
                        m_TData[i, m_TGSize] = midPoint;
                }
            }

            rand_range /= m_roughness;
        }

        if (m_terrain.terrainData.heightmapResolution != m_TGSize)
        {
            m_terrain.terrainData.heightmapResolution = m_TGSize;
        }

        m_terrain.terrainData.SetHeights(0, 0, m_TData);
    }

}
