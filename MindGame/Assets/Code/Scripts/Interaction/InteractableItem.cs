using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InteractableItem : MonoBehaviour
{
    public ShortTextItem m_shortTextItem { get; private set; }
    public InspectableItem m_inspectableItem { get; private set; }
    public CollectableItem m_collectableItem { get; private set; }
    public TriggerableItem m_triggerableItem { get; private set; }

    // Start is called before the first frame update
    protected void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");

        m_shortTextItem = GetComponent<ShortTextItem>();
        m_inspectableItem = GetComponent<InspectableItem>();
        m_collectableItem = GetComponent<CollectableItem>();
        m_triggerableItem = GetComponent<TriggerableItem>();
    }
}
