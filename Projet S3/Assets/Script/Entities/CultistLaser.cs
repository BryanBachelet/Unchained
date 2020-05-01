using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistLaser : MonoBehaviour
{

    public enum StateAttackCultist { Movement, Charging, Reload, FinishCharging, Attack }
    public StateAttackCultist attackCultist;
    public float speedOfDeplacement;
    public float distance;

    public GameObject spriteGo;

    public GameObject attackCollideGo;
    public float timeBeforeHit;
    private float _timeToHit;
    public float timeToCharge;

    public float timeReload;


    public int frameAttackTime;

    public LayerMask wallHit;

    public float strenghProjection;

    public float prediction;

    public float anglePrediction;

    private GameObject player;

    private CircleFormation circle;
    private BoxCollider attackCollider;
    private float _timeToCharge;
    private float _timeReload;
    private int _frameAttackTime;

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
    void Start()
    {
        attackCollider = attackCollideGo.GetComponent<BoxCollider>();
        cultistLaserEffect = attackCollideGo.GetComponent<CultistLaserEffect>();
        player = PlayerMoveAlone.Player1;
        circle = GetComponent<CircleFormation>();
        spriteRend = spriteGo.GetComponent<SpriteRenderer>();
        distance = distance + ((((circle.childEntities.Length / circle.numberByCircle) * circle.sizeBetweenCircle) - circle.sizeBetweenCircle) + circle.radiusAtBase);
        lasersScript = myMouseTargetLasersScript.GetComponent<MouseTargetLasers>();
        ConLaserScript = myMouseTargetLasersScript.GetComponentInChildren<ConLaser>();
    }

    // Update is called once per frame
    void Update()
    {
        if (test == false)
        {

            if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable && circle.attack == 1)
            {
                switch (attackCultist)
                {

                    case (StateAttackCultist.Movement):
                        if (Vector3.Distance(transform.position, player.transform.position) > distance)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedOfDeplacement * Time.deltaTime);
                        }
                        else
                        {
                            ChangeStateAttack(StateAttackCultist.Charging);
                        }


                        break;


                    case (StateAttackCultist.Charging):

                        posToShoot = PredictAttack();

                        float angle = Vector3.SignedAngle(Vector3.forward, (posToShoot - transform.position).normalized, Vector3.up);
                        spriteGo.transform.rotation = Quaternion.Euler(spriteGo.transform.eulerAngles.x, angle - 90, spriteGo.transform.eulerAngles.z);
                        RaycastHit hit;

                        if (Physics.Raycast(transform.position + Vector3.up, spriteGo.transform.right, out hit, Mathf.Infinity, wallHit))
                        {
                            spriteRend.size = new Vector2(Vector3.Distance(transform.position, hit.point), spriteRend.size.y);
                        }
                        if (_timeToCharge > timeToCharge)
                        {
                            ChangeStateAttack(StateAttackCultist.FinishCharging);
                        }
                        else
                        {
                            _timeToCharge += Time.deltaTime;
                        }

                        break;

                    case (StateAttackCultist.FinishCharging):

                        if (_timeToHit > timeBeforeHit)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot(shotSound, transform.position);
                            ChangeStateAttack(StateAttackCultist.Attack);



                        }
                        else
                        {
                            _timeToHit += Time.deltaTime;
                        }

                        break;

                    case (StateAttackCultist.Attack):

                        _frameAttackTime++;
                        if (_frameAttackTime > frameAttackTime)
                        {
                            ChangeStateAttack(StateAttackCultist.Reload);
                        }
                        break;


                    case (StateAttackCultist.Reload):


                        if (_timeReload > timeReload)
                        {
                            ChangeStateAttack(StateAttackCultist.Movement);
                        }
                        else
                        {
                            _timeReload += Time.deltaTime;
                        }

                        break;

                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, (speedOfDeplacement - 1) * Time.deltaTime);
                _timeToHit = 0;
                _timeToCharge = 0;
                spriteGo.SetActive(false);
                attackCollideGo.GetComponent<MeshRenderer>().enabled = false;
                attackCollider.enabled = false;
                ChangeStateAttack(StateAttackCultist.Movement);
            }
        }
        else
        {
            if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
            {
                if (isAttacking)
                {
                    if(launchLaser)
                    {
                        lasersScript.startWavePS.Emit(1);
                        ConLaserScript.hitPsArray[1].Emit(100);

                        lasersScript.startParticles.Emit(lasersScript.startParticlesCount);
                        launchLaser = false;
                    }
                    float angle = Vector3.SignedAngle(Vector3.forward, (player.transform.position - transform.position).normalized, Vector3.up);
                    spriteGo.transform.rotation = Quaternion.Euler(spriteGo.transform.eulerAngles.x, angle - 90, spriteGo.transform.eulerAngles.z);
                    RaycastHit hit;
                    float distanceHit = 0;
                    if (Physics.Raycast(transform.position + Vector3.up, spriteGo.transform.right, out hit, Mathf.Infinity, wallHit))
                    {
                        distanceHit = Vector3.Distance(transform.position, hit.point);
                        hitPos = hit.point;
                        spriteRend.size = new Vector2(Vector3.Distance(transform.position, hit.point), spriteRend.size.y);
                    }
                    #region Collider
                    attackCollideGo.transform.rotation = Quaternion.Euler(attackCollideGo.transform.eulerAngles.x, angle, attackCollideGo.transform.eulerAngles.z);
                    attackCollideGo.transform.position = transform.position + (hit.point - transform.position).normalized * (distanceHit / 2);
                    attackCollideGo.transform.localScale = new Vector3(spriteRend.size.y, 5, distanceHit);

                    //attackCollideGo.GetComponent<MeshRenderer>().enabled = true;
                    //attackCollider.enabled = true;
                    myMouseTargetLasersScript.SetActive(true);
                    lasersScript.mouseWorldPosition = hitPos;
                    lasersScript.anim.SetBool("Fire", true);
                    ConLaserScript.globalProgress = 0;


                    #endregion
                }
                else
                {
                    ConLaserScript.globalProgress += Time.deltaTime * ConLaserScript.globalProgressSpeed;
                    //attackCollideGo.GetComponent<MeshRenderer>().enabled = false;
                    //attackCollider.enabled = false;
                    lasersScript.anim.SetBool("Fire", false);
                    if(ConLaserScript.globalProgress > 1)
                    {
                        myMouseTargetLasersScript.SetActive(false);
                    }


                }
                if (_AttackTime > timeAttack)
                {
                    isAttacking = isAttacking == false ? true : false;
                    _AttackTime = 0;
                    launchLaser = true;
                }
                _AttackTime += Time.deltaTime;
            }
        }

    }


    public void ChangeStateAttack(StateAttackCultist attackCultistState)
    {
        switch (attackCultistState)
        {

            case (StateAttackCultist.Movement):

                attackCultist = attackCultistState;

                break;

            case (StateAttackCultist.Charging):

                spriteGo.SetActive(true);
                attackCultist = attackCultistState;
                _timeToCharge = 0;
                break;

            case (StateAttackCultist.FinishCharging):
                spriteGo.GetComponent<SpriteRenderer>().color = Color.black;
                float distanceHit = 0;
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, spriteGo.transform.right, out hit, Mathf.Infinity, wallHit))
                {
                    distanceHit = Vector3.Distance(transform.position, hit.point);
                    spriteRend.size = new Vector2(distanceHit, spriteRend.size.y);
                }



                Debug.DrawLine(transform.position, hit.point, Color.blue);
                float angle = Vector3.SignedAngle(Vector3.forward, (hit.point - transform.position).normalized, Vector3.up);
                attackCollideGo.transform.rotation = Quaternion.Euler(attackCollideGo.transform.eulerAngles.x, angle, attackCollideGo.transform.eulerAngles.z);
                attackCollideGo.transform.position = transform.position + (hit.point - transform.position).normalized * (distanceHit / 2);
                attackCollideGo.transform.localScale = new Vector3(spriteRend.size.y, 5, distanceHit);

                cultistLaserEffect.ResetAttact();

                cultistLaserEffect.ejectionDirection = (hit.point - transform.position).normalized;
                cultistLaserEffect.ejectionForce = strenghProjection;

                _timeToHit = 0;
                attackCultist = attackCultistState;

                break;

            case (StateAttackCultist.Attack):

                attackCollideGo.GetComponent<MeshRenderer>().enabled = true;
                attackCollider.enabled = true;
                attackCultist = attackCultistState;
                _frameAttackTime = 0;
                break;


            case (StateAttackCultist.Reload):
                spriteGo.GetComponent<SpriteRenderer>().color = Color.white;
                spriteGo.SetActive(false);
                attackCollideGo.GetComponent<MeshRenderer>().enabled = false;
                attackCollider.enabled = false;
                attackCultist = attackCultistState;
                _timeReload = 0;
                break;
        }
    }

    public Vector3 PredictAttack()
    {
        Vector3 pos = Vector3.zero;
        if (StateAnim.state == StateAnim.CurrentState.Rotate)
        {

            if (rotation == null)
            {
                rotation = PlayerMoveAlone.Player1.GetComponent<RotationPlayer>();
            }
            Vector3 dir = (PlayerMoveAlone.Player1.transform.position - rotation.pointPivot);
            float distance = Vector3.Distance(PlayerMoveAlone.Player1.transform.position, rotation.pointPivot);


            if (rotation.right)
            {
                pos = rotation.pointPivot + (Quaternion.Euler(0, 50, 0) * dir.normalized * distance);
            }
            else
            {
                pos = rotation.pointPivot + (Quaternion.Euler(0, -50, 0) * dir.normalized * distance);
            }

        }

        else
        {
            if (PlayerMoveAlone.playerRigidStatic.velocity.magnitude >= 0.5f)
            {
                pos = PlayerMoveAlone.Player1.transform.position + PlayerMoveAlone.playerRigidStatic.velocity.normalized * prediction;
            }
            else
            {
                pos = PlayerMoveAlone.Player1.transform.position;
            }

        }
        return pos;

    }

}
