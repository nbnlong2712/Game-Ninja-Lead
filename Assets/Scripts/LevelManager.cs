using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadAboutUs()
    {
        SceneManager.LoadScene("About");
    }

    public void LoadInstruction()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void LoadJourneyMap()
    {
        SceneManager.LoadScene("JourneyMap");
    }

    public void LoadGameWin()
    {
        StartCoroutine(LoadNewScene("GameWin"));
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadNewScene("GameOver"));
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }

    IEnumerator LoadNewScene(string name)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
