﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbillon : MonoBehaviour
{
    private Transform otherPlayer;
    private float angleRotated;
    private PlayerNumber playerNumber;
    private PlayerMouvement playerMouvement;


    private float ghostAngleRotated;  ///////////////////////

    private bool isRotate = false;
    private bool ghostIsRotate = false;  ///////////////////////
    public float angleToRotateMinimum = 360;
    private float angleToRotate;
    private int rotationSens = 1;
    public float angleSpeed = 180;
    public float ratioAugmented;

    [Range(0, 1f)] public float opportunityWindow = 0.5f;
    private string playerIdentity;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        playerMouvement = GetComponent<PlayerMouvement>();
        otherPlayer = PlayerCommands.OtherPlayer(gameObject).transform;
        playerIdentity = "Player" + playerNumber.playerNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && playerIdentity == "Player1")
        {
            LaunchTourbillon();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && playerIdentity == "Player2")
        {
            LaunchTourbillon();
        }


        if (Input.GetKeyDown("joystick " + playerNumber.manetteNumber.ToString() + " button 4"))
        {
            LaunchTourbillon();

        }
        if (isRotate)
        {
            TourbillonSkill();
            if (angleRotated >= angleToRotate)
            {
                PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.Out);
                isRotate = false;
            }
        }
    }

    public void LaunchTourbillon()
    {
        if (!isRotate)
        {
            angleRotated = 0;
            angleToRotate = angleToRotateMinimum + VitesseFunction.RatioAugmented(ratioAugmented);
            PlayerCommands.ActiveOpportunityWindow(gameObject);
            isRotate = true;
            CheckOrientation();
        }
    }

    public void CheckOrientation()
    {
        Debug.Log(playerMouvement.horizontal);
        if (playerMouvement.horizontal >= 0)
        {

            rotationSens = 1;
        }
        if(playerMouvement.horizontal<0)
        {
            rotationSens = -1;
        }
        
    } 


    public void TourbillonSkill()
    {
        angleRotated += angleSpeed * Time.deltaTime;
        transform.RotateAround(otherPlayer.position, Vector3.up, rotationSens * angleSpeed * Time.deltaTime);

        float currentDist = angleRotated / angleToRotate;
        if (currentDist > opportunityWindow)
        {
            PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.In);
        }
    }
}

