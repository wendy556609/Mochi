﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : MoveTile
{

    public GameObject[] btn = new GameObject[2];

    TileButton[] btnScript = new TileButton[2];

    void Start()
    {
        btnScript[0] = btn[0].GetComponentInChildren<TileButton>();
        btnScript[1] = btn[1].GetComponentInChildren<TileButton>();

        trans = this.gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 pos = trans.position;

        if (trans.position.x < minVec.x || trans.position.y < minVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            btnScript[0].SetIsMove(false);
            btnScript[1].SetIsMove(true);
            if (!isContinue)
            {
                if (!isMax)
                    Invoke("SetCanMove", minStopSec);
            }
            else
                Invoke("SetCanMove", minStopSec);
        }

        else if (trans.position.x > maxVec.x || trans.position.y > maxVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            btnScript[0].SetIsMove(true);
            btnScript[1].SetIsMove(false);
            if (!isContinue)
            {
                if (!isMax)
                    Invoke("SetCanMove", maxStopSec);
            }
            else
                Invoke("SetCanMove", maxStopSec);
        }

        newPos = pos + moveSpeed;

        trans.position = newPos;
    }


}