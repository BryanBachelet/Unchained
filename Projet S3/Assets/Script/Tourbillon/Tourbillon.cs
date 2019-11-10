using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbillon : MonoBehaviour
{
    public GameObject trail_Prefab;
    private Transform otherPlayer;
    private float angleRotated;
    private PlayerNumber playerNumber;
    private PlayerMouvement playerMouvement;


    private float ghostAngleRotated;  ///////////////////////

    private bool isRotate = false;
    private bool ghostIsRotate = false;  ///////////////////////
    public float angleToRotateMinimum = 360;
    private float angleToRotate;
    private int rotationSens = 1;
    public float angleSpeed = 180;
    public float ratioAugmented;

    [Range(0, 1f)] public float opportunityWindow = 0.5f;
    private string playerIdentity;
    public GameObject nextPos;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        playerMouvement = GetComponent<PlayerMouvement>();
        otherPlayer = PlayerCommands.OtherPlayer(gameObject).transform;
        playerIdentity = "Player" + playerNumber.playerNumber.ToString();
        nextPos = new GameObject();
        nextPos.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && playerIdentity == "Player1")
        {
            LaunchTourbillon();
            Instantiate(trail_Prefab, transform.position, transform.rotation, transform);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && playerIdentity == "Player2")
        {
            LaunchTourbillon();
            Instantiate(trail_Prefab, transform.position, transform.rotation, transform);
        }


        if (Input.GetKeyDown("joystick " + playerNumber.manetteNumber.ToString() + " button 4"))
        {
            LaunchTourbillon();


        }
        if (isRotate)
        {
            TourbillonSkill();
            if (angleRotated >= angleToRotate)
            {
                PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.Out);
                isRotate = false;
            }
        }
    }

    public void LaunchTourbillon()
    {
        if (!isRotate)
        {
            angleRotated = 0;
            angleToRotate = angleToRotateMinimum + VitesseFunction.RatioAugmented(ratioAugmented);
            PlayerCommands.ActiveOpportunityWindow(gameObject);
            isRotate = true;
            CheckOrientation();
        }
    }

    public void CheckOrientation()
    {

        if (playerMouvement.horizontal >= 0)
        {

            rotationSens = 1;
        }
        if (playerMouvement.horizontal < 0)
        {
            rotationSens = -1;
        }

    }

    public void CheckDeplacement()
    {
       
        nextPos.transform.position = transform.position;
        nextPos.transform.rotation = Quaternion.identity;
        nextPos.transform.RotateAround(otherPlayer.position, Vector3.up, rotationSens * angleSpeed * Time.deltaTime);
        Vector3 direction = nextPos.transform.position - transform.position;

        Ray ray = new Ray(nextPos.transform.position, direction.normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            isRotate = false;
        }

    }

    public void TourbillonSkill()
    {

        angleRotated += angleSpeed * Time.deltaTime;

        CheckDeplacement();
        transform.RotateAround(otherPlayer.position, Vector3.up, rotationSens * angleSpeed * Time.deltaTime);

        float currentDist = angleRotated / angleToRotate;
        if (currentDist > opportunityWindow)
        {
            PlayerCommands.ChangeOpportunityState(gameObject, PlayerState.OpportunityState.In);
        }
    }
}

