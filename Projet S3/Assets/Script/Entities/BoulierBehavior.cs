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
    // Start is called before the first frame update
    void Start()
    {
        myMR = GetComponent<MeshRenderer>();
        player = PlayerMoveAlone.Player1;
        myMR.material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if(StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
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
            }

            if (dashState == DashEntityState.Dash)
            {
                myMR.material.color = Color.black;
                
                if(Vector3.Distance(transform.position, hit.point) > 20)
                {
                    //transform.Translate(dirDash.normalized * speed * Time.deltaTime);
                    transform.position += dirDash.normalized * speed * Time.deltaTime;
                }
                else
                {
                    tempsForRepos = 0;
                    dashState = DashEntityState.Repos;
                }
            }
            if(dashState == DashEntityState.Repos)
            {
                myMR.material.color = Color.cyan;
                if(tempsEcouleRepos < tempsForRepos)
                {
                    tempsEcouleRepos += Time.deltaTime;
                }
                else
                {
                    tempsEcoulePrep = 0;
                    dashState = DashEntityState.Preparation;
                }
            }
            if(PlayerMoveAlone.Player1.GetComponent<EnnemiStock>().ennemiStock != null)
            {
                isGrab = false;
                stichPos = Vector3.zero;
                gameObject.layer = 9;
                gameObject.tag = "Ennemi";
            }
            if(isGrab)
            {
                PlayerMoveAlone.Player1.transform.position = transform.position + stichPos;
                
            }
        }
    }

    private void ChangeDashState(DashEntityState stateChange)
    {
        switch(stateChange)
        {
                case(DashEntityState.Preparation):
                    myMR.material.color = Color.blue;

                break;
                case(DashEntityState.Dash):
                    dirDash = player.transform.position - transform.position;
                    Physics.Raycast(transform.position + Vector3.up, dirDash, out hit, Mathf.Infinity, wallHit);
                    dashState = stateChange;

                break;
                case(DashEntityState.Repos):

                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == PlayerMoveAlone.Player1)
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
    public void OnDestroy()
    {
        ManageEntity.DestroyEntity(ManageEntity.EntityType.Coloss);
    }
}
