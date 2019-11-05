using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
public class PlayerController : MonoBehaviour
{

    private Rigidbody rigid;
    PlayerState playerState;

    public int playerManette = 1;
    public int playerButton;
    public float horizontal;
    public float vertical;
    public float deadzoneManette;
    public float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        playerState = GetComponent<PlayerState>();
        if (playerManette == 1)
        {
            playerButton = InputManager.controllerOne;
        }
        else
        {
            playerButton = InputManager.controllerTwo;
        }
      
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal" + playerManette.ToString());
        vertical = Input.GetAxis("Vertical" + playerManette.ToString());

        if (PlayerCommand.CheckState(gameObject, PlayerState.PlayerCurrentState.Libre))
        {

            if (horizontal < deadzoneManette && horizontal > -deadzoneManette)
            {
                horizontal = 0;
            }
            if (horizontal > deadzoneManette)
            {
                horizontal = 1;
            }
            if (horizontal < -deadzoneManette)
            {
                horizontal = -1;
            }
            if (vertical < deadzoneManette && vertical > -deadzoneManette)
            {
                vertical = 0;
            }
            if (vertical > deadzoneManette)
            {
                vertical = 1;
            }
            if (vertical < -deadzoneManette)
            {
                vertical = -1;
            }

            rigid.MovePosition(rigid.position + transform.right * horizontal * speed * Time.deltaTime + transform.forward * vertical * speed * Time.deltaTime);
        }

    }
}

