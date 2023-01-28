using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightSwitch : MonoBehaviour
{
    public int switchIndex;
    public GameObject lightSource;
    public bool isActivated = false;
    public CheckOrderScript checkOrderScript;
    public GameObject player;
 
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 2.75)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isActivated)
                {
                    isActivated = true;
                    lightSource.SetActive(true);
                    Debug.Log(lightSource.name);
                    checkOrderScript.CheckOrder(switchIndex);
                    Debug.Log(switchIndex);
                }
            }
        }

    }
}