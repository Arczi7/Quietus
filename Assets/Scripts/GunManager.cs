using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rifle;
    [SerializeField] private UIManager UIManager;
    void Start()
    {
        pistol.SetActive(true);
        UIManager.SetGun(pistol);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && !pistol.activeSelf && !rifle.GetComponent<Gun>().Reloading)
        {
            rifle.SetActive(false);
            rifle.GetComponent<Gun>().ActiveShoot();
            pistol.SetActive(true);
            UIManager.SetGun(pistol);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && !rifle.activeSelf && !pistol.GetComponent<Gun>().Reloading)
        {
            pistol.SetActive(false);
            pistol.GetComponent<Gun>().ActiveShoot();
            rifle.SetActive(true);
            UIManager.SetGun(rifle);
        }
    }

    public GameObject Pistol
    {
        get => pistol;
    }

    public GameObject Rifle
    {
        get => rifle;
    }
}
