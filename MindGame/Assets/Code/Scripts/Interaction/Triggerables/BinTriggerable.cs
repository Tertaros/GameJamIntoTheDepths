using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BinController))]
public class BinTriggerable : TriggerableItem
{
    private BinController m_binController;

    // Start is called before the first frame update
    void Start()
    {
        m_binController = GetComponent<BinController>();
        if (!m_binController)
        {
            Debug.LogError("BinController is misisng on [" + gameObject.ToString() + "]!");
        }
    }

    public override void OnTriggered()
    {
        m_binController.Interact();
    }
}
