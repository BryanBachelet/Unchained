using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransformation : MonoBehaviour
{

    public GameObject shockWaveTransformation;
    private KillCountPlayer countOfKill;
    private StepOfPlayerStates playerStates;
    private ProgressionOfPlayer progressionPlayer;
    private EnnemiStock ennemiStock;
    private PlayerMoveAlone moveAlone;
    private Explosion explosion;
    public float pourcentOfState;
    public bool activePropulsion;

    public Image imgTransformation;
    public Image imgPropulsion;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

        pourcentOfState = countOfKill.count / playerStates.arrayOfKill[playerStates.currentStates];
        if (pourcentOfState > 0.5f)
        {
            imgTransformation.color = Color.white;
        }
        else
        {
            imgTransformation.color = Color.black;
        }
        if (pourcentOfState > 0.6f)
        {
            imgPropulsion.color = Color.blue;
        }
        else
        {
            imgPropulsion.color = Color.black;
        }
        if (Input.GetKey(KeyCode.Joystick1Button4) && Input.GetKey(KeyCode.Joystick1Button5) && !activePropulsion)
        {

            if (pourcentOfState > 0.5f && !activePropulsion)
            {
                if (playerStates.currentStates < playerStates.arrayOfKill.Length - 1)
                {
                    playerStates.currentStates++;
                    KillCountPlayer.CleanArray();
                }

                Instantiate(shockWaveTransformation, transform.position, Quaternion.Euler(-90, 0, 0));
                activePropulsion = true;

                progressionPlayer.ChangeState(true);
                if (pourcentOfState > 0.6f)
                {

                    if (ennemiStock.ennemiStock != null)
                    {
                        ennemiStock.DetachPlayer();
                    }
                    if (ennemiStock.ennemiStock == null)
                    {
                        moveAlone.AddProjection();
                    }

                    
                }
                if (pourcentOfState > 0.85f)
                {
                    explosion.ExplosionTransformation();
                }
                if (pourcentOfState > 0.95f)
                {

                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button4) || Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            if (activePropulsion)
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
