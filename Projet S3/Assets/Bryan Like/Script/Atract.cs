using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atract : MonoBehaviour
{

    public float speed;
    public float distanceOfStops;
    public float vitesseAngle;
    public float currentAngle;
    public float ratioTransmit;
    public float angleRotate;
    float angleTorotate;

    bool checktest = false;
    private float dist;
    public bool rotateStart;


    private PlayerController playerController;



    private void Start()
    {
        this.playerController = PlayerCommand.player1Static.GetComponent<PlayerController>();
    }


    void Update()
    {
        if (PlayerCommand.CheckState(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Attireur))
        {
            if (Vector3.Distance(PlayerCommand.player2Static.transform.position, transform.position) > 1)
            {

                if (Input.GetKeyDown("joystick " + playerController.playerButton.ToString() + " button 2"))
                {
                     rotateStart = true;
                    checktest = true;
                    dist = Vector3.Distance(PlayerCommand.player1Static.transform.position, PlayerCommand.player2Static.transform.position);
                    PlayerCommand.ChangeStatePlayer(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Libre);
                    PlayerCommand.ChangeStatePlayer(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Rotation);
                }
                if (!rotateStart)
                {
                    PlayerCommand.player2Static.transform.position = Vector3.MoveTowards(PlayerCommand.player2Static.transform.position, transform.position, speed * Time.deltaTime);
                }
            }
            else
            {
                StopAttract();
            }


        }
        if (PlayerCommand.CheckState(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Libre) && rotateStart)
        {
            angleRotate += vitesseAngle * Time.deltaTime;
            if(checktest)
            {
                (currentAngle, ratioTransmit) = gameObject.GetComponent<VitesseFunc>().changeVelocity(true, currentAngle, ratioTransmit);
                angleTorotate = currentAngle;
                checktest = false;
            }
            Debug.Log(angleTorotate);
            if (angleRotate < angleTorotate)
            {
                Vector3 dir = PlayerCommand.player2Static.transform.position - PlayerCommand.player1Static.transform.position;

                PlayerCommand.player2Static.transform.position = Vector3.MoveTowards(PlayerCommand.player2Static.transform.position, dir.normalized * dist, playerController.speed * Time.deltaTime);
                PlayerCommand.player2Static.transform.RotateAround(transform.position, Vector3.up, vitesseAngle * Time.deltaTime);

            }
            else
            {
                rotateStart = false;
                angleRotate = 0;
                StopAttract();
                PlayerCommand.player2Static.transform.rotation = Quaternion.identity;
            }
       }
            if (PlayerCommand.CheckState(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Slaming) && rotateStart)
        {
            rotateStart = false;
            angleRotate = 0;
            StopAttract(); 
        }

    }
    void StopAttract()
    {
        PlayerCommand.ChangeStatePlayer(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Stun, Color.blue);
        PlayerCommand.ChangeStatePlayer(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Libre, Color.red);
        Physics.IgnoreCollision(PlayerCommand.player2Static.transform.GetComponent<Collider>(), GetComponent<Collider>(), false);
        Physics.IgnoreCollision(PlayerCommand.player2Static.transform.GetComponentInChildren<Collider>(), GetComponent<Collider>(), false);
    }
}


