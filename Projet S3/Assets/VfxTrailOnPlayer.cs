﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxTrailOnPlayer : MonoBehaviour
{
    Transform playerTransform;
    PlayerMoveAlone pmaPlayer;
    EnnemiStock esPlayer;
    PlayerFall pfPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerMoveAlone.Player1.transform;
        pmaPlayer = playerTransform.GetComponent<PlayerMoveAlone>();
        esPlayer = playerTransform.GetComponent<EnnemiStock>();
        // pfPlayer = playerTransform.GetComponent<PlayerFall>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pmaPlayer.currentPowerOfProjection > 0 && StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3 || esPlayer.ennemiStock != null && StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
        transform.position = new Vector3(playerTransform.position.x, 2, playerTransform.position.z);
           // transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
          //  transform.GetChild(0).gameObject.SetActive(false);
        }
        // if(pfPlayer.activeFall)
        // {
        //     gameObject.SetActive(false);
        // }


    }
}
