﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Tilemaps;

public class CircleGround : MonoBehaviour
{
    public float rotateDeg;
    float rotateAngle;
    public float r;

    public float h, k;

    public List<GameObject> childList;

    public GameObject sprite;

    List<Transform> transList;
    public List<Rigidbody2D> rbList;

    float eachDeg;


    float deg;

    void Start()
    {
        deg = 0;
        rotateAngle = (Mathf.PI / 180) * rotateDeg;

        eachDeg = 2f * Mathf.PI / childList.Count;

        int index = 0;
        foreach (var item in childList)
        {
            float x = r * Mathf.Cos(eachDeg * index) + h;
            float y = r * Mathf.Sin(eachDeg * index) + k;
            childList[index].transform.position = new Vector2(x, y);
            index++;
        }

        //tile式移動
        // tilemap = GetComponent<Tilemap>();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < childList.Count; i++)
        {
            Rotate(i);
        }

        sprite.transform.Rotate(new Vector3(0, 0, rotateDeg * 5.0f * Time.deltaTime));
    }

    void Rotate(int index)
    {
        Vector3 pos = childList[index].transform.position;

        MoveRigibody(index);
    }

    void MoveRigibody(int index)
    {
        deg += rotateAngle * Time.deltaTime;

        float x = r * Mathf.Cos(deg + eachDeg * index) + h;
        float y = r * Mathf.Sin(deg + eachDeg * index) + k;

        rbList[index].MovePosition(new Vector2(x, y));

        rbList[index].MoveRotation(deg * 57.5f);
    }
}

