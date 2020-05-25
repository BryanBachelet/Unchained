using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainVelocitySystem : MonoBehaviour
{
    [Range(0,100)]
    public int gainVelocityPercent;
    [Range(0,10)]
    public int gainKillP1;
    [Range(0, 30)]
    public int gainMashP1;
    [Range(0, 10)]
    public int gainKillP2;
    [Range(0, 30)]
    public int gainMashP2;
    public int maxGainKill;
    public int maxGainMash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase1)
        {
            gainKillP1 = ((maxGainKill * (ManageEntity.PercentKill * 2)) / 100);
            if (gainKillP1 > maxGainKill)
            {
                gainKillP1 = maxGainKill;
            }
        }
        if (StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase2)
        {
            gainKillP2 = ((maxGainKill * (ManageEntity.PercentKill * 2)) / 100);
            if (gainKillP2 > maxGainKill)
            {
                gainKillP2 = maxGainKill;
            }
        }
        

        if (gainMashP1 > maxGainMash)
        {
            gainMashP1 = maxGainMash;
        }
        if (gainMashP2 > maxGainMash)
        {
            gainMashP2 = maxGainMash;
        }

        gainVelocityPercent = gainKillP1 + gainMashP1 + gainKillP2 + gainMashP2;

    }

    public float CalculGain(float valueToBuff)
    {
        valueToBuff = valueToBuff * (1 + gainKillP1 / 100);
        valueToBuff = valueToBuff * (1 + gainMashP1 / 100);
        valueToBuff = valueToBuff * (1 + gainKillP2 / 100);
        valueToBuff = valueToBuff * (1 + gainMashP2 / 100);
        return valueToBuff;
    }
}
