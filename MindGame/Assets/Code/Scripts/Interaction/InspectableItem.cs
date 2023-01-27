using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableItem : MonoBehaviour
{
    [Header("Parameters")]
    public Sprite m_inspectView; // For inspectable

    // Start is called before the first frame update
    void Start()
    {
        if (m_inspectView == null)
        {
            Debug.LogWarning("InspectableItem [" + gameObject.name + "] has no inspectable view. ");
        }
    }
}
