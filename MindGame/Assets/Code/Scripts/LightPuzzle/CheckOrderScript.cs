using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckOrderScript : MonoBehaviour
{
    public int[] correctOrder = { 1, 2, 3 };
    public GameObject door;
    public AudioSource open;
    public AudioSource wrong;
    private int[] playerOrder = { 0, 0, 0 };
    private int currentIndex = 0;
    private bool isOpen = false;
    private GameObject[] switches;

    public void CheckOrder(int switchIndex)
    {
        playerOrder[currentIndex] = switchIndex;
        currentIndex++;

        if (currentIndex == 3)
        {
            if (playerOrder.SequenceEqual(correctOrder))
            {
                // Open the door
                isOpen = true;
                door.transform.Rotate(new Vector3(0, 90, 0));
                open.Play();
            }
            else
            {
                // Reset the switches
                currentIndex = 0;
                for (int i = 0; i < playerOrder.Length; i++)
                {
                    playerOrder[i] = 0;
                }
                switches = GameObject.FindGameObjectsWithTag("Switch");
                foreach (var item in switches)
                {
                    var comp = item.GetComponent<LightSwitch>();
                    comp.isActivated = false;
                    comp.lightSource.SetActive(false);
                }
                wrong.Play();
            }
        }
    }
}
