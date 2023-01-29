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
    public AudioSource m_switch;


    void Update()
    {
        Physics.Raycast(player.transform.position, player.transform.forward, out var hit,  2.75f);

        if(Vector3.Distance(transform.position, player.transform.position) <= 2.75 && hit.collider.tag.Equals(this.tag))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isActivated)
                {
                    isActivated = true;
                    lightSource.SetActive(true);
                    m_switch.Play();
                    checkOrderScript.CheckOrder(switchIndex);
                }
            }
        }

    }
}