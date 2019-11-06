using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opportunity : MonoBehaviour
{
    public bool activeInput;

    private PlayerState playerState;

    public void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    public void Update()
    {
        GainSpeed();
    }

    public void GainSpeed()
    {
        if (activeInput)
        {
            if (playerState.opportunityState == PlayerState.OpportunityState.In)
            {
                VitesseFunction.VitesseChange(true);
            }
            if (playerState.opportunityState == PlayerState.OpportunityState.Out)
            {
                VitesseFunction.VitesseChange(false);
            }

            activeInput = false;
        }
    }
}
