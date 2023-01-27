using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryController : MonoBehaviour
{
    public Image[] m_itemIcons = new Image[4];

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;++i)
        {
            if(m_itemIcons[i] == null)
            {
                Debug.LogError("Item icon image ["+i.ToString()+"] is missing! ");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory(CollectableItem[] items)
    {
        for (int idx = 0; idx < 4; ++idx)
        {
            if(items[idx] != null)
            {
                m_itemIcons[idx].sprite = items[idx].m_itemIcon;
                m_itemIcons[idx].enabled = true;
            }
            else
            {
                m_itemIcons[idx].sprite = null;
                m_itemIcons[idx].enabled = false;
            }
        }
    }
}
