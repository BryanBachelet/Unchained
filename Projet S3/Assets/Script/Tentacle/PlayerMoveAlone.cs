using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAlone : MonoBehaviour
{
    private Rigidbody playerRigid;
    public float speed;
    public float powerOfProjection;
    public float deprojection = 60;
    [HideInInspector] public Vector3 DirProjection;
    [HideInInspector] public float powerProjec;
   
    void Start()
    {
        GetComponent<EnnemiStock>().powerOfProjection = powerOfProjection;
        GetComponent<WallRotate>().powerOfProjection = powerOfProjection;
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRigid.velocity = (Direction() * speed) + (DirProjection.normalized * powerProjec);
        if (powerProjec > 0)
        {
            powerProjec -= deprojection * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
     
    }

    private Vector3 Direction()
    {
        float horizontal = Input.GetAxis("Horizontal1");
        float vertical = Input.GetAxis("Vertical1");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        return dir.normalized;
    }
}
