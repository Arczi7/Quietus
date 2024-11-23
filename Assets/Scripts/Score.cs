using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Score/Score")]
public class Score : ScriptableObject
{
    [SerializeField] private float score = 0;
    [SerializeField] private float level = 0;
    public void AddScore(float value)
    {
        score += value;
    }

    public void ZeroScore()
    {
        score = 0;
        level = 0;
    }


    public void AddLevel()
    {
        level += 1;
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
