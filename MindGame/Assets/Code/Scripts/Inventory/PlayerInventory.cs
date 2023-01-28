using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Parameters")]
    public UIInventoryController m_uiController;
    public Vector3 m_whereToPutInventoryItems;

    // Delegates and events;
    public delegate void InventoryContentUpdateEventHandler(CollectableItem[] items);
    public delegate void InventoryContentFullEventHandler();
    public InventoryContentUpdateEventHandler m_eventOnContentChanged;
    public InventoryContentFullEventHandler m_eventOnContentFull;

    public CollectableItem[] Items { get => m_items; }

    private CollectableItem[] m_items = new CollectableItem[4];
    private int m_curIdxSlot;

    // Start is called before the first frame update
    void Start()
    {
        if (m_uiController == null)
        {
            Debug.LogError("UIInventoryController is not found. ");
        }
        else
        {
            m_eventOnContentChanged += m_uiController.UpdateInventory;
        }

        m_curIdxSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToInventory(CollectableItem item)
    {
        m_items[m_curIdxSlot] = item;
        m_curIdxSlot++;

        item.OnCollected();
        item.gameObject.transform.position = m_whereToPutInventoryItems;

        m_eventOnContentChanged?.Invoke(m_items);

        if(CheckInventoryFull())
        {
            m_eventOnContentFull?.Invoke();
        }
    }

    public bool CheckInventoryFull()
    {
        for (int i = 0; i < m_items.Length; ++i)
        {
            if (m_items[i] == null)
                return false;
        }
        return true;
    }

    public void ClearInventory()
    {
        for (int i = 0; i < m_items.Length; ++i)
        {
            m_items[i] = null;
        }
        m_curIdxSlot = 0;

        m_eventOnContentChanged?.Invoke(m_items);
    }
}
