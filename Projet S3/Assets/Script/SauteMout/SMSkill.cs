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
    private void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();

        playerIdentity = "Player" + playerNumber.playerNumber.ToString();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));


        if (Input.GetKeyDown(KeyCode.Alpha1) &&  playerIdentity == "Player1")
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
            PointPivot();
        }
    }

    public void SauteMSkill()
    {
        if (angleRotated >= angleToRotate)
        {
            isJumping = false;
        }
        else
        {
            angleRotated += angleSpeed * Time.deltaTime;
            transform.RotateAround(pos, normal, angleSpeed * Time.deltaTime);
            float prevision = angleRotated + angleSpeed * Time.deltaTime;

        }
    }
    public void PointPivot()
    {
        dir = PlayerCommands.OtherPlayer(gameObject).transform.position - gameObject.transform.position;
        Dist = Vector3.Distance(PlayerCommands.player1.transform.position, PlayerCommands.player2.transform.position);
        pos = gameObject.transform.position + (dir.normalized * (Dist * (distanceMinimum + VitesseFunction.RatioAugmented(ratioAugmented))));
        pos = new Vector3(pos.x, 1, pos.z);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(pos, Vector3.one);
    }
}
