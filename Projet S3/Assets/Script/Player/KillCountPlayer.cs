﻿using System.Collections;
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
    public GameObject postProcesse;

    [HideInInspector] public bool activeDecrease;

    [FMODUnity.EventRef]
    public string Lose;
    private FMOD.Studio.EventInstance loseCondition;
    public float volume = 20;

    private static float timerOfKill;

    private int frameDecreaseCondition;
    private static float compteurOfDeath;

    private static bool activeReset;
    public float compteur;

    public MusicPlayer myMP;

    public void Awake()
    {
        loseCondition = FMODUnity.RuntimeManager.CreateInstance(Lose);
        loseCondition.setVolume(volume);
    }
    public void Start()
    {


    }

    void Update()
    {


        compteur = compteurOfDeath;
        
        if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {

            if (compteurOfDeath > timeBeforeDeath)
            {
                if (myMP != null)
                {
                    myMP.track1.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                }
                ResetTiming();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                loseCondition.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                postProcesse.GetComponent<PostProcessVolume>().enabled = false;
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
                    postProcesse.GetComponent<PostProcessVolume>().enabled = true;
                }
            }
        }

        if (activeReset)
        {
            frameDecreaseCondition = 0;
            postProcesse.GetComponent<PostProcessVolume>().enabled = false;
            activeReset = false;
        }

    }


    public static void HitEnnemi()
    {
        ResetTiming();
    }

    public static void ResetTiming()
    {
        compteurOfDeath = 0;
        activeReset = true;
    }
}
