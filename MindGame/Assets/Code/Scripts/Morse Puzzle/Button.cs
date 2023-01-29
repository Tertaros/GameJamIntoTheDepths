using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    public GameObject player;
    public AudioSource m_switch;
    public CheckLetters checkLetters;
    public bool IsEnter = false;
    public bool IsUp = false;
    public bool IsDown = false;
    public Button upbutton;
    public Button downbutton;
    public LetterController info;

    public TextMeshPro N;
    public TextMeshPro O;
    public TextMeshPro V;
    public TextMeshPro A;
    private char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
   
    void Start()
    {
        
    }
    void Update()
    {
        Physics.Raycast(player.transform.position, player.transform.forward, out var hit, 3f);

        if (Vector3.Distance(transform.position, player.transform.position) <= 2.75 && hit.collider.tag.Equals(this.tag))
        {
            if (Input.GetKeyDown(KeyCode.E)&& info.isActive)
            {
                m_switch.Play();

                if (IsEnter)
                {
                    info.letterIndex++;
                    checkLetters.CheckLetterOrder(alphabet[info.index]);

                    info.index = 0;
                }
                if (IsUp)
                {
                    if (info.index == 25)
                        info.index = 0;
                    else
                        info.index++;
                }
                if (IsDown)
                {

                    if (info.index == 0)
                        info.index = 25;
                    else
                        info.index--;
                }
                Debug.Log(alphabet[info.index]);

                if (info.letterIndex == 0)
                {
                    N.text = alphabet[info.index].ToString();
                }
                if (info.letterIndex == 1)
                {
                    O.text = alphabet[info.index].ToString();
                }
                if (info.letterIndex == 2)
                {
                    V.text = alphabet[info.index].ToString();
                }
                if (info.letterIndex == 3)
                {
                    A.text = alphabet[info.index].ToString();
                }

            }
        }

    }
}
