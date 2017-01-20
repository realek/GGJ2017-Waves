using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class GridUnit : MonoBehaviour
{
    [SerializeField]
    private float m_amplitude = 1;
    private EventTrigger m_trigger;

    private Rigidbody m_rb;

    public Grid parent;

    public float angle
    {
        get
        {
            return parent.angle;
        }
    }

    void Awake ()
    {
        m_trigger = GetComponent<EventTrigger>();

        m_rb = GetComponent<Rigidbody>();
    }

    private void Update ()
    {
        transform.position = new Vector3(transform.position.x, m_amplitude * Mathf.Sin(angle * Mathf.Deg2Rad), transform.position.z);
    }

    public void SetCallback(Grid grid)
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { grid.SetSelected(gameObject); });
        entry.callback.AddListener((eventData) => { Debug.Log("TEST"); });
        trigger.triggers.Add(entry);
    }
}
