using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableItem : MonoBehaviour
{
    [Header("Parameters")]
    public CollectableItem m_key;
    public bool m_retriggerable;

    protected bool m_triggered = false;

    public void Trigger(CollectableItem[] inventoryItems)
    {
        bool toTrigger = false;
        if(m_key)
        {
            foreach (var item in inventoryItems)
            {
                if (m_key == item)
                {
                    toTrigger = true;
                }
            }
        }
        else
        {
            toTrigger = true;
        }
        
        if(toTrigger)
        {
            if (!m_retriggerable)
            {
                if (!m_triggered)
                {
                    OnTriggered();
                    m_triggered = true;
                }
            }
            else
            {
                OnTriggered();
            }
        }
    }

    public bool IsTriggerable()
    {
        if(m_retriggerable)
        {
            return true;
        }
        else
        {
            if(!m_triggered)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public virtual void OnTriggered()
    {

    }
}
