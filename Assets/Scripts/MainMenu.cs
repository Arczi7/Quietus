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

    public void ShowSettings()
    {
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
