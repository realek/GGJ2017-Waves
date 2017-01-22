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


    public void SpawnResource(GridResources res,Vector3 position, Vector3 direction)
    {
        GameObject resource = null;
        switch (res)
        {
            case GridResources.Rock:
                resource = Instantiate(res_rocksPrefab);
                    break;
            case GridResources.Metal:
                resource = Instantiate(res_metalsPrefab);
                    break;
            case GridResources.Crystal:
                resource = Instantiate(res_crystalsPrefab);
                break;
        }
        resource.transform.position = position;
        //rb.AddForce(direction * launchforce, ForceMode.Impulse);

    }
}
