using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingProp : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private Vector3 position;
    private Quaternion rotation;

    void Awake()
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    void Update()
    {
        if(gameManager.HowManyEnemies == 0)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}
