using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [Header("Parameters")]
    public Sprite m_itemIcon; // For collectable

    private Rigidbody m_rigidbody;
    private Collider m_collider;
    private MeshRenderer m_renderer;

    private AudioSource m_grab;

    // Start is called before the first frame update
    void Start()
    {
        if (m_itemIcon == null)
        {
            Debug.LogWarning("CollectableItem [" + gameObject.name + "] has no item icon. ");
        }

        m_renderer = GetComponent<MeshRenderer>();
        m_collider = GetComponent<Collider>();
        m_rigidbody = GetComponent<Rigidbody>();


        m_grab = gameObject.GetComponent<AudioSource>();
        if (!m_grab)
            Debug.LogError("Grab sound not found");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollected()
    {
        if (m_renderer)
            m_renderer.enabled = false;
        if (m_collider)
            m_collider.enabled = false;
        if (m_rigidbody)
            m_rigidbody.isKinematic = false;

        m_grab.Play();
    }

    public void OnUncollected()
    {
        if (m_renderer)
            m_renderer.enabled = true;
        if (m_collider)
            m_collider.enabled = true;
        if (m_rigidbody)
            m_rigidbody.isKinematic = true;
    }
}
