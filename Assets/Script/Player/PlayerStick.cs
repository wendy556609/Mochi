﻿// #define JOYSTICK
// #define HOLDSTICK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStick : MonoBehaviour
{

    PlayerMovement playerMovement;
    UnityJellySprite jellySprite;
    public StickDetect stickDetect;

    //

    public bool getIsOnFloor;
    // Use this for initialization
    [SerializeField]
    public bool isStick { get { return m_isStick; } }

    [SerializeField]
    public bool m_isStick;  //黏的狀態

    [SerializeField]
    public bool canStick;  //有碰到東西可以黏


    [SerializeField]
    private bool isPointAttachItem;  //有碰到Item

    [SerializeField]
    public List<GameObject> stickItemList;  //黏住的角色

    [SerializeField]
    private List<GameObject> pointAttachPlayerList;  //有碰到角色

    [SerializeField]
    public List<GameObject> stickPlayerList = new List<GameObject>();  //黏住的角色

    // [SerializeField]
    // public List<GameObject> touchPlayerList;  //黏住的角色



    [SerializeField]
    public bool isPointAttachWall;  //有碰到wall


    [SerializeField]
    private bool isTouchGround;  //有碰到Item

    [SerializeField]
    private bool isPointAttachGround;  //有碰到Ground


    bool isStickRocket;

    //
    [SerializeField]
    public bool isPop;
    //

    void Start()
    {
        jellySprite = gameObject.GetComponent<UnityJellySprite>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        stickDetect = gameObject.GetComponentInChildren<StickDetect>();
        isPop = false;
        jellySprite.notFreeze = false;
    }
    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            if (!playerMovement.isDead)
            {
                AttachDetect();

                if (stickDetect.isTouchWall || isPointAttachGround)
                {
                    if (isPop)
                    {
                        isPop = false;
                    }
                }

                jellySprite.SetAnimBool("isStick", m_isStick);

                CanStick();

                if (m_isStick)
                {
                    ItemToStick();
                    PlayerToStick();
                }
            }
            else
            {
                DieReset();
            }
        }
    }

    public void SetStick()
    {
        stickDetect.SetDetect(true);
        m_isStick = true;
        jellySprite.SetAnimBool("isWalk", false);
    }
    public void ResetStick()
    {
        ResetNotStick_Normal();
    }

    void DieReset()
    {

        m_isStick = false;
        jellySprite.SetAnimBool("isWalk", false);
        ResetNotStick_Normal();

        isPop = false;

        jellySprite.SetAnimBool("isStick", false);

        canStick = false;

    }

    private void ItemToStick()
    {
        stickItemList = jellySprite.SetItemStick(stickDetect.touchItemList);

        isStickRocket = jellySprite.isStickRocket;

        if (isStickRocket)
            RocketStick();

        if (stickDetect.isTouchWall && isPointAttachWall)
            jellySprite.SetWallStick();

        if (getIsOnFloor || isPointAttachGround)
        {
            jellySprite.SetFloorStick();
        }
    }


    public void ResetItemNotStick()
    {
        stickItemList = null;
        jellySprite.ResetItemStick();
    }

    public void ResetFloorStick()
    {
        jellySprite.ResetFloorStick();
    }

    public void ResetWallStick()
    {
        jellySprite.ResetWallStick();
    }

    private void PlayerToStick()
    {
        if (pointAttachPlayerList != null && stickDetect.isTouchPlayer)
        {
            foreach (var pointAttachPlayer in pointAttachPlayerList)
            {
                if (!stickPlayerList.Contains(pointAttachPlayer) && !pointAttachPlayer.GetComponent<PlayerStick>().isPop)
                {
                    stickPlayerList.Add(pointAttachPlayer);
                }
            }

            if (stickPlayerList != null)
                jellySprite.SetPlayerStick(stickPlayerList);
        }
    }


    public void ResetPlayersNotStick()
    {
        jellySprite.ResetPlayersStick(stickPlayerList);
        stickPlayerList.Clear();
    }


    public void ResetThePlayersNotStick(List<GameObject> popPlayerList)
    {
        foreach (var player in popPlayerList)
        {
            stickPlayerList.Remove(player);
            jellySprite.ResetThePlayeStick(player);
        }
    }


    public void ResetThePlayersNotStick(GameObject player)
    {
        if (player != null && stickPlayerList != null)
        {
            if (stickPlayerList.Contains(player))
            {
                stickPlayerList.Remove(player);
                jellySprite.ResetThePlayeStick(player);
            }
        }
    }

    public void ResetNotStick_Normal()
    {
        m_isStick = false;

        ResetItemNotStick();

        ResetPlayersNotStick();

        ResetFloorStick();

        ResetWallStick();
    }

    public void ResetNotStick_PopWithPlayer()
    {
        m_isStick = false;

        ResetItemNotStick();

        ResetPlayersNotStick();
    }

    void RocketStick()
    {
        LevelController.instance.SetRocketStick(this.gameObject, isStickRocket, playerMovement.playerColor);
    }

    void AttachDetect()
    {
        isPointAttachItem = jellySprite.GetIsItemAttach();

        pointAttachPlayerList = jellySprite.GetPlayersAttach();

        isPointAttachWall = jellySprite.GetIsWallAttach();

        isPointAttachGround = jellySprite.GetIsFloorAttach();
    }

    void CanStick()
    {
        if (((getIsOnFloor || isPointAttachGround) || (isPointAttachWall && stickDetect.isTouchWall) || pointAttachPlayerList.Count != 0 || (isPointAttachItem && stickDetect.touchItemList.Count != 0)))
        {
            canStick = true;
        }
        else
        {
            canStick = false;
        }
    }
}
