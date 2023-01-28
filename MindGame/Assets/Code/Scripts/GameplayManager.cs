using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    private static int s_count = 0;
    private static GameplayManager s_instance = null;

    private void Awake()
    {
        s_count++;
        if(s_count>1)
        {
            Debug.LogError("More than 1 GameplayManager in scene!");
        }

        if(!s_instance)
        {
            s_instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Quit"))
        {
            LoadMainMenu();
        }
    }

    private void OnDestroy()
    {
        s_instance = null;
        s_count--;
    }

    public static GameplayManager Instance
    {
        get { return s_instance; }
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void LoadRoom1()
    {
        SceneManager.LoadScene("Room1", LoadSceneMode.Single);
    }
}
