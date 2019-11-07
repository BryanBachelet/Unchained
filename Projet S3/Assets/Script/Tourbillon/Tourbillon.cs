using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbillon : MonoBehaviour
{
    private Transform otherPlayer;
    private float angleRotated;
    private PlayerNumber playerNumber;

    private bool isRotate = false;
    public float angleToRotateMinimum = 360;
    private float angleToRotate;

    public float angleSpeed = 180;
    public float ratioAugmented;

    [Range(0, 1f)] public float opportunityWindow = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        otherPlayer = PlayerCommands.OtherPlayer(gameObject).transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("joystick " + playerNumber.manetteNumber.ToString() + " button 4") && !isRotate)
        {
            angleRotated = 0;
            angleToRotate = angleToRotateMinimum + VitesseFunction.RatioAugmented(ratioAugmented);
            PlayerCommands.ActiveOpportunityWindow(gameObject);
            isRotate = true;

        }
        if (isRotate)
        {
            AttractSkill();
            if (angleRotated >= angleToRotate)
            {
                PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.Out);
                isRotate = false;
            }
        }
    }

    public void AttractSkill()
    {
        angleRotated += angleSpeed * Time.deltaTime;
        transform.RotateAround(otherPlayer.position, Vector3.up, angleSpeed * Time.deltaTime);

        float currentDist = angleRotated / angleToRotate;
        if (currentDist > opportunityWindow)
        {
            PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.In);
        }
    }
}

