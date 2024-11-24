using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Score score;
    [Header("Player Sounds")]
    [SerializeField] private AudioClip playerHurt;
    [SerializeField] private AudioClip playerGameOver;
    void Update()
    {
        if(health <= 0)
        {
            AudioManager.Instance.PlayPlayerEffect(playerGameOver);
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("SceneDeath");
        }
    }

    public void HealPlayer(int heal)
    {
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void DamagePlayer(int damage)
    {
        AudioManager.Instance.PlayPlayerEffect(playerHurt);
        health -= damage;
    }

    public void AddScore(float value)
    {
        score.AddScore(value);
    }

    public void AddLevel()
    {
        score.AddLevel();
    }

    //--GETTERY--//
    public float GetHealth()
    {
        return health;
    }

    public float GetScore()
    {
        return score.GetScore();
    }

    public float GetLevel()
    {
        return score.GetLevel();
    }
}
