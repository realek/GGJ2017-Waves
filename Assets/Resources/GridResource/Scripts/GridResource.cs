using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridResources
{
    None,
    Rock = 5,
    Metal = 10,
    Crystal = 20
}

public class GridResource : MonoBehaviour {

    [HideInInspector]
    public GridResources type;
    private int m_scoreValue;
    public int ScoreValue { get { return m_scoreValue; } }
    WaitForSeconds m_w8;
    [SerializeField]
    private float m_distanceFromGridNeutral;
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private Sprite m_rock;
    [SerializeField]
    private Sprite m_metal;
    [SerializeField]
    private Sprite m_crystal;
    [SerializeField]
    private SpriteRenderer childSprite;
	// Use this for initialization

	void Start ()
    {
        m_w8 = new WaitForSeconds(Time.fixedDeltaTime);
        m_scoreValue = (int)type;
        if (transform.parent == null)
            throw new MissingReferenceException("No parent please assign parent.");
        StartCoroutine(FloatUP()); // I LIKE TO ABUSE // Our lord and savior
	}

    IEnumerator FloatUP()
    {
        while (true)
        {
            if (transform.parent.position.y + m_distanceFromGridNeutral > transform.position.y)
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.fixedDeltaTime * speed, transform.position.z);
            childSprite.transform.LookAt(Camera.main.transform);
            yield return m_w8;
        }

    }
}
