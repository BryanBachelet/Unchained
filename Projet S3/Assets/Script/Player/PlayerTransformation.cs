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
    private Explosion explosion;
    public float pourcentOfState;
    private bool activePropulsion;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

        pourcentOfState = countOfKill.count / playerStates.arrayOfKill[playerStates.currentStates];

        if (Input.GetKey(KeyCode.Joystick1Button4) && Input.GetKey(KeyCode.Joystick1Button5))
        {


            if (pourcentOfState > 0.5f)
            {
                Debug.Log(0);
                progressionPlayer.ChangeState(true);
                if (!activePropulsion)
                {
                    activePropulsion = true;
                }
                if (pourcentOfState > 0.6f)
                {
                    Debug.Log("EnnemiStock = " + ennemiStock.ennemiStock);
                    if (ennemiStock.ennemiStock != null)
                    {
                        Debug.Log("rotate");
                        ennemiStock.DetachPlayer();
                    }
                    if (ennemiStock.ennemiStock == null)
                    {
                        moveAlone.AddProjection();
                    }
                    Debug.Log(1);

                }
                if (pourcentOfState > 0.85f)
                {
                    Debug.Log(2);
                    explosion.ExplosionTransformation();
                }
                if (pourcentOfState > 0.95f)
                {

                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button4) && Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            if (!activePropulsion)
            {

                activePropulsion = false;
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
        explosion = GetComponent<Explosion>();
    }
}
