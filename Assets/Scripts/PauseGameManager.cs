using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    public static bool gameIsPause = false;
    [SerializeField] GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPause)
                Resume();
            else Pause();
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }
    public void LoadJourney()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("JourneyMap");
        print("1 + 1 = 2");
        Invoke("LoadMain", 2);
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}