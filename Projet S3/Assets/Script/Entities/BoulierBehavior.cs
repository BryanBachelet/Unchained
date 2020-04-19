using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulierBehavior : MonoBehaviour
{
    public enum DashEntityState
    {
        Preparation, Dash, Repos
    }
    public DashEntityState dashState = DashEntityState.Preparation;

    GameObject player;
    float tempsEcoulePrep;
    public float tempsForPrep;
    float tempsEcouleRepos;
    public float tempsForRepos;

    Vector3 dirDash;
    public LayerMask wallHit;
    public float speed;
    MeshRenderer myMR;
    RaycastHit hit;
    bool checkStich = false;
    public bool isGrab = false;
    Vector3 stichPos;

    private bool isFall = false;
    
    private StateOfEntity stateOfEntity;

    // Start is called before the first frame update
    void Start()
    {
        myMR = GetComponent<MeshRenderer>();
        player = PlayerMoveAlone.Player1;
        myMR.material.color = Color.blue;
        stateOfEntity = GetComponent<StateOfEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        if(StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable && stateOfEntity.entity != StateOfEntity.EntityState.Destroy )
        {

            switch(dashState) 
            {
                case(DashEntityState.Preparation):
                    
                    transform.LookAt(player.transform);
                    tempsEcoulePrep += Time.deltaTime;
                    
                    if(tempsEcoulePrep > tempsForPrep)
                    {
                        ChangeDashState(DashEntityState.Dash);
                    }
                
                break;

                case(DashEntityState.Dash):
                
                    if(!isFall)
                    {
                        transform.position += dirDash.normalized * speed * Time.deltaTime;
                        ExitPlayer();
                    }
                    else
                    {
                        
                        
                    }
                    if(isGrab)
                    {
                        PlayerMoveAlone.Player1.transform.position = transform.position + stichPos;
                
                    }
                    else
                    {
                        if(Vector3.Distance(transform.position, hit.point) < 20)
                        {
                            ChangeDashState(DashEntityState.Repos);
                        }
                    }

                

                break;

                case(DashEntityState.Repos):

                 tempsEcouleRepos += Time.deltaTime;
                 if(tempsEcouleRepos > tempsForRepos)
                 {
                         ChangeDashState(DashEntityState.Preparation);
                 }

                break;

            }

            
        }

        if(stateOfEntity.entity == StateOfEntity.EntityState.Destroy)
        {
            ChangeDashState(DashEntityState.Repos);
        }
    }

    private void ChangeDashState(DashEntityState stateChange)
    {
        switch(stateChange)
        {
                case(DashEntityState.Preparation):
                  
                    myMR.material.color = Color.blue;
                    tempsEcoulePrep = 0;
                    dashState = stateChange;
                  
                break;

                case(DashEntityState.Dash):
                    
                    dirDash = player.transform.position - transform.position;
                    Physics.Raycast(transform.position + Vector3.up, dirDash, out hit, Mathf.Infinity, wallHit);
                    myMR.material.color = Color.black;
                    dashState = stateChange;

                break;

                case(DashEntityState.Repos):
                     
                    tempsForRepos = 0;
                    myMR.material.color = Color.cyan;
                    dashState = DashEntityState.Repos;
                    dashState = stateChange;
                     
                break;
        }

    }

    public void ExitPlayer()
    {
        if(PlayerMoveAlone.Player1.GetComponent<EnnemiStock>().ennemiStock != null)
        {
            isGrab = false;
            stichPos = Vector3.zero;
            gameObject.layer = 9;
            gameObject.tag = "Ennemi";
        }
    }

    public void CatchPlayer( Collider collision)
    {
        if(dashState == DashEntityState.Dash && StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {
              if(checkStich == false)
            {
                checkStich = true;
                stichPos = collision.transform.position - transform.position;
                collision.gameObject.GetComponent<EnnemiStock>().DetachPlayer();
                isGrab = true;
                gameObject.layer = 0;
                gameObject.tag = "Untagged";
            }
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject == PlayerMoveAlone.Player1)
        {
          CatchPlayer(collision);

        }

        if(collision.tag =="Wall Layer")
        {
            isFall = true;
            transform.GetComponent<Rigidbody>().AddForce(dirDash.normalized* speed,ForceMode.Impulse);
        }
    }
    public void OnDestroy()
    {
        ManageEntity.DestroyEntity(ManageEntity.EntityType.Coloss);
    }
}
