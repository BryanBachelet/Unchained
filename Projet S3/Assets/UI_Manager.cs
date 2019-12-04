using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{

    public bool rotationState;
    public GameObject player;
    RotationPlayer rotationPlayer;
    public GameObject uI;
    

    void Start()
    {
        player = GameObject.Find("Player");
        rotationPlayer = player.GetComponent<RotationPlayer>();
        //rotationState = gameObject.GetComponent<RotationPlayer>().rotate;
    }


    void Update()
    {

        rotationState = rotationPlayer.rotate;

        
        if (rotationState == false)
        {
            uI.SetActive(false);
        }

        if (rotationState == true)
        {
            uI.SetActive(true);
        }
        
    }
}
