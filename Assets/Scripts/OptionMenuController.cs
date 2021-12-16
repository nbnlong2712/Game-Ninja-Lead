using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour
{
    [Header("Game Sound")]
    [SerializeField] AudioMixer audioMixerGame;
    [SerializeField] float volumeGame;
    [SerializeField] Slider sliderGameSound;
    [Header("Character Sound")]
    [SerializeField] AudioMixer audioMixerCharacter;
    [SerializeField] float volumeCharacter;
    [SerializeField] Slider sliderCharacterSound;
    [Header("Full Screen Toggle")]
    [SerializeField] Toggle toggleFullScreen;

    public static OptionMenuController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        string pathGame = Path.Combine(Application.persistentDataPath, "musicGame.txt");
        if (File.Exists(pathGame))
        {
            string volumeStr = File.ReadAllText(pathGame);
            volumeGame = float.Parse(volumeStr);
        }

        string pathCharacter = Path.Combine(Application.persistentDataPath, "musicCharacter.txt");
        if (File.Exists(pathCharacter))
        {
            string volumeStr = File.ReadAllText(pathCharacter);
            volumeCharacter = float.Parse(volumeStr);
        }

        ChangeVolumeGame(volumeGame);
        ChangeVolumeCharacter(volumeCharacter);
    }

    public void ChangeVolumeGame(float volume)
    {
        sliderGameSound.value = volume;
        string path = Path.Combine(Application.persistentDataPath, "musicGame.txt");
        audioMixerGame.SetFloat("volumeGame", volume);
        File.WriteAllText(path, volume.ToString());
    }

    public void ChangeVolumeCharacter(float volume)
    {
        sliderCharacterSound.value = volume;
        string path = Path.Combine(Application.persistentDataPath, "musicCharacter.txt");
        audioMixerCharacter.SetFloat("volumeCharacter", volume);
        File.WriteAllText(path, volume.ToString());
    }

    public void DropDownChange(int value)
    {
        if (value == 0)
            Screen.SetResolution(3840, 2160, toggleFullScreen.isOn);
        else if (value == 1)
            Screen.SetResolution(2560, 1440, toggleFullScreen.isOn);
        else if (value == 2)
            Screen.SetResolution(1366, 768, toggleFullScreen.isOn);
    }
}
