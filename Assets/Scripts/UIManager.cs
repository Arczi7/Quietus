using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI - Ammunition")]
    [SerializeField] private Text ammoText;
    [SerializeField] private Image ammoSprite;
    private Gun activeGun;
    void Update()
    {
        UIAmmo();
    }

    private void UIAmmo()
    {
        ammoText.text = activeGun.GetCurrentAmmo().ToString() + "/" + activeGun.GetMaxAmmo().ToString();
        ammoSprite.sprite = activeGun.GetAmmoSprite();
    }

    public void SetGun(GameObject gun)
    {
        activeGun = gun.GetComponent<Gun>();
    }
}
