using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Panels")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsPanel;
    [Header("Settings - Volume Sliders")]
    [SerializeField] private Slider backgroundMusicVolume;
    [SerializeField] private Slider effectsVolume;
    [SerializeField] private Slider playerEffectsVolume;

    private bool pause = false;
    private bool settingsOpen = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        pause = true;
        AudioManager.Instance.PauseMusic();
        ToggleSettings(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pause = false;
        AudioManager.Instance.ResumeMusic();
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void ToggleSettings(bool toggle)
    {
        settingsPanel.SetActive(toggle);
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
        if(value == 0)
        {
            AudioManager.Instance.PlayEffect(Resources.Load<AudioClip>("Sounds/PistolSound"));
        }
        else if(value == 1)
        {
            AudioManager.Instance.PlayPlayerEffect(Resources.Load<AudioClip>("Sounds/PlayerDamage"));
        }
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SceneMenu");
    }

    public bool Pause
    {
        get => pause;
    }

}
