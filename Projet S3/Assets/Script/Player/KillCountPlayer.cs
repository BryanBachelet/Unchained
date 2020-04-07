using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering.PostProcessing;


public class KillCountPlayer : MonoBehaviour
{

    public float timeBeforeDeath;

    public float activeLoseEffect;
    public float maxEffectTimeBeforDeath;
    public float timeToWeightReturn;
    public PostProcessVolume postProcesse;

    [HideInInspector] public bool activeDecrease;

    [FMODUnity.EventRef]
    public string Lose;
    private FMOD.Studio.EventInstance loseCondition;
    public float volume = 20;

    private static float timerOfKill;

    private int frameDecreaseCondition;
    private  float compteurOfDeath;

    private  bool activeReset;
    public float compteur;
    public  int countKillEnnemi;


    public MusicPlayer myMP;


private float maxTime;
private float frameStartReturn;
private float currentweight;
private float realTimeToWeightRetur;
private float compteurWeightReturn;

    public void Awake()
    {
        loseCondition = FMODUnity.RuntimeManager.CreateInstance(Lose);
        loseCondition.setVolume(volume);

        maxTime = timeBeforeDeath-(maxEffectTimeBeforDeath+activeLoseEffect);
    }
   


    void Update()
    {
        if(FastTest.debugLoseCondition)
        {
            timeBeforeDeath = 100;
        }

        compteur = compteurOfDeath;
        
        if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {

            if (compteurOfDeath > timeBeforeDeath)
            {
               ActiveDeathCondition();
            }
            else
            {

                compteurOfDeath += Time.deltaTime;
            }

            if (compteurOfDeath > activeLoseEffect)
            {
                if (frameDecreaseCondition == 0)
                {
                    loseCondition.start();
                    frameDecreaseCondition = 1;
                    myMP.track1.setParameterByName("TrackEffect", 0.0F);
                }
                    postProcesse.weight = ((compteurOfDeath-activeLoseEffect)/maxTime );
            }
            else
            {
                if(postProcesse.weight != 0)
                {
                    if(frameStartReturn == 0)
                    {
                        currentweight = postProcesse.weight;
                        frameStartReturn = 1;
                        realTimeToWeightRetur = timeToWeightReturn*( currentweight/1);
                    }
                
                postProcesse.weight = Mathf.Lerp(currentweight,0,(compteurWeightReturn/realTimeToWeightRetur));
                compteurWeightReturn +=Time.deltaTime;
                }else
                {
                    frameStartReturn =0;
                 compteurWeightReturn =0;
                }
            }
        }
        else
        {
            compteurOfDeath = 0;
        }

        if (activeReset)
        {
            frameDecreaseCondition = 0;
      
         activeReset = false;
        }

    }


    public void ActiveDeathCondition ()
    {
        if (myMP != null)
        {
            myMP.track1.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        ResetTiming();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
        loseCondition.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        postProcesse.weight = 0;
        StateOfGames.currentState = StateOfGames.StateOfGame.Cinematic;
    }

    public void HitEnnemi()
    {
        ResetTiming();
        countKillEnnemi++;
    }

    public  void ResetTiming()
    {
        loseCondition.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        compteurOfDeath = 0;
        activeReset = true;
        myMP.track1.setParameterByName("TrackEffect", 1.0F);
    }
}
