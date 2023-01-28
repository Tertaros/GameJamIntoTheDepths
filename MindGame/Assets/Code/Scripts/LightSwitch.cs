using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightSwitch : MonoBehaviour
{
    public int switchIndex;
    public GameObject lightSource;
    public bool isActivated = false;
    public CheckOrderScript checkOrderScript;

    public void OnMouseDown()
    {
        if (!isActivated)
        {
            isActivated = true;
            lightSource.SetActive(true);
            checkOrderScript.CheckOrder(switchIndex);
        }
    }
}