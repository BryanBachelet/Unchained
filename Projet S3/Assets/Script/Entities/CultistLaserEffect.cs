using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistLaserEffect : MonoBehaviour
{

    [HideInInspector] public float ejectionForce;
    [HideInInspector] public Vector3 ejectionDirection;
    private LayerMask playerLayer =10;

    private bool hit;

    public void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.layer == 10 && hit ==false) 
        {
               // collider.GetComponent<EnnemiStock>().DetachPlayer();
                collider.GetComponentInChildren<KillCountPlayer>().multiLoseCondition = 1f;
                hit = true;
        }   
    }

       public void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == 10 && hit ==true) 
        {
                //collider.GetComponent<EnnemiStock>().DetachPlayer();
                collider.GetComponentInChildren<KillCountPlayer>().multiLoseCondition = 0;
                hit = false;
        }   
    }

    public void ResetAttact()
    {
        hit = false;
    }
}
