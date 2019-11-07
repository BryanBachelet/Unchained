using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbillon : MonoBehaviour
{
    public GameObject myGhost;  ///////////////////////
    public GameObject newGhost; ///////////////////////

    public Transform otherPlayer; ///////////////////////
    private float angleRotated;
    private PlayerNumber playerNumber;


    private float ghostAngleRotated;  ///////////////////////

    private bool isRotate = false;
    private bool ghostIsRotate = false;  ///////////////////////
    public float angleToRotateMinimum = 360;
    private float angleToRotate;

    public float angleSpeed = 180;
    public float ratioAugmented;

    [Range(0, 1f)] public float opportunityWindow = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        //otherPlayer = PlayerCommands.OtherPlayer(gameObject).transform;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown("joystick " + playerNumber.manetteNumber.ToString() + " button 4") && !isRotate || Input.GetKeyDown(KeyCode.Alpha1))  ///////////////////////
        if(Input.GetKeyDown(KeyCode.Alpha1) && !isRotate)
        {
            newGhost = Instantiate(myGhost, transform.position, transform.rotation); ///////////////////////

            angleRotated = 0;
            ghostAngleRotated = 0;  ///////////////////////
            angleToRotate = angleToRotateMinimum + VitesseFunction.RatioAugmented(ratioAugmented);
            //PlayerCommands.ActiveOpportunityWindow(gameObject);
            isRotate = true;
            ghostIsRotate = true;  ///////////////////////

        }
        if (isRotate)
        {
            TourbillonSkill();
            if (angleRotated >= angleToRotate)
            {
                //PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.Out); ////////////////////////
                isRotate = false;
                Destroy(newGhost);
            }
            if(ghostAngleRotated >= angleToRotate)  ///////////////////////
            {  ///////////////////////
                ghostIsRotate = false;  ///////////////////////
            }  ///////////////////////
        }
    }

    public void TourbillonSkill()
    {
        angleRotated += angleSpeed * Time.deltaTime;
        if(ghostIsRotate) ////////////////////////
        { ////////////////////////
            ghostAngleRotated += angleSpeed * Time.deltaTime * 5f; ///////////////////////
            newGhost.transform.RotateAround(otherPlayer.position, Vector3.up, angleSpeed * Time.deltaTime * 5f); ///////////////////////
        } ////////////////////////

        transform.RotateAround(otherPlayer.position, Vector3.up, angleSpeed * Time.deltaTime);

        float currentDist = angleRotated / angleToRotate;
        if (currentDist > opportunityWindow)
        {
            //PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.In); //////////////////////////
        }
    }
}

