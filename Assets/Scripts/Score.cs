using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Score/Score")]
public class Score : ScriptableObject
{
    [SerializeField] private float score;
    [SerializeField] private float level = 1;
    public void AddScore(float value)
    {
        score += value;
    }

    public void ZeroScore()
    {
        score = 0;
    }

    public void SetLevel(float value)
    {
        level = value;
    }

    public float GetScore()
    {
        return score;
    }

    public float GetLevel()
    {
        return level;
    }
}
