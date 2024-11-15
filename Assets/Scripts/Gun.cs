using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioManager audioSource;
    [SerializeField] private float range;
    [SerializeField] private float impactForce;
    [SerializeField] private float shootDelay;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private GameObject shootEffect;
    [Header("Gun&Ammo Settings")]
    [SerializeField] private float gunDamage;
    [SerializeField] private float reloadTime;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int maxClipAmmo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private Sprite ammoSprite;
    private enum ShootingType { single, auto}
    [SerializeField] private ShootingType gunShootingType;
    [Header("Gun Sounds")]
    [SerializeField] private AudioClip gunShoot;
    [SerializeField] private AudioClip gunReload;
    [SerializeField] private AudioClip gunNoAmmo;
    private Animator animator;
    private bool isShooting = false;
    private bool canShoot = true;
    private bool reloading = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(gunShootingType == ShootingType.single)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        else if(gunShootingType == ShootingType.auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        
        if(Input.GetKeyDown(KeyCode.R) && !reloading && maxAmmo > 0)
        {
            StartCoroutine(Reload());
        }

        if(isShooting)
        {
            if(canShoot && !reloading)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        canShoot = false;
        if(currentAmmo > 0)
        {
            currentAmmo -= 1;

            shootEffect.GetComponent<ParticleSystem>().Play();
            animator.SetTrigger("Shoot");
            audioSource.Play(gunShoot);
 
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            //FIZYKA I BULLETHOLE
            if(Physics.Raycast(ray, out hit, range, hitLayer))
            {
                if (bulletHolePrefab != null)
                {
                    GameObject bulletHole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(-hit.normal));
                    bulletHole.transform.SetParent(hit.transform);
                    Destroy(bulletHole, 3f);
                }

                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForceAtPosition(ray.direction * impactForce, hit.point, ForceMode.Impulse);
                }

                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.RemoveHealth(gunDamage);
                    Debug.Log("HIT!");
                }
            }

            //ENEMY
            if(Physics.Raycast(ray, out hit, range, enemyLayer))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.RemoveHealth(gunDamage);
                }
            }


            StartCoroutine(ShootDelay());
        }
        else if(currentAmmo == 0 && maxAmmo > 0)
        {
            StartCoroutine(Reload());
            canShoot = true;
        }
        else if(currentAmmo == 0 && maxAmmo == 0)
        {
            canShoot = true;
            audioSource.Play(gunNoAmmo);
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    private IEnumerator Reload()
    {
        canShoot = false;
        reloading = true;
        audioSource.Play(gunReload);
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        int neededAmmo = maxClipAmmo - currentAmmo;
        if(maxAmmo >= neededAmmo)
        {
            currentAmmo += neededAmmo;
            maxAmmo -= neededAmmo;
        }
        else
        {
            currentAmmo += maxAmmo;
            maxAmmo = 0;
        }
        reloading = false;
        canShoot = true;
    }

    //GETTERY I SETTERY
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public Sprite GetAmmoSprite()
    {
        return ammoSprite;
    }

    public bool IsReloading()
    {
        return reloading;
    }

    public void ActiveShoot()
    {
        canShoot = true;
    }
}
