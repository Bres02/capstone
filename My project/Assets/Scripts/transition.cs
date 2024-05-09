using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transition : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool gamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            if(pauseMenu != null)
            {
                gamePaused = true;
                Pause();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused)
        {
            if (pauseMenu != null)
            {
                gamePaused = false;
                Resume();
            }

        }

    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void info()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void startgame()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    public void exit()
    {
        Application.Quit();
        Time.timeScale = 1;
    }
}
