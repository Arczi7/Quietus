using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI - Ammunition")]
    [SerializeField] private Text currentAmmoText;
    [SerializeField] private Text maxAmmoText;
    [SerializeField] private Image ammoSprite;
    [Header("UI - Health & Score")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Text healthText;
    [SerializeField] private Text scoreText;
    [Header("Crosshair")]
    [SerializeField] private Image crosshairBorder;
    [Header("Texts")]
    [SerializeField] private Text newLevelText;
    private Gun activeGun;
    void Update()
    {
        UIAmmo();
        UIHealth();
        UIScore();
    }

    private void UIAmmo()
    {
        currentAmmoText.text = activeGun.GetCurrentAmmo().ToString();
        maxAmmoText.text = activeGun.GetMaxAmmo().ToString();
        ammoSprite.sprite = activeGun.GetAmmoSprite();
    }

    private void UIHealth()
    {
        healthText.text = playerStats.GetHealth().ToString();
    }

    private void UIScore()
    {
        scoreText.text = playerStats.GetScore().ToString();
    }

    private IEnumerator LevelText()
    {
        newLevelText.text = "LEVEL " + playerStats.GetLevel().ToString();
        newLevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        newLevelText.gameObject.SetActive(false);
    }

    public void SetGun(GameObject gun)
    {
        activeGun = gun.GetComponent<Gun>();
    }

    public void ShowLevelText()
    {
        StartCoroutine(LevelText());
    }

    public Image GetCrosshairBorder()
    {
        return crosshairBorder;
    }
}
