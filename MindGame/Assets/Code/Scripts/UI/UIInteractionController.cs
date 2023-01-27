using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInteractionController : MonoBehaviour
{
    [Header("Parameters")]
    public TMPro.TMP_Text m_tmProInteract;
    public string m_textInspect;
    public string m_textCollect;
    public string m_textInspectAndCollect;
    public string m_textInteract;
    public TMPro.TMP_Text m_tmProShortText;
    public UIMenuInspectionController m_uiMenuInspectionController;

    // Start is called before the first frame update
    void Start()
    {
        if (m_tmProInteract == null) 
        {
            Debug.LogError("No interaction TMPro found!");
        }
        if (m_tmProShortText == null)
        {
            Debug.LogError("No short text TMPro found!");
        }
        if (m_uiMenuInspectionController == null)
        {
            Debug.LogError("No UIMenuInspectionController found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableInteractText(InteractableItem item)
    {
        bool enable = true;

        if(item.m_inspectableItem && item.m_collectableItem)
        {
            m_tmProInteract.text = m_textInspectAndCollect;
        }
        else if(item.m_inspectableItem)
        {
            m_tmProInteract.text = m_textInspect;
        }
        else if(item.m_collectableItem)
        {
            m_tmProInteract.text = m_textCollect;
        }
        else if(item.m_triggerableItem)
        {
            if(item.m_triggerableItem.IsTriggerable())
            {
                m_tmProInteract.text = m_textInteract;
            }
            else
            {
                enable = false;
            }
        }
        else
        {
            enable = false;
        }

        m_tmProInteract.enabled = enable;
    }

    public void DisableInteractText()
    {
        m_tmProInteract.enabled = false;
    }

    public void ShowShortText(string shortText)
    {
        m_tmProShortText.text = shortText;
        m_tmProShortText.enabled = true;
    }

    public void DisableShortText()
    {
        m_tmProShortText.enabled = false;
        m_tmProShortText.text = "";
    }

    public void OnInteractWithInspectable(InspectableItem inspectable)
    {
        m_uiMenuInspectionController.Setup(inspectable.m_inspectView);
        m_uiMenuInspectionController.Enable();
    }
}
