using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private Score score;
    void Start()
    {
        score.ZeroScore();
        GenerateNavMesh();
    }

    private void GenerateLevel()
    {
        
    }

    private void GenerateNavMesh()
    {
        surface.BuildNavMesh();
    }

}
