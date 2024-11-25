using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    private Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        healthBar.transform.localScale = new Vector3(enemy.Health, 1, 1);
    }

    public void UpdateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(enemy.Health, 1, 1);
    }
}
