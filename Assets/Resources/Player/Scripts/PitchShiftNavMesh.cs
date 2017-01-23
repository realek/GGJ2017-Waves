using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PitchShiftNavMesh : MonoBehaviour
{
    private Vector3 previousPosition;
    public float curSpeed;
    private AudioSource audioSource;

    void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update ()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;

        audioSource.pitch = 0.9f + (curSpeed / 25);
        previousPosition = transform.position;
    }
}
