using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FastTest : MonoBehaviour
{
     public bool debugLoseConditionActive, debugMashingActive, debugPalierActive;
      public static  bool debugLoseCondition;
    public static bool debugMashing;
    public static bool debugPalier;

    public void Start()
    {
        debugLoseCondition = debugLoseConditionActive;
        debugMashing = debugMashingActive;
        debugPalier = debugPalierActive;
        
    }
 
}
