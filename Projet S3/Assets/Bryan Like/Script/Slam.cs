using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
{
    public bool go = false;
    private Vector3 normal;
    Vector3 dir;
    float compteur;
    public float speed;
    float angle;

    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown("joystick " + playerController.playerButton.ToString() + " button 1") && PlayerCommand.CheckState(PlayerCommand.player2Static,PlayerState.PlayerCurrentState.Rotation))
        {
            go = true;
            normal = Vector3.Cross(PlayerCommand.DirectionBetweenPlayer(), Vector3.up);
            Debug.DrawRay(gameObject.transform.position, normal.normalized * 10, Color.red);
            angle = 0;

            PlayerCommand.ChangeStatePlayer(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Slaming);
            PlayerCommand.ChangeStatePlayer(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Libre);
        }
        if (go)
        {
            dir = PlayerCommand.DirectionBetweenPlayer();
            PlayerCommand.player1Static.transform.RotateAround(PlayerCommand.player2Static.transform.position, normal.normalized, speed * Time.deltaTime);
            PlayerCommand.player1Static.transform.rotation = Quaternion.identity;
            Ray ray = new Ray(PlayerCommand.player1Static.transform.position, -PlayerCommand.player1Static.transform.up);
            RaycastHit hit;
            if (angle > 100f)
            {
                if (Physics.Raycast(ray, out hit, transform.localScale.y + 0.1f))
                {
                    go = false;

                    PlayerCommand.player1Static.transform.position = new Vector3(PlayerCommand.player1Static.transform.position.x, 1, PlayerCommand.player1Static.transform.position.z);
                    angle = 0;
                    PlayerCommand.ChangeStatePlayer(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Libre);
                }
            }
            if (angle > 120)
            {
                if (Input.GetKeyDown("joystick " + playerController.playerButton.ToString() + " button 0"))
                {
                    PlayerCommand.ChangeStatePlayer(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Attireur, Color.grey);
                }
            }
            angle += speed * Time.deltaTime;

        }
    }
}
