using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colorful;

public class PlayerMoveAlone : MonoBehaviour
{
    public static Rigidbody playerRigidStatic;
    private Rigidbody playerRigid;
    private float speedOfDeplacement = 10;
    [Header("Projection")]
    public float powerOfProjection;
    public float DecelerationOfProjection = 60;
    public float currentDeprojectionOfProjection;
    
    public AnimationCurve ratioOfExpulsion;
    [HideInInspector] public Vector3 DirProjection;
   /* [HideInInspector]*/ public float currentPowerOfProjection;

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

    private Colorful.RadialBlur blur;

    private RotationPlayer rotationPlayer;

    private PlayerAnimState playerAnim;

    private bool isTouchWall =false;

    public int frameDetection = 3;

    private int compteurFrameDetection= 0;

    private void Awake()
    {
        Player1 = gameObject;
    }
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        stock = GetComponent<EnnemiStock>();
        isStickGround = true;
        stock.powerOfProjection = powerOfProjection;
        GetComponent<WallRotate>().powerOfProjection = powerOfProjection;
        playerRigidStatic = playerRigid = GetComponent<Rigidbody>();
        mouseScop = GetComponent<MouseScope>();
        if( line == null ) { line = transform.GetComponentInChildren<LineRend>(); }
        TransmitionOfStrenghOfExpulsion();
        currentPowerOfProjection = 0;
        blur = Camera.main.GetComponent<RadialBlur>();  
        rotationPlayer = GetComponent<RotationPlayer>(); 
        playerAnim = GetComponent<PlayerAnimState>();    
     //  currentPowerOfProjection = DecelerationOfProjection;
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
            if(stock.ennemiStock == null)
            {
                playerAnim.ChangeStateAnim(PlayerAnimState.PlayerStateAnim.Projection);
            }else
            {
                playerAnim.ChangeStateAnim(PlayerAnimState.PlayerStateAnim.Rotation);
            }
            blur.Strength += (0.1f/15);
            blur.Strength = Mathf.Clamp(blur.Strength,0,0.11f);
            _timeProjection  = 0;
            _timeProjection +=Time.deltaTime;
            float ratio =  ratioOfExpulsion.Evaluate(_timeProjection);
            currentPowerOfProjection -= (currentDeprojectionOfProjection* ratio )* Time.deltaTime;
            if(transform.position.y<1)
            {
                isStickGround =true;
            }
            aura.SetActive( true);
        }
        else
        {   
            if(stock.ennemiStock== null && mouseScop.instanceBullet == null)
            {
               
                playerAnim.ChangeStateAnim(PlayerAnimState.PlayerStateAnim.Idle);
            }
            blur.Strength -= (0.1f/15);
            blur.Strength = Mathf.Clamp(blur.Strength,0,1);
            _timeProjection  = 0;
            aura.SetActive(false);
           // blur.enabled = false;
        }

         
        if(isStickGround)
        {
           transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);
        }
        Ray ray = new Ray(transform.position, DirProjection.normalized );
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit, frameDetection* currentPowerOfProjection * Time.deltaTime) && hit.collider.gameObject.layer == 13)
        {
            isTouchWall = true;
            Debug.Log("isTouchWall");
        }
        if(isTouchWall)
        {
            if(compteurFrameDetection<frameDetection)
            { 
                compteurFrameDetection++;
            }
            else
            {                
                Debug.Log("Reflet");
                DirProjection = Vector3.Reflect(DirProjection.normalized, hit.normal);
            }
        }
            
        
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
        if (collision.collider.gameObject.layer == 13)
        {
          DirProjection = Vector3.Reflect(DirProjection.normalized, hit.normal);
        }
    }

 

    public void TransmitionOfStrenghOfExpulsion()
    {
        line.strenghOfExpulsion = expulsionStrengh;
    }

    public void AddProjection(Vector3 dir)
    {
        StateAnim.ChangeState(StateAnim.CurrentState.Projection);
        playerRigid.velocity = Vector3.zero;
        playerRigid.AddForce(dir.normalized * powerOfProjection, ForceMode.Impulse);
        DirProjection = dir.normalized;
        currentDeprojectionOfProjection = DecelerationOfProjection;
        currentPowerOfProjection = powerOfProjection;
    }
     public void AddProjection(Vector3 dir, float power)
    {
        StateAnim.ChangeState(StateAnim.CurrentState.Projection);     
        playerRigid.velocity = Vector3.zero;
        playerRigid.AddForce(dir.normalized * power, ForceMode.Impulse);
        DirProjection = dir.normalized;
        currentDeprojectionOfProjection = DecelerationOfProjection;
        currentPowerOfProjection = power;
    }
       public void AddProjection(Vector3 dir, float power, float deprojection )
    {
        StateAnim.ChangeState(StateAnim.CurrentState.Projection);     
        playerRigid.velocity = Vector3.zero;
        playerRigid.AddForce(dir.normalized * power, ForceMode.Impulse);
        DirProjection = dir.normalized;
        currentDeprojectionOfProjection = deprojection;
        currentPowerOfProjection = power;
    }

    public void AnimationAvatar()
    {
        if (StateAnim.state == StateAnim.CurrentState.Projection || StateAnim.state == StateAnim.CurrentState.Idle )
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
        stock.DetachPlayer(true);
        currentPowerOfProjection = 0;
        
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        aura.SetActive(false);
        playerAnim.ChangeStateAnim(PlayerAnimState.PlayerStateAnim.EntraveStart);
        this.enabled =false;
    }
    public void  StopVelocity()
    {
        playerRigid.velocity = Vector3.zero;
    }
}
