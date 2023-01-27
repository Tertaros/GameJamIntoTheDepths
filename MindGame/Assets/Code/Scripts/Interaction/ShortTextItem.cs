using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortTextItem : MonoBehaviour
{
    [Header("Parameters")]
    public string m_shortText = "";

    // Start is called before the first frame update
    void Start()
    {
        if (m_shortText.Length <= 0)
        {
            Debug.LogWarning("ShortTextItem [" + gameObject.name + "] has no short text. ");
        }
    }
}
