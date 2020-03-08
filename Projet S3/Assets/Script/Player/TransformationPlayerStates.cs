using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationPlayerStates : MonoBehaviour
{
    public enum Palier { Palier0, Palier1, Palier2, Tranformation1, Palier4, Palier5, Transformation2 }

    public static Palier currentPalier = Palier.Palier0;
    
   
    public int[] palierCondition = new int[7];


    private KillCountPlayer countPlayer;
    private int palierStep;
   

    // Start is called before the first frame update
    void Start()
    {
        countPlayer = GetComponentInChildren<KillCountPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }

   public void CheckState()
    {
        if (countPlayer.countKillEnnemi > palierCondition[palierStep])
        {
            ChangeStates();
        }
    }

    public void ChangeStates()
    {
        currentPalier++;
        palierStep++;
        if (palierStep % 3 == 0)
        {
            GoTranformation();
        }
    }

    public void GoTranformation()
    {
        StateOfGames.currentState = StateOfGames.StateOfGame.Transformation;
    }

}
