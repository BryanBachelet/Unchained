using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationPlayerStates : MonoBehaviour
{
    public enum Palier { Palier0, Palier1, Palier2, Tranformation1, Palier4, Palier5, Transformation2 }

    public static Palier currentPalier = Palier.Palier0;


    public int[] palierCondition = new int[7];


    private KillCountPlayer countPlayer;

    public int palierStep;


    private PlayerMoveAlone playerMove;

    [FMODUnity.EventRef]
    public string attractSound;
    [FMODUnity.EventRef]
    public string expulseSound;
    bool expulseSoundPlay = false;
    bool isTransforming = false;

    private MashingFeedback mash;

    private PlayerAnimState playerAnim;

    public float timeActivePlayerAnim = 1;

    private float compteurTime;

    private bool activePanel;


    public float tempsAvantCheckLoop1 = 20.57f;
    public float tempsEcouleCheckLoop1;
    bool isActivable;
    public float tempsAvantCheckLoop2 = 42.9f;
    public float tempsEcouleCheckLoop2;
    bool checkBoucle1 = false;
    // Start is called before the first frame update
    void Start()
    {
        mash = GetComponent<MashingFeedback>();
        playerMove = GetComponent<PlayerMoveAlone>();
        countPlayer = GetComponentInChildren<KillCountPlayer>();
        playerAnim = GetComponent<PlayerAnimState>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (tempsEcouleCheckLoop2 <= tempsAvantCheckLoop2)
        //{
        //    tempsEcouleCheckLoop2 += Time.deltaTime;
        //}
        //else
        //{
        //    tempsEcouleCheckLoop2 = 0;
        //}
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
        if (FastTest.debugPalier)
        {
            if (palierStep < palierCondition.Length - 1)
            {
                if (countPlayer.countKillEnnemi > (palierCondition[palierStep] / 10))
                {
                    activePanel = true;
                }
            }
        }
        else
        {
            if (tempsEcouleCheckLoop1 <= tempsAvantCheckLoop1 && !checkBoucle1)
            {
                tempsEcouleCheckLoop1 += Time.deltaTime;
            }
            if (tempsEcouleCheckLoop2 <= tempsAvantCheckLoop2 && checkBoucle1)
            {
                tempsEcouleCheckLoop2 += Time.deltaTime;
            }

            if (palierStep < palierCondition.Length - 1)
            {
                if (countPlayer.countKillEnnemi > (palierCondition[palierStep]))
                {
                    if (tempsEcouleCheckLoop1 > tempsAvantCheckLoop1 && !checkBoucle1 && palierStep == 3)
                    {
                        tempsEcouleCheckLoop1 = 0;
                        activePanel = true;
                    }
                    if (tempsEcouleCheckLoop2 > tempsAvantCheckLoop2 && checkBoucle1 && palierStep == 6)
                    {
                        tempsEcouleCheckLoop2 = 0;
                        activePanel = true;
                    }
                    if(palierStep != 3 || palierStep != 6)
                    {
                        activePanel = true;
                    }


                }
            }
            if (tempsEcouleCheckLoop1 > tempsAvantCheckLoop1 && !checkBoucle1)
            {
                tempsEcouleCheckLoop1 = 0;
                tempsAvantCheckLoop1 = 10.19f;
            }
            if (tempsEcouleCheckLoop2 > tempsAvantCheckLoop2 && checkBoucle1)
            {
                tempsEcouleCheckLoop2 = 0;
                tempsAvantCheckLoop2 = 13.7f;
            }
        }




        if (activePanel)
        {

            if (compteurTime > timeActivePlayerAnim)
            {
                ChangeStates();
                compteurTime = 0;
                activePanel = false;
            }
            else
            {


                compteurTime += Time.deltaTime;
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
            MusicPlayer.checkP1 = true;
            checkBoucle1 = true;
            FMODUnity.RuntimeManager.PlayOneShot(attractSound);
        }
        if (palierStep % 6 == 0)
        {
            MusicPlayer.checkP2 = true;
            FMODUnity.RuntimeManager.PlayOneShot(attractSound);
        }
    }

    public void GoTranformation()
    {
        mash.ActiveFeedback();
        playerMove.GoTransformation();
        CinematicCam.StartTransformation(true);
        StateOfGames.currentState = StateOfGames.StateOfGame.Transformation;
        playerAnim.ChangeStateAnim(PlayerAnimState.PlayerStateAnim.EntraveStart);
        SlowTime.StopTime();


    }

}
