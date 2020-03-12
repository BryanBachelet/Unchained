using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{

    [HideInInspector] public GameObject player;

    [HideInInspector] public Vector3 ecartJoueur;
    private bool activeBehavior;
    [HideInInspector] public Vector3 basePosition;
    
    public Camera orthoCam;
  
    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerMoveAlone.Player1;
       

        ecartJoueur = transform.position - player.transform.position;
     
    }


   public void ResetPos()
    {
        ecartJoueur = transform.position - player.transform.position;
        activeBehavior = false;
    }

    public void Deactive()
    {
        activeBehavior = true;
    }


    void Update()
    {
        orthoCam.orthographicSize = 15 * Vector3.Distance(transform.position, player.transform.position) / 25;

        if (!activeBehavior)
        {
            basePosition = player.transform.position + ecartJoueur;
            transform.position = basePosition;
         
        }

    }

}
