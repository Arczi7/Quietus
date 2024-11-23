using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [Header("Pick Up Item Settings")]
    [SerializeField] private ItemType type;
    private enum ItemType {health, pistolAmmo, rifleAmmo}
    [SerializeField] private int value;
    [SerializeField] private float rotationSpeed;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float floatOffset = Mathf.Sin(Time.time * 1f) * 0.25f;
        transform.position = startPosition + new Vector3(0, floatOffset, 0);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(type == ItemType.health)
            {
                other.GetComponent<PlayerStats>().HealPlayer(value);
                Destroy(gameObject);
            }
            else if(type == ItemType.pistolAmmo)
            {
                other.GetComponent<GunManager>().Pistol.GetComponent<Gun>().AddAmmo(value);
                Destroy(gameObject);
            }
            else if(type == ItemType.rifleAmmo)
            {
                other.GetComponent<GunManager>().Rifle.GetComponent<Gun>().AddAmmo(value);
                Destroy(gameObject);
            }
        }
    }

}
