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
    public bool isMoving = true;
    public Vector3 moveTo = new Vector3(4.2f, 0, 34.9f);
    public float radiusRNDTogo;
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
        moveTo = new Vector3(4.2f, 0, 34.9f) + Random.insideUnitSphere * radiusRNDTogo;
    }

    // Update is called once per frame
    void Update()
    {
        if (test != false)
        {

            if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
            {
                if (!isMoving)
                {
                    if (isAttacking)
                    {
                        if (launchLaser)
                        {
                            lasersScript.startWavePS.Emit(1);
                            ConLaserScript.hitPsArray[1].Emit(100);

                            lasersScript.startParticles.Emit(lasersScript.startParticlesCount);
                            launchLaser = false;
                        }
                        if (playPreviousPos.Count <= maxFrameDelayAim)
                        {
                            playPreviousPos.Add(player.transform.position);
                        }
                        else
                        {
                            playPreviousPos.RemoveAt(0);
                            playPreviousPos.Add(player.transform.position);
                        }
                        float angle = Vector3.SignedAngle(Vector3.forward, (playPreviousPos[0] - transform.position).normalized, Vector3.up);
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
                        if (ConLaserScript.globalProgress > 1)
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
                else
                {
                    if(Vector3.Distance(transform.position, new Vector3(moveTo.x, 1, moveTo.z)) > 1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveTo.x, 1, moveTo.z), 8 * Time.deltaTime);
                        Debug.Log("je me déplace vraiment vite");
                    }
                    else
                    {
                        isMoving = false;
                    }
                }
            }
            if (StateOfGames.currentState == StateOfGames.StateOfGame.Cinematic || StateOfGames.currentState == StateOfGames.StateOfGame.Transformation || StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
            {
                if (launchLaser)
                {
                    ConLaserScript.globalProgress = 1;
                    _AttackTime = 0;
                    isAttacking = false;
                    myMouseTargetLasersScript.SetActive(false);
                    launchLaser = false;
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(4.2f, 1, 34.9f), radiusRNDTogo);
        Gizmos.DrawSphere(new Vector3(moveTo.x, 1, moveTo.z), 1f);
    }

}
