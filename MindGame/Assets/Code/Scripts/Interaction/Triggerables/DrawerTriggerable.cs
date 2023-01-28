using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DrawerController))]
public class DrawerTriggerable : TriggerableItem
{
    private DrawerController m_drawerController;

    // Start is called before the first frame update
    void Start()
    {
        m_drawerController = GetComponent<DrawerController>();
        if(!m_drawerController)
        {
            Debug.LogError("DrawerController is misisng on [" + gameObject.ToString() + "]!" );
        }
    }

    public override void OnTriggered()
    {
        m_drawerController.Interact();
    }
}
