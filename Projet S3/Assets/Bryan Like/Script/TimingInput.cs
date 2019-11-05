using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingInput : MonoBehaviour
{
    float tempsEcouleComp = 0;
    public bool inComp;
    public float perfectTiming;
    public float reduceTiming;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        reduceTiming = VitesseFunc.velocity / 100;
        if(inComp)
        {
            tempsEcouleComp += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (tempsEcouleComp > perfectTiming  - reduceTiming && tempsEcouleComp < perfectTiming + reduceTiming)
                {
                    Debug.Log("WWWWWWWWWWIIIIIIIIIIIIIIIIINNNNNNNNNNNNNNNN");
                }
                else if (tempsEcouleComp > perfectTiming + reduceTiming)
                {
                    Debug.Log("FFFFFFFFFFFAAAAAAAAAAAAAAAIIIIIIIIIIIIIILLLLLLLLLLLLLLLLLL");
                }
            }

        }
  
    }
}
