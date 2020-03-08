using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionScript : MonoBehaviour
{
    public MonoBehaviour[] cinematics;
  public  MonoBehaviour[] playtime;
  public  MonoBehaviour[] transformation;

    private bool maj = false;

    // Start is called before the first frame update
    void Start()
    {

        if (StateOfGames.currentState == StateOfGames.StateOfGame.Cinematic)
        {

            ActiveCinematique(true);
            ActivePlaytime(false);
            ActiveTransformation(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StateOfGames.currentState == StateOfGames.StateOfGame.Cinematic)
        {

            ActiveCinematique(true);
            ActivePlaytime(false);
            ActiveTransformation(false);
        }
        if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {

            ActiveCinematique(false);
            ActivePlaytime(true);
            ActiveTransformation(false);
        }
        if (StateOfGames.currentState == StateOfGames.StateOfGame.Transformation)
        {

            ActiveCinematique(false);
            ActivePlaytime(false);
            ActiveTransformation(true);
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
            playtime[i].enabled = states;
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
