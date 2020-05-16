using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFormation : MonoBehaviour
{
    public GameObject[] childEntities;
    public int numberByCircle;
    public float radiusAtBase;
    public float sizeBetweenCircle;
    public float speedAgent;
    public float positionMouvement;
    public float rotateRituelSpeed =50;
    public bool activeRituel;
    public float timeForInvoq;
    public bool startInvoq = false;
    public float tempsEcouleInvoq;
    public int attack;
    public GameObject invoq1;
    public GameObject fbCastInvoq;

    private float radiusUse;
    private int currentCircleNumber;
    private float angleByCircle;
    private bool doDestruct;
    private float[] compteurOfMouvement;
    private float angle;
    private EntitiesManager entityManage;

private bool activeRunPlayer;

    void Start()
    {
        childEntities = new GameObject[transform.childCount - 4];
        for (int i = 0; i < transform.childCount - 4; i++)
        {

            childEntities[i] = transform.GetChild(i).gameObject;

        }
        compteurOfMouvement = new float[childEntities.Length];
        entityManage = GetComponent<EntitiesManager>();
    }

    // Update is called once per frame
    void Update()
    {

            Formation();
        if (StateOfGames.currentPhase != StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
        }
        // else
        // {
        //     activeRunPlayer =true;
        // }
        // if(activeRunPlayer == true && StateOfGames.currentState !=StateOfGames.StateOfGame.Transformation )
        // {
        //     RunPlayer();
        // }
        

        if (startInvoq && activeRituel)
        {
          
            tempsEcouleInvoq += Time.deltaTime;
            if(fbCastInvoq.transform.localScale.x < 5)
            {
                fbCastInvoq.transform.localScale = new Vector3(1 + tempsEcouleInvoq, 1 + tempsEcouleInvoq, 1 + tempsEcouleInvoq);
            }

            if (tempsEcouleInvoq > timeForInvoq)
            {
                
                if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Coloss))
                {
                    Instantiate(invoq1, fbCastInvoq.transform.position, fbCastInvoq.transform.rotation);
                    tempsEcouleInvoq = 0;
                }
            }



        }
        if(!activeRituel)
        {
            startInvoq =false;
            tempsEcouleInvoq = 0;
            if(fbCastInvoq.transform.localScale.x>0.1 && fbCastInvoq.activeInHierarchy)
            {
                fbCastInvoq.transform.position = new Vector3 (entityManage.pointToGo.transform.position.x ,fbCastInvoq.transform.position.y, entityManage.pointToGo.transform.position.z);
                fbCastInvoq.transform.localScale -= Vector3.one  *10*Time.deltaTime;  
            }else
            {
                 fbCastInvoq.SetActive(false);
            }
        }

        Destruct();
    }

