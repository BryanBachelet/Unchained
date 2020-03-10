﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationPlayerStates : MonoBehaviour
{
    public enum Palier { Palier0, Palier1, Palier2, Tranformation1, Palier4, Palier5, Transformation2 }

    public static Palier currentPalier = Palier.Palier0;
    
   
    public int[] palierCondition = new int[7];


    private KillCountPlayer countPlayer;
    [HideInInspector]
    public int palierStep;


    private PlayerMoveAlone playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMoveAlone>();
        countPlayer = GetComponentInChildren<KillCountPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {

            CheckState();
        }
    }

   public void CheckState()
    {
        if (countPlayer.countKillEnnemi > palierCondition[palierStep])
        {
            ChangeStates();
        }
    }

    public void ChangeStates()
    {
        currentPalier++;
        palierStep++;
        if (palierStep % 3 == 0)
        {
            GoTranformation();
        }
    }

    public void GoTranformation()
    {
        playerMove.GoTransformation();
        StateOfGames.currentState = StateOfGames.StateOfGame.Transformation;

    }

}
