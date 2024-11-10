using System.Collections;
using System.Collections.Generic;
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
        if(Input.GetKeyDown(KeyCode.Alpha1) && !pistol.activeSelf && !rifle.GetComponent<Gun>().IsReloading())
        {
            rifle.SetActive(false);
            rifle.GetComponent<Gun>().ActiveShoot();
            pistol.SetActive(true);
            UIManager.SetGun(pistol);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && !rifle.activeSelf && !pistol.GetComponent<Gun>().IsReloading())
        {
            pistol.SetActive(false);
            pistol.GetComponent<Gun>().ActiveShoot();
            rifle.SetActive(true);
            UIManager.SetGun(rifle);
        }
    }
}
