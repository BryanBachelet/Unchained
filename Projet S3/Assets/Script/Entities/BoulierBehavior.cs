using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulierBehavior : MonoBehaviour
{
    public enum DashEntityState
    {
        Preparation, Dash, Repos, Ejection
    }
    public DashEntityState dashState = DashEntityState.Preparation;

    private GameObject player;
    private float tempsEcoulePrep;
    public float tempsForPrep;
    private float tempsEcouleRepos;
    public float tempsForRepos;

    private Vector3 dirDash;
    public LayerMask wallHit;
    public float speed;
    private MeshRenderer myMR;
    private RaycastHit hit;
    private bool checkStich = false;
    public bool isGrab = false;
    private Vector3 stichPos;
    private Vector3 posOnRepos = Vector3.zero;

    private bool isFall = false;

    private StateOfEntity stateOfEntity;

    public float distanceStopWall = 1;

    public float distanceDead = 100;

    private AnimBoulier animBoulier;
    private Rigidbody rigidbody;

    [FMODUnity.EventRef]
    public string chargeSound;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Init();
    }

    // Update is called once per frame
    void Update()
    { 
       
        Vector3 playerDir = player.transform.position - transform.position;
        float angleAgent = Vector3.SignedAngle(Vector3.forward, playerDir,Vector3.up);
        transform.eulerAngles = new Vector3(0,angleAgent,0);
        rigidbody.velocity = new Vector3(0,0,0);
        Debug.DrawLine(transform.position,hit.point);
        if(StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable && stateOfEntity.entity != StateOfEntity.EntityState.Destroy 
        && stateOfEntity.entity != StateOfEntity.EntityState.Dead )
        {

            switch (dashState)
            {
                case (DashEntityState.Preparation):

                    transform.LookAt(player.transform);
                    if (posOnRepos != Vector3.zero)
                    {
                        transform.position = posOnRepos;
                    }

                    tempsEcoulePrep += Time.deltaTime;

                    if (tempsEcoulePrep > tempsForPrep)
                    {
                        ChangeDashState(DashEntityState.Dash);
                    }

                    break;

                case (DashEntityState.Dash):
                    transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
                    if (!isFall)
                    {
                        transform.position += dirDash.normalized * speed * Time.deltaTime;
                    }
                    if (isGrab)
                    {
                        Debug.Log(true);
                        PlayerMoveAlone.Player1.transform.position = transform.position + stichPos;
                        ExitPlayer();
                
                    }else
                    {
                      
                       if( Vector3.Distance(transform.position,player.transform.position)<1.5f)
                       {
                           CatchPlayer(player);
                       }
                    }
                    
                        if(Vector3.Distance(transform.position, hit.point) < distanceStopWall)
                        {
                            if(isGrab)
                            {
                                animBoulier.ChangeState(AnimBoulier.StateColoss.Jet);
                                PlayerMoveAlone.Player1.GetComponent<LifePlayer>().AddDamage(30);
                                PlayerMoveAlone.Player1.GetComponent<PlayerMoveAlone>().AddProjection(-(hit.point -transform.position ).normalized, 60,30,false);
                                FMODUnity.RuntimeManager.PlayOneShot(chargeSound);
                            }    
                            ChangeDashState(DashEntityState.Repos);
                            posOnRepos = transform.position;
                        }



                    break;

                case (DashEntityState.Repos):

                    tempsEcouleRepos += Time.deltaTime;
                    transform.position = posOnRepos;
                    if (tempsEcouleRepos > tempsForRepos)
                    {
                        ChangeDashState(DashEntityState.Preparation);
                    }

                    break;

            }


        }

        if (stateOfEntity.entity == StateOfEntity.EntityState.Destroy)
        {
            ChangeDashState(DashEntityState.Ejection);

        }
        else
        {
            if (transform.position.y > 0)
            {
                transform.position += -Vector3.up * 3 * Time.deltaTime;
            }

            if (dashState == DashEntityState.Ejection)
            {

                if (dashState == DashEntityState.Ejection)
                {

                    ChangeDashState(DashEntityState.Repos);
                }

            }
            Vector3 center = new Vector3(4.2f, 0, 34.9f);

            if (Vector3.Distance(center, transform.position) > distanceDead)
            {

                ManageEntity.DestroyEntity(ManageEntity.EntityType.Coloss);
                Destroy(gameObject);
            }
        }
    }

    private void Init()
    {
        myMR = GetComponent<MeshRenderer>();
        player = PlayerMoveAlone.Player1;
        myMR.material.color = Color.blue;
        stateOfEntity = GetComponent<StateOfEntity>();
        animBoulier = GetComponent<AnimBoulier>();
    }

    private void ChangeDashState(DashEntityState stateChange)
    {
        switch (stateChange)
        {
            case (DashEntityState.Preparation):

                animBoulier.ChangeState(AnimBoulier.StateColoss.Projection);
                myMR.material.color = Color.blue;
                tempsEcoulePrep = 0;
                dashState = stateChange;

                break;

                case(DashEntityState.Dash):
                    
                    animBoulier.ChangeState(AnimBoulier.StateColoss.Charge);
                    dirDash = player.transform.position - transform.position;
                    Physics.Raycast(transform.position + Vector3.up, dirDash, out hit, Mathf.Infinity, wallHit);
                    hit.point = new Vector3(hit.point.x,1.5f,hit.point.z);
                    myMR.material.color = Color.black;
                    dashState = stateChange;

                break;

                case(DashEntityState.Repos):
                     
                    animBoulier.ChangeState(AnimBoulier.StateColoss.Idle);
                    tempsEcoulePrep = 0;
                    myMR.material.color = Color.cyan;
                    dashState = stateChange;
                    JustReset();

                break;
                case(DashEntityState.Ejection):
                     
                    animBoulier.ChangeState(AnimBoulier.StateColoss.Projection);
                    tempsEcoulePrep = 0;
                    myMR.material.color = Color.cyan;
                    dashState = stateChange;
                    JustReset();
                    
                     
                break;
        }

    }

    public void ExitPlayer()
    {
        if (PlayerMoveAlone.Player1.GetComponent<EnnemiStock>().ennemiStock != null)
        {
            isGrab = false;
            stichPos = Vector3.zero;
            gameObject.layer = 9;
            gameObject.tag = "Ennemi";
            checkStich = false;
        }
    }  public void JustReset()
    {
            isGrab = false;
            stichPos = Vector3.zero;
            gameObject.layer = 9;
            gameObject.tag = "Ennemi";
            checkStich = false;
            
    }

    public void CatchPlayer( GameObject collision)
    {
        if (dashState == DashEntityState.Dash && StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {
            if (checkStich == false)
            {
                checkStich = true;
                stichPos = collision.transform.position - transform.position;
                collision.GetComponent<EnnemiStock>().DetachPlayer();
                collision.GetComponent<PlayerMoveAlone>().currentPowerOfProjection= 0;
                isGrab = true;
                gameObject.layer = 0;
                gameObject.tag = "Untagged";
                animBoulier.ChangeState(AnimBoulier.StateColoss.Grap);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == PlayerMoveAlone.Player1)
        {
         // CatchPlayer(collision.collider);
        }

        
    }
    public void OnDestroy()
    {
        ManageEntity.DestroyEntity(ManageEntity.EntityType.Coloss);
    }
}
