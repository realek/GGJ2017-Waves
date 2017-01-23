using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject target;
    [SerializeField]
    public float shakeAmplitude;

    [SerializeField]
    public float dampen;

    public List<AudioClip> laugthers = new List<AudioClip>();

    [SerializeField]
    private AudioSource audioSource;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update ()
    {
        target.transform.localPosition = Random.insideUnitSphere * shakeAmplitude;
        shakeAmplitude *= dampen;

        if (shakeAmplitude > 3.14f && !audioSource.isPlaying)
        {
            audioSource.clip = laugthers[Random.Range(0, laugthers.Count)];
            audioSource.Play();
        }
    }
}
