using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float timeToFlight;
    [HideInInspector] public float speedOfFight;
    [HideInInspector] public float opportunityWindow;

    private float compteurOfFlyTime;
    private float playerState;
    
    void Update()
    {

        FlyBehavior();
        
    }

   private void FlyBehavior()
    {
        if (compteurOfFlyTime > timeToFlight)
        {
            GetComponent<PlayerState>().playerState = PlayerState.StateOfPlayer.Free;
            GetComponent<PlayerState>().opportunityState = PlayerState.OpportunityState.Out;
            Destroy(this);
        }
        else
        {
            transform.position += direction * speedOfFight * Time.deltaTime;
            compteurOfFlyTime += Time.deltaTime;
        }

        if(compteurOfFlyTime/ timeToFlight >= opportunityWindow)
        {
            GetComponent<PlayerState>().opportunityState = PlayerState.OpportunityState.In;
            
        }   
    }
}
