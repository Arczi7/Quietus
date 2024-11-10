using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackground : MonoBehaviour
{
    [SerializeField] private Image movingLayer;
    [SerializeField] private float leftBorderX;
    [SerializeField] private float rightBorderX;
    [SerializeField] private float speed;
    private float direction = 1;
    void Update()
    {
        movingLayer.transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        Debug.Log(movingLayer.transform.localPosition.x);
        if(movingLayer.transform.localPosition.x >= rightBorderX)
        {
            direction = -1;
        }
        else if(movingLayer.transform.localPosition.x <= leftBorderX)
        {
            direction = 1;
        }
    }
}
