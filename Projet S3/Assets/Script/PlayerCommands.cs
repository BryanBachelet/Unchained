﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject PlayerTwo;

    public static GameObject player1;
    public static GameObject player2;



    public static GameObject OtherPlayer(GameObject player)
    {
        if (player == player1)
        {
            return player2;
        }
        else
        {
            return player1;
        }
    }

    public static void ChangeOpportunityState(GameObject player, PlayerState.OpportunityState state)
    {
        PlayerState currentState = player.GetComponent<PlayerState>();
        currentState.opportunityState = state;
    }
    public static void ChangeOpportunityState(GameObject player, PlayerState.OpportunityState state, Color color)
    {
        PlayerState currentState = player.GetComponent<PlayerState>();
        currentState.opportunityState = state;
        player.GetComponent<Renderer>().material.color = color;
    }

    public static void ActiveOpportunityWindow(GameObject player)
    {
        Opportunity opportunity = player.GetComponent<Opportunity>();
        opportunity.activeInput = true;
    }

    public static void ChangePlayerState(GameObject player, PlayerState.StateOfPlayer state)
    {
        
        PlayerState currentState = player.GetComponent<PlayerState>();
        currentState.playerState = state;
    }

    public static void ChangePlayerState(GameObject player, PlayerState.StateOfPlayer state, Color color)
    {
        PlayerState currentState = player.GetComponent<PlayerState>();
        currentState.playerState = state;
        player.GetComponent<Renderer>().material.color = color;
    }

    void Awake()
    {
        player1 = playerOne;
        player2 = PlayerTwo;
    }



}
