using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [Header("Death Screen Settings")]
    [SerializeField] private Score score;
    [SerializeField] private Text deathScore;
    [SerializeField] private Text deathLevels;
    [SerializeField] private Text deathFinalScore;
    void Start()
    {
        AudioManager.Instance.StopMusic();
        UIFinalScore();
    }

    private void UIFinalScore()
    {
        deathScore.text = "SCORE: " + score.GetScore().ToString();
        deathLevels.text = "LEVELS: " + score.GetLevel().ToString();
        float finalScore = score.GetScore() * score.GetLevel();
        deathFinalScore.text = "FINAL SCORE: " + finalScore.ToString();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("SceneMenu");
    }

}
