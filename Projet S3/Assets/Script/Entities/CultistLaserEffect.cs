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
        collider.GetComponent<PlayerMoveAlone>().AddProjection(ejectionDirection*ejectionForce);
        hit = true;
  }   
 }

 public void ResetAttact()
 {
     hit = false;
 }
}
