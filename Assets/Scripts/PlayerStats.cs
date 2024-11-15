using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private Score score;
    void Update()
    {
        if(health <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("SceneDeath");
        }
    }

    public void HealPlayer(float heal)
    {
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void DamagePlayer(float damage)
    {
        Debug.Log(health);
        health -= damage;
    }

    public void AddScore(float value)
    {
        score.AddScore(value);
    }

    public void SetLevel(float value)
    {
        score.SetLevel(value);
    }

    //GETTERY
    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetScore()
    {
        return score.GetScore();
    }
}
