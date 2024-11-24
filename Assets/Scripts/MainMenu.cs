using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Settings Panel")]
    [SerializeField] private GameObject settings;
    [SerializeField] private Slider backgroundMusicVolume;
    [SerializeField] private Slider effectsVolume;
    [SerializeField] private Slider playerEffectsVolume;
    [Header("Credtis Panel")]
    [SerializeField] private GameObject credits;
    private bool settingsOpen = false;

    void Start()
    {
        AudioManager.Instance.BackgroundMusicManager(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene01");
    }

    public void ToggleSettings(bool toggle)
    {
        settings.SetActive(toggle);
        backgroundMusicVolume.value = AudioManager.Instance.MusicVolume;
        effectsVolume.value = AudioManager.Instance.EffectsVolume;
        playerEffectsVolume.value = AudioManager.Instance.PlayerEffectsVolume;
        settingsOpen = toggle;
    }

    public void SettingsMusicVolumeChange()
    {
        if(settingsOpen)
        {
            AudioManager.Instance.MusicVolume = backgroundMusicVolume.value;
            AudioManager.Instance.EffectsVolume = effectsVolume.value;
            AudioManager.Instance.PlayerEffectsVolume = playerEffectsVolume.value;
            AudioManager.Instance.UpdateVolumes();            
        }
    }

    public void SettingsTestVolume(int value)
    {
        Debug.Log(Resources.Load<AudioClip>("Sounds/PistolSound"));
        if(value == 0)
        {
            AudioManager.Instance.PlayEffect(Resources.Load<AudioClip>("Sounds/PistolSound"));
        }
        else if(value == 1)
        {
            AudioManager.Instance.PlayPlayerEffect(Resources.Load<AudioClip>("Sounds/PlayerDamage"));
        }
    }

    public void ToggleCredits(bool toggle)
    {
        credits.SetActive(toggle);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
