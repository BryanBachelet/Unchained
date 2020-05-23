using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistLaser : MonoBehaviour
{


    public float distance;

    public GameObject spriteGo;

    public GameObject attackCollideGo;


    public LayerMask wallHit;


    private GameObject player;

    private CircleFormation circle;
    private BoxCollider attackCollider;


    private SpriteRenderer spriteRend;
    private CultistLaserEffect cultistLaserEffect;

    [FMODUnity.EventRef]
    public string shotSound;

    private RotationPlayer rotation;

    private Vector3 posToShoot;

    [Header("Test")]
    public bool test;

    public bool isAttacking;
    public float timeAttack;
    private float _AttackTime;

    public GameObject myMouseTargetLasersScript;
    private ConLaser ConLaserScript;
    private MouseTargetLasers lasersScript;
    private Vector3 hitPos;
    bool launchLaser = false;
    public int maxFrameDelayAim = 10;
    public List<Vector3> playPreviousPos = new List<Vector3>();
    public float LaserDamagePerSecond = 10;
    private LifePlayer lifePlayer;
    public bool isMoving = true;
    public Vector3 moveTo = new Vector3(4.2f, 0, 34.9f);
    public float radiusRNDTogo;

    public float timingMouvement = 2;

    public float compteurMouvement;

    public float playerDistanceMove;

    public float speedMouvement = 10;

    public float laserPositionHeight = 1;

    public float percentShootTiming;

    void Start()
    {
        attackCollider = attackCollideGo.GetComponent<BoxCollider>();
        cultistLaserEffect = attackCollideGo.GetComponent<CultistLaserEffect>();
        player = PlayerMoveAlone.Player1;
        circle = GetComponent<CircleFormation>();
        circle.activeCircle = true;
        spriteRend = spriteGo.GetComponent<SpriteRenderer>();
        distance = distance + ((((circle.childEntities.Length / circle.numberByCircle) * circle.sizeBetweenCircle) - circle.sizeBetweenCircle) + circle.radiusAtBase);
        lasersScript = myMouseTargetLasersScript.GetComponent<MouseTargetLasers>();
        ConLaserScript =  myMouseTargetLasersScript.GetComponentInChildren<ConLaser>();
        moveTo = new Vector3(4.2f, 0, 34.9f) + Random.insideUnitSphere * radiusRNDTogo;
        moveTo = new Vector3(moveTo.x,0,moveTo.z);
        isMoving =true;
        percentShootTiming = Random.Range(100,200);
    }

    // Update is called once per frame
    void Update()
    {
            if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
            {
                if (!isMoving)
                {
                    if (isAttacking  & circle.attack == 1)
                    {
                       
                        ActivationLaser();
                        LaserHit();
                    }  
                        circle.activeCircle = true;
                        GetPlayerPosition();
                        OrientationEntities();
                        OrientationCultist();                   
                        TimingAttack();
                }
                else
                {
                    
                        
                    if(StateOfGames.currentPhase != StateOfGames.PhaseOfDefaultPlayable.Phase3)
                    {
                        MouvementPlay( moveTo,1f);
                    }
                    else
                    {   
                        MouvementPlay(PlayerMoveAlone.playerPos , playerDistanceMove);
                    }
                }
            }
            if (StateOfGames.currentState == StateOfGames.StateOfGame.Cinematic || StateOfGames.currentState == StateOfGames.StateOfGame.Transformation)
            {

                BreakBehavior();
            }
    }
    
    

    public void OrientationCultist()
    {
        for(int i= 0;i <circle.childEntities.Length;i++)
        {
            if(Vector3.SignedAngle(Vector3.forward, spriteGo.transform.right, Vector3.up)!=0)
            {
                if(Vector3.Distance(circle.childEntities[i].transform.position,transform.position)<= circle.radiusAtBase)
                {
                    float angleAgent = Vector3.SignedAngle(Vector3.forward,spriteGo.transform.right,Vector3.up);
                    circle.childEntities[i].transform.eulerAngles =  new Vector3(0, angleAgent,0);      
                }        
            }             
        }                   
    }

    public void BreakBehavior()
    {
        isAttacking = false;
        _AttackTime = 0;
        launchLaser = true;
        isMoving = true;
        myMouseTargetLasersScript.SetActive(false);
    }

    public void ActivationLaser()
    {
         if (launchLaser)
        {   
            //Laser
            lasersScript.startWavePS.Emit(1);
            ConLaserScript.hitPsArray[1].Emit(100);
            lasersScript.startParticles.Emit(lasersScript.startParticlesCount);
            myMouseTargetLasersScript.SetActive(true);

            //Animation
            circle.AnimRituel(Anim_Cultist_States.AnimCultistState.Invocation_Idle);
            launchLaser = false;
        }
    }

    public void GetPlayerPosition()
    {
        if (playPreviousPos.Count <= maxFrameDelayAim)
        {
            playPreviousPos.Add(player.transform.position);
        }
        else
        {
            playPreviousPos.RemoveAt(0);
            playPreviousPos.Add(player.transform.position);
        }
    }

    public void TimingAttack()
    {
        if(_AttackTime>timeAttack)
        {
            isAttacking = false;
            isMoving = true;
            myMouseTargetLasersScript.SetActive(false);
            _AttackTime  = 0;
            percentShootTiming = Random.Range(100,200);
        }
        
        _AttackTime +=Time.deltaTime;

    }
    public void LaserHit()
    {
        RaycastHit hit;
        float distanceHit = 0;
        myMouseTargetLasersScript.transform.position = transform.position+ Vector3.up* laserPositionHeight;
        if (Physics.Raycast(transform.position, spriteGo.transform.right, out hit, Mathf.Infinity, wallHit))
        {
            distanceHit = Vector3.Distance(transform.position, hit.point);
            hitPos = hit.point;
            spriteRend.size = new Vector2(Vector3.Distance(transform.position, hit.point), spriteRend.size.y);
            if(hit.collider.gameObject.layer == 10)
            {
                
                if(lifePlayer == null)
                {
                    lifePlayer = hit.collider.gameObject.GetComponent<LifePlayer>();
                }
                lifePlayer.AddDamage(LaserDamagePerSecond*Time.deltaTime);
            }
        }
       
        lasersScript.mouseWorldPosition = hitPos;
        lasersScript.anim.SetBool("Fire", true);
        ConLaserScript.globalProgress = 0;
    }

    public void OrientationEntities()
    {
        float angle = Vector3.SignedAngle(Vector3.forward, (playPreviousPos[0] - transform.position).normalized, Vector3.up);
        spriteGo.transform.rotation = Quaternion.Euler(spriteGo.transform.eulerAngles.x, angle - 90, spriteGo.transform.eulerAngles.z);           
    }

    public void MouvementPlay( Vector3 position,float distance)
    {
          circle.AnimRituel(Anim_Cultist_States.AnimCultistState.Run);
     
        Vector3 dirProjection =  position - transform.position;
        if(Vector3.Distance(transform.position,position) > distance)
         {
            transform.position = Vector3.MoveTowards(transform.position, position, 10 * Time.deltaTime);
            for(int i= 0;i <circle.childEntities.Length;i++)
            {
                if(Vector3.SignedAngle(Vector3.forward, dirProjection.normalized, Vector3.up)!=0)
                {
                        if(Vector3.Distance(circle.childEntities[i].transform.position,transform.position)<= circle.radiusAtBase)
                    {
                        float angle = Vector3.SignedAngle(Vector3.forward,dirProjection.normalized,Vector3.up);
                        circle.childEntities[i].transform.eulerAngles =  new Vector3(0, angle,0);    
                    }        
                }     
            }
        }

       if(compteurMouvement>timingMouvement * (percentShootTiming/100))
       {
            isMoving =false;
            isAttacking = true;
            launchLaser = true;
            compteurMouvement = 0;
       }
       else
       {
           compteurMouvement += Time.deltaTime;
       }
    }
   
}
