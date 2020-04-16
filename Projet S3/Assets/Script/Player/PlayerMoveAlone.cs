using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAlone : MonoBehaviour
{
    private Rigidbody playerRigid;
    private float speedOfDeplacement = 10;
    [Header("Projection")]
    public float powerOfProjection;
    public float DecelerationOfProjection = 60;
    public AnimationCurve ratioOfExpulsion;
    [HideInInspector] public Vector3 DirProjection;
    [HideInInspector] public float currentPowerOfProjection;

    [Header("Expulsion")]
    public float expulsionStrengh;

    [Header("Options ")]
    public bool activeDeplacement;

    [Header("Animation")]
    public float speedOfRotation = 10f;
    float angleAvatar;

    private LineRend line;
    private MouseScope mouseScop;
    public GameObject aura;
    static public Vector3 playerPos;
    static public GameObject Player1;
    private LineRenderer lineRenderer;
    private EnnemiStock stock;
    private  bool isStickGround = true;
    private float _timeProjection;

    private void Awake()
    {
        Player1 = gameObject;
    }
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        stock = GetComponent<EnnemiStock>();
        isStickGround = true;
        GetComponent<EnnemiStock>().powerOfProjection = powerOfProjection;
        GetComponent<WallRotate>().powerOfProjection = powerOfProjection;
        playerRigid = GetComponent<Rigidbody>();
        mouseScop = GetComponent<MouseScope>();
        if( line == null ) { line = transform.GetComponentInChildren<LineRend>(); }
        TransmitionOfStrenghOfExpulsion();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        playerPos = transform.position;
        currentPowerOfProjection = Mathf.Clamp(currentPowerOfProjection, 0, 1000);
        if (activeDeplacement)
        {
            playerRigid.velocity = (Direction() * speedOfDeplacement) + (DirProjection.normalized * currentPowerOfProjection);
        }
        else
        {    
            if(!isStickGround)
            {
            playerRigid.velocity = new Vector3(0,playerRigid.velocity.y,0)+ (DirProjection.normalized * currentPowerOfProjection);
            }
            else
            {
                playerRigid.velocity =  (DirProjection.normalized * currentPowerOfProjection);
            }
        }
        AnimationAvatar();
        if (currentPowerOfProjection > 0)
        {
            _timeProjection +=Time.deltaTime;
            float ratio =  ratioOfExpulsion.Evaluate(_timeProjection);
            currentPowerOfProjection -= (DecelerationOfProjection* ratio )* Time.deltaTime;
            if(transform.position.y<1)
            {
                isStickGround =true;
            }
            aura.SetActive( true);
        }
        else
        {
            aura.SetActive(false);
         
        }

         
        if(isStickGround)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
        Ray ray = new Ray(transform.position, DirProjection.normalized);
       // RaycastHit hit;

       /* if (Physics.Raycast(ray, out hit, currentPowerOfProjection * Time.deltaTime) && hit.collider.tag == "wall")
        {
            DirProjection = Vector3.Reflect(DirProjection.normalized, hit.normal);
        }*/
    }



    public void Update()
    {
    
        if(line.strenghOfExpulsion != expulsionStrengh)
        {
            TransmitionOfStrenghOfExpulsion();
        }
    }


    public void DeactiveStickHGround()
    {
    isStickGround = !isStickGround;

    }


    public void OnCollisionEnter(Collision collision)
    {
        Ray ray = new Ray(transform.position, DirProjection.normalized);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, currentPowerOfProjection);
       // if (collision.collider.tag == "wall")
        //{
          //  DirProjection = Vector3.Reflect(DirProjection.normalized, hit.normal);
        //}
    }

    public void TransmitionOfStrenghOfExpulsion()
    {
        line.strenghOfExpulsion = expulsionStrengh;
    }

    public void AddProjection(Vector3 dir)
    {
        Debug.Log("Propulsion");
        playerRigid.velocity = Vector3.zero;
        playerRigid.AddForce(dir.normalized * powerOfProjection, ForceMode.Impulse);
        DirProjection = dir;
        currentPowerOfProjection = powerOfProjection;
    }
     public void AddProjection(Vector3 dir, float power)
    {
        Debug.Log("Propulsion");
        playerRigid.velocity = Vector3.zero;
        playerRigid.AddForce(dir.normalized * power, ForceMode.Impulse);
        DirProjection = Vector3.Lerp(DirProjection,dir,0.5f);
        currentPowerOfProjection = power;
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

        if (StateAnim.state == StateAnim.CurrentState.Projection && currentPowerOfProjection == 0)
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

    public void Repulsion(GameObject ennemiGO ,Transform pos)
    {

        StateOfEntity ennemi = ennemiGO.GetComponent<StateOfEntity>();
        if (ennemi.entity != StateOfEntity.EntityState.Destroy)
        {
            Vector3 dir = ennemiGO.transform.position - transform.position;
            ennemi.DestroyProjection(true,dir);
           
            
        }
    }
    public void GoTransformation()
    {
        stock.DetachPlayer();
        currentPowerOfProjection = 0;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);

    }
}
