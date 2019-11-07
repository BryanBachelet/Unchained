﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{

    public float speedOfAttrack;
    private PlayerNumber playerNumber;
    private bool isAttract = false;
    private GameObject otherPlayer;

    [Range(0, 1f)] public float opportunityWindow;
    private float startDistance;
    private float currentDistance;
    private Vector3 startPos;
    public float percentDistance;


    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
    }

    // Update is called once per frame
    void Update()
    {
        float hit = Input.GetAxis("Attract" + playerNumber.playerNumber.ToString());
        if (hit != 0 && !isAttract)
        {
            isAttract = true;
            PlayerCommands.ActiveOpportunityWindow(gameObject);

            startPos = transform.position;
            otherPlayer = PlayerCommands.OtherPlayer(gameObject);
            startDistance = Vector3.Distance(transform.position, otherPlayer.transform.position);

        }

        if (isAttract)
        {
            AttractSkill();

        }
    }

    public void AttractSkill()
    {
        currentDistance = Vector3.Distance(startPos, transform.position);
        percentDistance = currentDistance / (startDistance-1);

        if (percentDistance > opportunityWindow)
        {
            PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.In);
        }


        transform.position = Vector3.MoveTowards(transform.position, otherPlayer.transform.position, speedOfAttrack * Time.deltaTime);
        if (Vector3.Distance(transform.position, otherPlayer.transform.position) < 1f)
        {
            isAttract = false;
            PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.Out);
        }
        
    }
}