public int CurrentActiveChild()
{
    int k = 0;
    for (int i = 0; i < childEntities.Length; i++)
        {
            if (childEntities[i].GetComponent<StateOfEntity>() && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
            {
              k++;   
            }
        }
    return k;
}

public void ActiveRunPlayer()
{
    activeRunPlayer = true;
}
    private void Destruct()
    {
        doDestruct = true;
        for (int i = 0; i < childEntities.Length; i++)
        {
            if (childEntities[i].GetComponent<StateOfEntity>() && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
            {
                doDestruct = false;
                break;
            }

        }
        if (doDestruct)
        {
            if(entityManage.cultisteBehavior == EntitiesManager.BeheaviorCultiste.Harass)
            {
                ManageEntity.DestroyEntity(ManageEntity.EntityType.Distance);
            }
            else
            {
                ManageEntity.DestroyEntity(ManageEntity.EntityType.Cultiste);
            }

            if(activeRituel)
            {
                ManageEntity.SetActiveRitualPoint(entityManage.pointToGo.gameObject,false);
            }


            Destroy(gameObject);

        }
    }


    private void Formation()
    { 
        int numFor = 0;
        if(activeRituel)
        {     
            //angle += rotateRituelSpeed*Time.deltaTime;
        }
        for (int i = 0; i < childEntities.Length ; i++)
        {
            if (i >= numberByCircle * currentCircleNumber)
            {
                NewCircle();
            }
            Vector3 pos = new Vector3(0, 0, 0);
            Vector3 transformFor = transform.forward;
            pos = transform.position + (Quaternion.Euler(0, (angle) + (angleByCircle * i), 0)*  transform.forward * radiusUse);
            if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead && childEntities[i].GetComponent<StateOfEntity>())
            {
                float distanceDestination = Vector3.Distance(childEntities[i].transform.position, pos);
               
                if (distanceDestination > 0.01f)
                {
                    Vector3 dir = pos - childEntities[i].transform.position;
                    if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch)
                    {
                        /*if(activeRituel)
                        {
                            childEntities[i].transform.position += Quaternion.Euler(0,angle,0)*childEntities[i].transform.forward;
                            Quaternion.Euler(0,angle,0)*(dir.normalized* Vector3.Distance(childEntities[i].transform.position , transform.position))
                        }*/

                         
                        if (distanceDestination > 1f)
                        {
                            Vector3 testPosCamView = Camera.main.WorldToScreenPoint(childEntities[i].transform.position);
                            if(testPosCamView.x > 0 || testPosCamView.x < 1920 || testPosCamView.y < 0 || testPosCamView.y > 1080)
                            {
                                childEntities[i].transform.position += (dir.normalized * speedAgent * 4 * Time.deltaTime);
                            }
                            childEntities[i].transform.position += (dir.normalized * speedAgent * Time.deltaTime);
                            childEntities[i].transform.eulerAngles = Vector3.zero;
                            childEntities[i].GetComponent<StateOfEntity>().entity = StateOfEntity.EntityState.ReturnFormation;

                            if(Vector3.SignedAngle(Vector3.forward, dir.normalized, Vector3.up)!=0)
                            {
                                float angle = Vector3.SignedAngle(Vector3.forward,dir.normalized,Vector3.up);
                                childEntities[i].transform.eulerAngles =  new Vector3(0, angle,0);
                            
                            }               
                        }
                        else
                        {
                            
                            childEntities[i].transform.position = Vector3.Lerp(childEntities[i].transform.position, pos,20*Time.deltaTime);
                            childEntities[i].transform.eulerAngles = Vector3.zero;
                            childEntities[i].GetComponent<StateOfEntity>().entity = StateOfEntity.EntityState.Formation;
                            
                            Vector3 dirProjection = entityManage.pointToGo.transform.position - childEntities[i].transform.position;
           
                            if(Vector3.SignedAngle(Vector3.forward, dirProjection.normalized, Vector3.up)!=0)
                            {
                                float angle = Vector3.SignedAngle(Vector3.forward,dirProjection.normalized,Vector3.up);
                                childEntities[i].transform.eulerAngles =  new Vector3(0, angle,0);
                            
                            }               
                        }
                    }


                }
                    if (distanceDestination < 6)
                {

                    numFor++;
                }
            }
        }

        if(numFor>10)
        {
            attack = 1;
        }
        if(numFor<10 && attack != 0) 
        {
            attack =-1;
        }
        ResetCircle();
    }

    public void RunPlayer()
    {
        for (int i = 0; i < childEntities.Length; i++)
        {
            if (childEntities[i].GetComponent<StateOfEntity>() && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
            {
                Vector3 testPosCamView = Camera.main.WorldToScreenPoint(childEntities[i].transform.position);
                if (testPosCamView.x > 0 || testPosCamView.x < 1920 || testPosCamView.y < 0 || testPosCamView.y > 1080)
                {
                    childEntities[i].transform.position = Vector3.MoveTowards(childEntities[i].transform.position, PlayerMoveAlone.Player1.transform.position, 8 * speedAgent * Time.deltaTime);
                }
                else
                {
                    childEntities[i].transform.position = Vector3.MoveTowards(childEntities[i].transform.position, PlayerMoveAlone.Player1.transform.position, 2 * speedAgent * Time.deltaTime);
                }
                childEntities[i].transform.eulerAngles = Vector3.zero;
               childEntities[i].GetComponentInChildren<Anim_Cultist_States>().ChangeAnimState(Anim_Cultist_States.AnimCultistState.Run);
                Vector3 dirProjection = PlayerMoveAlone.playerPos - childEntities[i].transform.position;
           
                if(Vector3.SignedAngle(Vector3.forward, dirProjection.normalized, Vector3.up)!=0)
                {
                    float angle = Vector3.SignedAngle(Vector3.forward,dirProjection.normalized,Vector3.up);
                    childEntities[i].transform.eulerAngles =  new Vector3(0, angle,0);   
                }               
            }
        }
    }

    private void NewCircle()
    {
        radiusUse = radiusAtBase + (sizeBetweenCircle * currentCircleNumber);
        angleByCircle = 360 / numberByCircle;
        currentCircleNumber++;
    }
    private void ResetCircle()
    {

        currentCircleNumber = 0;
    }
    public void CastInvoq()
    {
        if (startInvoq != true)
        {
            fbCastInvoq.SetActive(true);
            tempsEcouleInvoq = 0;
            startInvoq = true;
        }

    }

    public void AnimRituel(Anim_Cultist_States.AnimCultistState animToChange)
    {
        for(int i = 0 ; i< childEntities.Length;i++)
        {
            if(childEntities[i].GetComponent<StateOfEntity>().entity == StateOfEntity.EntityState.Formation)
            {
                childEntities[i].GetComponentInChildren<Anim_Cultist_States>().animCultist =  animToChange;

            }
        }
    }
}
