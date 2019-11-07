using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbillon : MonoBehaviour
{
    public GameObject myGhost;

    private Transform otherPlayer;
    private float angleRotated;
    private PlayerNumber playerNumber;

    private bool isRotate = false;
    private bool ghostIsRotate = false;
    public float angleToRotateMinimum = 360;
    private float angleToRotate;

    public float angleSpeed = 180;
    public float ratioAugmented;

    [Range(0, 1f)] public float opportunityWindow = 0.5f;


    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        otherPlayer = PlayerCommands.OtherPlayer(gameObject).transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("joystick " + playerNumber.manetteNumber.ToString() + " button 4") && !isRotate || Input.GetKeyDown(KeyCode.Alpha1))  ///////////////////////
        {

            angleRotated = 0;

            angleToRotate = angleToRotateMinimum + VitesseFunction.RatioAugmented(ratioAugmented);
            PlayerCommands.ActiveOpportunityWindow(gameObject);
            isRotate = true;
          
          

        }
        if (isRotate)
        {
            TourbillonSkill();
            if (angleRotated >= angleToRotate)
            {
                PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.Out);
                isRotate = false;
                myGhost.SetActive(false);

            }

        }
    }

    public void TourbillonSkill()
    {
        angleRotated += angleSpeed * Time.deltaTime;

        transform.RotateAround(otherPlayer.position, Vector3.up, angleSpeed * Time.deltaTime);
        float angleReset = angleToRotate - angleRotated;
        if (angleReset < 360)
        {
            myGhost.SetActive(true);
            myGhost.transform.position = transform.position;
            myGhost.transform.RotateAround(otherPlayer.position, Vector3.up, angleReset);
        }

        float currentDist = angleRotated / angleToRotate;
        if (currentDist > opportunityWindow)
        {
            PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.In);
        }
    }
}

