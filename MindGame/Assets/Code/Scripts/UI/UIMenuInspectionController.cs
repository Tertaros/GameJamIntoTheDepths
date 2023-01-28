using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuInspectionController : UISingleLayerMenuController
{
    [Header("Parameters")]
    public Image m_itemImage;

    public delegate void OnInspectionEndEventHandler();
    public OnInspectionEndEventHandler m_eventOnInspectionEnd;

    // Start is called before the first frame update
    void Start()
    {
        if (m_itemImage == null)
        {
            Debug.LogError("No Image found!");
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            Disable();
            m_eventOnInspectionEnd?.Invoke();
        }
    }

    public void Setup(Sprite item)
    {
        m_itemImage.sprite = item;
    }
}
