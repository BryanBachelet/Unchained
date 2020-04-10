using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistLaser : MonoBehaviour
{

    public enum StateAttackCultist{ Movement, Charging, Reload,FinishCharging, Attack}
    public StateAttackCultist attackCultist;
    public float speedOfDeplacement ;
    public float distance;

    public GameObject spriteGo; 
    
    public GameObject  attackCollideGo;
    public float timeBeforeHit;
    private float _timeToHit;
    public float timeToCharge;

    public float timeReload;


    public int frameAttackTime;

    public LayerMask wallHit;

    public float strenghProjection;

    private GameObject player;

    private CircleFormation circle;
    private BoxCollider attackCollider;
    private float _timeToCharge;
    private float _timeReload;
    private int _frameAttackTime;

    private SpriteRenderer spriteRend;
    private CultistLaserEffect cultistLaserEffect;

    // Start is called before the first frame update
    void Start()
    {
        attackCollider = attackCollideGo.GetComponent<BoxCollider>();
        cultistLaserEffect = attackCollideGo.GetComponent<CultistLaserEffect>();
        player = PlayerMoveAlone.Player1;
        circle = GetComponent<CircleFormation>();
        spriteRend = spriteGo.GetComponent<SpriteRenderer>();
        distance =  distance + ((((circle.childEntities.Length/circle.numberByCircle) * circle.sizeBetweenCircle) -circle.sizeBetweenCircle) + circle.radiusAtBase );
    
       
    }

    // Update is called once per frame
    void Update()
    {

        if(StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {    
            switch (attackCultist)
            {

            case(StateAttackCultist.Movement):
            if(Vector3.Distance(transform.position,player.transform.position)> distance )
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedOfDeplacement*Time.deltaTime);
            }
            else
            {
            ChangeStateAttack( StateAttackCultist.Charging);
            }
            

            break;


            case (StateAttackCultist.Charging) :

                float angle = Vector3.SignedAngle( Vector3.forward ,  (player.transform.position -transform.position).normalized, Vector3.up );
                spriteGo.transform.rotation =  Quaternion.Euler(spriteGo.transform.eulerAngles.x, angle - 90 ,spriteGo.transform.eulerAngles.z);
                RaycastHit hit ;
                Debug.DrawRay(transform.position + Vector3.up,spriteGo.transform.right*100);
                if(Physics.Raycast(transform.position + Vector3.up,spriteGo.transform.right, out hit,Mathf.Infinity,wallHit))
                {    
                    Debug.Log(hit.collider.gameObject);
                    spriteRend.size = new Vector2(Vector3.Distance(transform.position,  hit.point), spriteRend.size.y);
                }
            
                if(_timeToCharge>timeToCharge)
                {
                    ChangeStateAttack(StateAttackCultist.FinishCharging);

                }
                else
                {
                    _timeToCharge +=Time.deltaTime;
                }

            break;

            case(StateAttackCultist.FinishCharging):
            
                if(_timeToHit>timeBeforeHit)
                {
                    ChangeStateAttack(StateAttackCultist.Attack);

                }
                else
                {
                    _timeToHit +=Time.deltaTime;
                }

            break;

            case(StateAttackCultist.Attack):
            
            _frameAttackTime++ ;
            if(_frameAttackTime>frameAttackTime)
            {
                ChangeStateAttack(StateAttackCultist.Reload);
            } 
            break;


            case(StateAttackCultist.Reload):


            if(_timeReload>timeReload)
            {
                ChangeStateAttack(StateAttackCultist.Movement);
            }else
            {
                _timeReload +=Time.deltaTime;
            }
            
            break;

            }
        }else
        {
              transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, (speedOfDeplacement-1)*Time.deltaTime);
        }
        
    }


    public void ChangeStateAttack(StateAttackCultist attackCultistState)
     {
           switch (attackCultistState)
        {

            case(StateAttackCultist.Movement) :

            attackCultist= attackCultistState;

            break;

            case(StateAttackCultist.Charging):
            
            spriteGo.SetActive(true);
            attackCultist = attackCultistState;
            _timeToCharge = 0;
            break;

            case (StateAttackCultist.FinishCharging):
            float distanceHit = 0;
            RaycastHit hit ;
            if(Physics.Raycast(transform.position + Vector3.up,spriteGo.transform.right, out hit,Mathf.Infinity,wallHit))
            {   
                distanceHit = Vector3.Distance(transform.position,  hit.point);
                spriteRend.size = new Vector2(distanceHit, spriteRend.size.y);
            } 
            


            Debug.DrawLine(transform.position, hit.point, Color.blue);
            float angle = Vector3.SignedAngle(Vector3.forward,  (hit.point -transform.position).normalized, Vector3.up );
            attackCollideGo.transform.rotation = Quaternion.Euler(attackCollideGo.transform.eulerAngles.x, angle, attackCollideGo.transform.eulerAngles.z);
            attackCollideGo.transform.position = transform.position + (hit.point - transform.position).normalized *(distanceHit/2); 
            attackCollideGo.transform.localScale = new Vector3(spriteRend.size.y ,  5, distanceHit); 
          
            cultistLaserEffect.ResetAttact();
         
            cultistLaserEffect.ejectionDirection = (hit.point -transform.position).normalized;
            cultistLaserEffect.ejectionForce = strenghProjection;

            _timeToHit = 0;
            attackCultist = attackCultistState;

            break;

            case (StateAttackCultist.Attack):
           
            attackCollideGo.GetComponent<MeshRenderer>().enabled =true;
            attackCollider.enabled = true;
            attackCultist = attackCultistState;
            _frameAttackTime = 0;
            break;


            case(StateAttackCultist.Reload):
                spriteGo.SetActive(false);
                attackCollideGo.GetComponent<MeshRenderer>().enabled =false;
                attackCollider.enabled = false;
                attackCultist = attackCultistState;
                _timeReload =0;
            break;
        }
     }
}
