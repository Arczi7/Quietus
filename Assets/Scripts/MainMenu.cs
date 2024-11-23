using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settings;

    public void StartGame()
    {
        SceneManager.LoadScene("Scene01");
    }

    public void ToggleSettings(bool toggle)
    {
        settings.SetActive(toggle);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
