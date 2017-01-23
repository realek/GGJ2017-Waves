using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float rotationTime;
    public float rotationSpeed;
    private Vector3 target;
    private float currentTime;

    void Update ()
    {
        if (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = Random.Range(rotationTime,rotationTime*1.5f);
            target = new Vector3(0, 0, Random.Range(-360, 360));
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(target), Time.deltaTime * rotationSpeed);
    }
}
