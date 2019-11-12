using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityForce;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ActiveGravity();
    }

    void ActiveGravity()
    {
        Ray ray = new Ray(transform.position, -transform.up);

        if (!Physics.Raycast(ray, 1))
        {
            if (PlayerCommands.CheckPlayerState(gameObject, PlayerState.StateOfPlayer.Free))
            {
                Debug.Log("1");
                if (rigidbody.velocity.y > -2)
                {
                    rigidbody.velocity = rigidbody.velocity + -transform.up * gravityForce;
                }
               
            }

        }
    }

}
