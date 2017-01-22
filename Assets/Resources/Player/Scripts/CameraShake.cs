using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject target;

    [SerializeField]
    private float m_shakeAmplitude;

    [SerializeField]
    private float dampen;


    void Update ()
    {
        target.transform.localPosition = Random.insideUnitSphere * m_shakeAmplitude;
        m_shakeAmplitude *= dampen;
    }
}
