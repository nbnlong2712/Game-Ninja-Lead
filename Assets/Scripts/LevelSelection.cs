using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] bool unlocked;
    public Image unlockImage;
    
    void Start()
    {
        if(PlayerPrefs.GetInt("Level 1") == 0)
        {
            PlayerPrefs.SetInt("Level 1", 1);
        }
    }

    void Update()
    {
        UpdateLevelImage();
        UpdateLevelState();
    }

    void UpdateLevelState()
    {
        if (PlayerPrefs.GetInt(gameObject.name) > 0)
            unlocked = true;
    }

    void UpdateLevelImage()
    {
        if(!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
        }
        else
        {
            unlockImage.gameObject.SetActive(false);
        }
    }

    
}
