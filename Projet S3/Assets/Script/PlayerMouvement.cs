using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    [HideInInspector]
    public float horizontal;
    [HideInInspector]
    public float vertical;
    private PlayerNumber playerNumber;
    public float speedOfDeplacement;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
         horizontal = Input.GetAxis("Horizontal" + playerNumber.playerNumber.ToString());
         vertical = Input.GetAxis("Vertical" + playerNumber.playerNumber.ToString());
        //transform.position += GetDirection(playerNumber.playerNumber) * speedOfDeplacement * Time.deltaTime;
        rigidbody.velocity = GetDirection(playerNumber.playerNumber) * speedOfDeplacement ;
      
    }

    public static Vector3 GetDirection(int player)
    {
        float horizontal = Input.GetAxis("Horizontal" + player.ToString());
        float vertical = Input.GetAxis("Vertical" + player.ToString());
        return new Vector3(horizontal, 0, vertical).normalized;
    }
}
