using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckLetters : MonoBehaviour
{
    public char[] correctOrder = { 'N','O','V','A' };
    public GameObject img;
    public AudioSource wrong;
    private char[] playerOrder = { ' ', ' ', ' ', ' ' };
    private int currentIndex = 0;
   
    public void CheckLetterOrder(char switchIndex)
    {
        playerOrder[currentIndex] = switchIndex;
        currentIndex++;

        if (currentIndex == 4)
        {
            if (playerOrder.SequenceEqual(correctOrder))
            {
                // move photo
                img.SetActive(true);
                img.GetComponent<DrawerController>().Interact();
                GameObject[] up = GameObject.FindGameObjectsWithTag("up");
                up[0].GetComponent<Button>().info.isActive = false;

            }
            else
            {
                // Reset the switches
                currentIndex = 0;
                for (int i = 0; i < playerOrder.Length; i++)
                {
                    playerOrder[i] = ' ';
                }
                GameObject[] up = GameObject.FindGameObjectsWithTag("up");
                up[0].GetComponent<Button>().N.text = "A";
                up[0].GetComponent<Button>().O.text = "A";
                up[0].GetComponent<Button>().V.text = "A";
                up[0].GetComponent<Button>().A.text = "A";
                up[0].GetComponent<Button>().info.index = 0;
                up[0].GetComponent<Button>().info.letterIndex = 0;
                up[0].GetComponent<Button>().info.isActive = true;
                wrong.Play();
            }
        }
    }
}
