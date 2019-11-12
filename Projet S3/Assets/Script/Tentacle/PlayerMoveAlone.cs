using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAlone : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        rigidbody.velocity = Direction() * speed;
    }

    Vector3 Direction()
    {
        float horizontal = Input.GetAxis("Horizontal1");
        float vertical = Input.GetAxis("Vertical1");
        Vector3 dir = new Vector3(horizontal,0, vertical);
        return dir.normalized;
    }
}
