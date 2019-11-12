using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMSkill : MonoBehaviour
{

    private bool isJumping = false;
    public float angleToRotate;
    public float angleRotated;
    public float angleSpeed;
    public float ratioAugmented = 0.05f;
    public float distanceMinimum = 0.5f;

    public Color color;

    private PlayerNumber playerNumber;
    private Vector3 pos;
    private Vector3 normal;
    private float Dist;
    private Vector3 dir;
    private string playerIdentity;

    public GameObject trail_Prefab;
    private GameObject nextPos;
    private Plane plane;

    private void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        plane = GetComponent<Plane>();

        playerIdentity = "Player" + playerNumber.playerNumber.ToString();
        nextPos = new GameObject();
        nextPos.transform.parent = transform;
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));


        if (Input.GetKeyDown(KeyCode.Alpha1) && playerIdentity == "Player1")
        {
            LaunchSM();
            Instantiate(trail_Prefab, transform.position, transform.rotation, transform);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) && playerIdentity == "Player2")
        {
            LaunchSM();
            Instantiate(trail_Prefab, transform.position, transform.rotation, transform);
        }

        if (Input.GetKeyDown("joystick " + playerNumber.manetteNumber.ToString() + " button 5"))
        {
            LaunchSM();

        }


        if (isJumping)
        {
            SauteMSkill();

        }
    }


    public void LaunchSM()
    {
        if (!isJumping)
        {
            angleRotated = 0;
            dir = PlayerCommands.OtherPlayer(gameObject).transform.position - gameObject.transform.position;
            normal = Vector3.Cross(-dir, Vector3.up);
            isJumping = true;
            PlayerCommands.ChangePlayerState(gameObject, PlayerState.StateOfPlayer.Fly);
            PointPivot();
        }
    }

    public void CheckNextDeplacement()
    {

        nextPos.transform.position = transform.position;
        nextPos.transform.rotation = Quaternion.identity;
        //PointPivotActive();
        nextPos.transform.RotateAround(pos, normal, angleSpeed * Time.deltaTime);
        Vector3 direction = nextPos.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f))
        {
            isJumping = false;

            transform.position = new Vector3(transform.position.x, hit.transform.position.y + 1, transform.position.z);
            PlayerCommands.ChangePlayerState(gameObject, PlayerState.StateOfPlayer.Free);
            plane.CreatePlane();
            Debug.Log("KO");
        }
    }
    public void SauteMSkill()
    {
        if (angleRotated >= angleToRotate)
        {
            isJumping = false;
            PlayerCommands.ChangePlayerState(gameObject, PlayerState.StateOfPlayer.Free);
            plane.CreatePlane();
        }
        else
        {
            if (angleRotated > 90)
            {
                CheckNextDeplacement();
            }
            angleRotated += angleSpeed * Time.deltaTime;
          //  PointPivotActive();
            transform.RotateAround(pos, normal, angleSpeed * Time.deltaTime);


        }
    }
    public void PointPivot()
    {
        dir = PlayerCommands.OtherPlayer(gameObject).transform.position - gameObject.transform.position;
        Dist = Vector3.Distance(PlayerCommands.player1.transform.position, PlayerCommands.player2.transform.position);
        pos = gameObject.transform.position + (dir.normalized * (Dist * (distanceMinimum + VitesseFunction.RatioAugmented(ratioAugmented))));
        pos = new Vector3(pos.x, 0, pos.z);
    }
    public void PointPivotActive()
    {
        dir = PlayerCommands.OtherPlayer(gameObject).transform.position - gameObject.transform.position;
        pos = gameObject.transform.position + (dir.normalized * (Dist * (distanceMinimum + VitesseFunction.RatioAugmented(ratioAugmented))));
        pos = new Vector3(pos.x, -1, pos.z);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(pos, Vector3.one);
    }
}
