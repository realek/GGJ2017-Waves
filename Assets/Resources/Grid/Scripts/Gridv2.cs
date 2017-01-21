using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Gridv2 : MonoBehaviour {

    private Vector3[] m_verts;
    private int[] m_vertIDs;
    private int[] m_wBufferA;
    private int[] m_wBufferB;
    private Mesh m_mesh;

    private float damp = 0.985f;
    private float maxWaveHeight = 4.0f;
    private int splashForceBase = 1500;
    private bool swapMe = true;
    // Use this for initialization
    private int rowCols =128;
	void Start () {

        m_mesh = GetComponent<MeshFilter>().mesh;
        m_verts = m_mesh.vertices;
        m_vertIDs = new int[m_verts.Length];
        m_wBufferA = new int[m_verts.Length];
        m_wBufferB = new int[m_verts.Length];
        //rowCols = (int)Mathf.Sqrt(m_mesh.vertexCount);
        Bounds bounds = m_mesh.bounds;
        float meshUnitX = (bounds.max.x - bounds.min.x) / (float)rowCols;
        float meshUnitZ = (bounds.max.z - bounds.min.z) / (float)rowCols;
        
        for(int i = 0;i< m_verts.Length; i++)
        {
            m_vertIDs[i] = -1;
            m_wBufferA[i] = 0;
            m_wBufferB[i] = 0;
        }

        for(int i = 0; i< m_verts.Length; i++)
        {
            float c = ((m_verts[i].x - bounds.min.x) / meshUnitX);
            float r = ((m_verts[i].z - bounds.min.z) / meshUnitZ);
            float pos = (r * ((float)rowCols + 1)) + c + 0.5f;
            m_vertIDs[(int)pos] = i;
        }

        MakeSplash(rowCols / 2, rowCols / 2, 1);
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject caster = collision.gameObject;
        RaycastHit hit;
        Vector3 dir = transform.position - caster.transform.position;
        if (Physics.Raycast(caster.transform.position, dir, out hit, 10))
        {
            Bounds bounds = m_mesh.bounds;
            float xUnit = (bounds.max.x - bounds.min.x) / rowCols;
            float zUnit = (bounds.max.z - bounds.min.z) / rowCols;
            float x = (bounds.max.x - bounds.min.x) - ((bounds.max.x - bounds.min.x) * hit.textureCoord.x);
            float z = (bounds.max.z - bounds.min.z) - ((bounds.max.z - bounds.min.z) * hit.textureCoord.y);
            float c = (x/xUnit * );
            float r = (z/zUnit * );
            MakeSplash((int)c, (int)r,1);
        }
    }

    void checkInput()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Bounds bounds = m_mesh.bounds;
                float xStep = (bounds.max.x - bounds.min.x) / rowCols;
                float zStep = (bounds.max.z - bounds.min.z) / rowCols;
                float xCoord = (bounds.max.x - bounds.min.x) - ((bounds.max.x - bounds.min.x) * hit.textureCoord.x);
                float zCoord = (bounds.max.z - bounds.min.z) - ((bounds.max.z - bounds.min.z) * hit.textureCoord.y);
                float column = (xCoord / xStep);// + 0.5;
                float row = (zCoord / zStep);// + 0.5;
                MakeSplash((int)column, (int)row,1);
            }
        }
    }

        void MakeSplash(int x, int y, float splashMagMult)
    {
        int pos = ((y * (rowCols + 1)) + x);
        m_wBufferA[pos] = (int)(splashForceBase * splashMagMult);
        m_wBufferA[pos - 1] = (int)(splashForceBase * splashMagMult);
        m_wBufferA[pos + 1] = (int)(splashForceBase * splashMagMult);

        m_wBufferA[pos - (rowCols + 1)] = (int)(splashForceBase * splashMagMult);
        m_wBufferA[pos - (rowCols + 1) + 1] = (int)(splashForceBase * splashMagMult);
        m_wBufferA[pos - (rowCols +1) - 1] = (int)(splashForceBase * splashMagMult);

        m_wBufferA[pos + (rowCols+1)] = (int)(splashForceBase * splashMagMult);
        m_wBufferA[pos + (rowCols +1) + 1] = (int)(splashForceBase * splashMagMult);
        m_wBufferA[pos + (rowCols +1) - 1] = (int)(splashForceBase * splashMagMult);

    }

    void HandleSplash(int[] src, int[] dest)
    {
        int pos = 0;
        for(int i = 1;i < rowCols -1; i++)
        {
            for(int j = 1; j <rowCols; j++)
            {
                pos = (i * (rowCols + 1)) + j;
                dest[pos] = ((src[pos - 1] + src[pos + 1] + src[pos - (rowCols + 1)] + src[pos + (rowCols + 1)]) >> 1) - dest[pos];
                dest[pos] = (int)(dest[pos] * damp);
            }
        }
    }


	// Update is called once per frame
	void Update () {

        checkInput();

        int[] cwBuffer;
        if (swapMe)
        {
            HandleSplash(m_wBufferA, m_wBufferB);
            cwBuffer = m_wBufferB;
        }
        else
        {
            HandleSplash(m_wBufferB, m_wBufferA);
            cwBuffer = m_wBufferA;
        }

        swapMe = !swapMe;

        //apply ripple effect
        Vector3[] targetVerts = new Vector3[m_verts.Length];
        int vId;
        
        for(int i = 0; i < cwBuffer.Length;i++)
        {
            vId = m_vertIDs[i];
            targetVerts[vId] = m_verts[vId];
            targetVerts[vId].y += ((float)cwBuffer[i] / splashForceBase) * maxWaveHeight;
        }
        m_mesh.vertices = targetVerts;
	}
}
