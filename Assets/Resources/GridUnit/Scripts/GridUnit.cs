using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit : MonoBehaviour {

    [SerializeField]
    private float m_force = 100;
    private Rigidbody m_rb;
	// Use this for initialization
	void Start () {

        m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
