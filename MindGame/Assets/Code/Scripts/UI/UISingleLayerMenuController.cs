using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISingleLayerMenuController : MonoBehaviour
{
    public void Enable()
    {
        gameObject.SetActive(true);
        GameplayManager.Instance.PauseGame();
    }

    public void Disable()
    {
        GameplayManager.Instance.ResumeGame();
        gameObject.SetActive(false);
    }
}
