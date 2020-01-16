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
    public float powerProjec;
    [Header("Animation")]
    public float speedOfRotation = 10f;
    float angleAvatar;

    static public Vector3 playerPos;
    static public GameObject Player1;
    private void Awake()
    {
        Player1 = gameObject;
    }
    void Start()
    {
        GetComponent<EnnemiStock>().powerOfProjection = powerOfProjection;
        GetComponent<WallRotate>().powerOfProjection = powerOfProjection;
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = transform.position;
        powerProjec = Mathf.Clamp(powerProjec, 0, 1000);
        playerRigid.velocity = (Direction() * speed) + (DirProjection.normalized * powerProjec);
        AnimationAvatar();
        if (powerProjec > 0)
        {
            powerProjec -= deprojection * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);

    }


    public void AnimationAvatar()
    {
        if (StateAnim.state == StateAnim.CurrentState.Walk)
        {
            float angleConversion = transform.eulerAngles.y;
            angleConversion = angleConversion > 180 ? angleConversion - 360 : angleConversion;
            angleAvatar = Vector3.SignedAngle(Vector3.forward, Direction(), Vector3.up);
            if (angleConversion < 0 && angleAvatar == 180)
            {
                angleAvatar = -180;
            }
            transform.eulerAngles = Vector3.Lerp(new Vector3(transform.eulerAngles.x, angleConversion, transform.eulerAngles.z),
                     new Vector3(transform.eulerAngles.x, angleAvatar, transform.eulerAngles.z), speedOfRotation * Time.deltaTime);

            if (Direction() == Vector3.zero)
            {
                StateAnim.ChangeState(StateAnim.CurrentState.Idle);
            }
        }

        if (StateAnim.state == StateAnim.CurrentState.Projection && powerProjec == 0)
        {
            StateAnim.ChangeState(StateAnim.CurrentState.Idle);
        }
        
        if (StateAnim.state == StateAnim.CurrentState.Idle && Direction() != Vector3.zero)
        {
            StateAnim.ChangeState(StateAnim.CurrentState.Walk);
        }
    }

    public Vector3 Direction()
    {
        float horizontal = Input.GetAxis("Horizontal1");
        float vertical = Input.GetAxis("Vertical1");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        return dir.normalized;
    }
}
