using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Score/Score")]
public class Score : ScriptableObject
{
    private float score;
    private float level = 1;
    public void AddScore(float value)
    {
        score += value;
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
