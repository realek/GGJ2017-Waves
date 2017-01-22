using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject res_metalsPrefab;
    [SerializeField]
    private GameObject res_rocksPrefab;
    [SerializeField]
    private GameObject res_crystalsPrefab;
    [SerializeField]
    private Transform m_navPlane;
    [SerializeField]
    private float launchforce;


    public enum Resources
    {
        None,
        Rock = 5,
        Metal = 10,
        Crystal = 20
    }

    public void SpawnResource(Resources res,Vector3 position, Vector3 direction)
    {
        GameObject resource = null;
        switch (res)
        {
            case Resources.Rock:
                resource = Instantiate(res_rocksPrefab);
                    break;
            case Resources.Metal:
                resource = Instantiate(res_metalsPrefab);
                    break;
            case Resources.Crystal:
                resource = Instantiate(res_crystalsPrefab);
                break;
        }
        rb.transform.position = position;
        //rb.AddForce(direction * launchforce, ForceMode.Impulse);

    }
}
