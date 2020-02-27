using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    private KillCountPlayer countOfKill;
    private StepOfPlayerStates playerStates;
    private ProgressionOfPlayer progressionPlayer;
    private EnnemiStock ennemiStock;
    private PlayerMoveAlone moveAlone;
    private float pourcentOfState;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        pourcentOfState = countOfKill.count / playerStates.arrayOfKill[playerStates.currentStates];

        if (Input.GetKeyDown(KeyCode.Joystick1Button4) && Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if (pourcentOfState > 0.5f)
            {
                progressionPlayer.ChangeState(true);
            }
            if (pourcentOfState > 0.6f)
            {
                if (ennemiStock.ennemiStock)
                {
                    ennemiStock.DetachPlayer();
                }
                else
                {
                    moveAlone.AddProjection();
                }

                
            }
            if (pourcentOfState > 0.85f)
            {
                //Function Three
            }
            if (pourcentOfState > 0.95f)
            {
                // Function Forth
            }


        }
    }

    void Init()
    {
        countOfKill = GetComponentInChildren<KillCountPlayer>();
        playerStates = GetComponentInChildren<StepOfPlayerStates>();
        progressionPlayer = GetComponent<ProgressionOfPlayer>();
        ennemiStock = GetComponent<EnnemiStock>();
        moveAlone = GetComponent<PlayerMoveAlone>();
    }
}
