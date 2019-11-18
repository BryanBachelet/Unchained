using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAlone : MonoBehaviour
{
    private Rigidbody playerRigid;
    public float speed;
    public Vector3 DirProjection;
    public float powerProjec;
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRigid.velocity =(Direction() * speed) + (DirProjection.normalized * powerProjec) ;
        if (powerProjec > 0)
        {
            powerProjec -= 60 * Time.deltaTime;
        }
    }

    private Vector3 Direction()
    {
        float horizontal = Input.GetAxis("Horizontal1");
        float vertical = Input.GetAxis("Vertical1");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        return dir.normalized;
    }
}
