using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Light[] lightsToChange;

    public bool activePlayer;

    public float timing = 1;
    private float compteur;

 
    // Update is called once per frame
    void Update()
    {
        if(activePlayer)
        {
            for(int i = 0;i<lightsToChange.Length;i++)
            {
                lightsToChange[i].intensity = Mathf.Lerp(1,0,compteur/timing);
            }
            compteur+=Time.deltaTime;
        }else
        {
             for(int i = 0;i<lightsToChange.Length;i++)
            {
                lightsToChange[i].intensity = Mathf.Lerp(0,1,compteur/timing);
            }
            compteur+=Time.deltaTime;
        }
    }

    public void ChangeLight(bool active)
    {
        activePlayer = active;
        compteur = 0;
    }


}
