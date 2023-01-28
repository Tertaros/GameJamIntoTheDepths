using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrderScript : MonoBehaviour
{
    public GameObject door;
    private int[] correctOrder;
    private int currentIndex;

    void Start()
    {
        // Set the correct order of buttons here
        correctOrder = new int[] { 1, 2, 3 };
        currentIndex = 0;
    }

    public void CheckOrder(int switchIndex)
    {
        if (switchIndex == correctOrder[currentIndex])
        {
            currentIndex++;
            if (currentIndex == correctOrder.Length)
            {
                // Correct order was entered
                door.SetActive(false);
                currentIndex = 0;
            }
        }
        else
        {
            // Incorrect order was entered
            Debug.Log("Incorrect order!");
            currentIndex = 0;
            // reset all the light sources and switches
            Light[] lights = FindObjectsOfType<Light>();
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].gameObject.SetActive(false);
            }
            LightSwitch[] switches = FindObjectsOfType<LightSwitch>();
            for (int i = 0; i < switches.Length; i++)
            {
                switches[i].isActivated = false;
            }
        }
    }
}
