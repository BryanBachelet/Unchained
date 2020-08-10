using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionScript : MonoBehaviour
{
    public  MonoBehaviour[] cinematics;
    public  MonoBehaviour[] playtime;
    public  MonoBehaviour[] transformation;
    public  MonoBehaviour[] DefAndVictory;

    private bool maj = false;

    private StateOfGames.StateOfGame previousState; 
    // Start is called before the first frame update
    void Start()
    {
        if (StateOfGames.currentState == StateOfGames.StateOfGame.Cinematic)
        {
            if (previousState != StateOfGames.currentState)
            {
                ActiveCinematique(true);
                ActivePlaytime(false);
                ActiveTransformation(false);
                ActiveDefaite(false);
                previousState = StateOfGames.currentState;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (previousState != StateOfGames.currentState)
        {
            if (StateOfGames.currentState == StateOfGames.StateOfGame.Cinematic)
            {

                ActivePlaytime(false);
                ActiveTransformation(false);
                ActiveCinematique(true);
                ActiveDefaite(false);
            }
            if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
            {

                ActiveCinematique(false);
                ActiveTransformation(false);
                ActivePlaytime(true);
                ActiveDefaite(false);
            }
            if (StateOfGames.currentState == StateOfGames.StateOfGame.Transformation)
            {
                ActiveCinematique(false);
                ActivePlaytime(false);
                ActiveTransformation(true);
                ActiveDefaite(false);
            }
            if (StateOfGames.currentState == StateOfGames.StateOfGame.Defaite)
            {
                ActiveCinematique(false);
                ActivePlaytime(false);
                ActiveTransformation(false);
                ActiveDefaite(true);
            }
            previousState = StateOfGames.currentState;
        }
    }

   public void ActiveCinematique(bool states)
    {
        for (int i = 0; i < cinematics.Length; i++)
        {
            cinematics[i].enabled = states;
        }
    }

    public void ActivePlaytime(bool states)
    {
        for (int i = 0; i < playtime.Length; i++)
        { 
            if(StateOfGames.currentState == StateOfGames.StateOfGame.Transformation)
            { 
                if(playtime[i] is PlayerMoveAlone )
                {
                    
                }else
                {
                    playtime[i].enabled = states;
                }
            }else
            {   
                playtime[i].enabled = states;
            }
        }
    }

    public void ActiveDefaite(bool states)
    {
         for (int i = 0; i < DefAndVictory.Length; i++)
        {
            DefAndVictory[i].enabled = states;       
        }
    }

    public void ActiveTransformation(bool states)
    {
        for (int i = 0; i < transformation.Length; i++)
        {
            transformation[i].enabled = states;
        }

    }


}
