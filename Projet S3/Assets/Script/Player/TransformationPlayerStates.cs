using System.Collections;
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

    [FMODUnity.EventRef]
    public string attractSound;
    [FMODUnity.EventRef]
    public string expulseSound;
    bool expulseSoundPlay = false;
    bool isTransforming = false;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMoveAlone>();
        countPlayer = GetComponentInChildren<KillCountPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!expulseSoundPlay && StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable && palierStep == 3)
        {
            expulseSoundPlay = true;
            FMODUnity.RuntimeManager.PlayOneShot(expulseSound);
        }
        if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {

            CheckState();
        }
    }

    public void CheckState()
    {
        if (palierStep < palierCondition.Length - 1)
        {
            if (countPlayer.countKillEnnemi > palierCondition[palierStep])
            {
                ChangeStates();
            }
        }
        

    }

    public void ChangeStates()
    {
        currentPalier++;
        palierStep++;
        if (palierStep % 3 == 0)
        {
            GoTranformation();
            FMODUnity.RuntimeManager.PlayOneShot(attractSound);

        }
    }

    public void GoTranformation()
    {
        playerMove.GoTransformation();
        StateOfGames.currentState = StateOfGames.StateOfGame.Transformation;

    }

}
