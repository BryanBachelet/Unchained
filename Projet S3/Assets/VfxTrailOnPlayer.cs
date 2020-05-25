using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxTrailOnPlayer : MonoBehaviour
{
    public bool activeBeforePhase3;
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
       
    }

    // Update is called once per frame
    void Update()
    {
        if(pmaPlayer.currentPowerOfProjection > 0 || esPlayer.ennemiStock != null )
        {
            if(StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
            {
                if(!activeBeforePhase3)
                {
                    transform.position = new Vector3(playerTransform.position.x, 2, playerTransform.position.z ) +-playerTransform.forward *1;
                }

            }
            else
            {
                if(activeBeforePhase3)
                {
                    transform.position = new Vector3(playerTransform.position.x, 2, playerTransform.position.z);
                }
            }
         
        }

    }
}
