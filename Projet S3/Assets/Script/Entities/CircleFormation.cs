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

    private Vector3[] posAlea;

    public bool activeCircle;

    public float fxInvocationHeight;

    public float decalageDegree= 20;

    [FMODUnity.EventRef]
    public string invoqSound;

    public Material laser;

    public Material other;

    void Start()
    {
        childEntities = new GameObject[transform.parent.childCount - 5];
        entityManage = GetComponent<EntitiesManager>();
        for (int i = 0; i < childEntities.Length; i++)
        {

            childEntities[i] = transform.parent.GetChild(i).gameObject;
            if(entityManage.cultisteBehavior == EntitiesManager.BeheaviorCultiste.Harass)
            {
                childEntities[i].GetComponentInChildren<SkinnedMeshRenderer>().material = laser;
            }else
            {
                childEntities[i].GetComponentInChildren<SkinnedMeshRenderer>().material = other;
            }
        }
        compteurOfMouvement = new float[childEntities.Length];
        posAlea = new Vector3[childEntities.Length];
        float rangeAdd = 0;
        for(int i = 0 ; i<posAlea.Length;i++)
        {
            
            if(i%10 == 0)
            {
                rangeAdd +=2.5f;
            }
            Vector3 spherePos = new Vector3 (Random.insideUnitCircle.x,0, Random.insideUnitCircle.y).normalized * Random.Range(1 +rangeAdd, 3 +rangeAdd);
            posAlea[i]= spherePos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( StateOfGames.currentState != StateOfGames.StateOfGame.Transformation)
        {
            Formation();
        }else
        {
            for (int i = 0; i < childEntities.Length ; i++)
            {
                childEntities[i].transform.eulerAngles = new Vector3(0,0,0);
            }          
            
        }
        if(entityManage.autoDestruct && StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {
            ActiveAutoDestruct();
        }
        

        if (startInvoq && activeRituel)
        {
          
            tempsEcouleInvoq += Time.deltaTime;
            if(fbCastInvoq.transform.localScale.x < 5)
            {
                fbCastInvoq.transform.position = transform.position + Vector3.up * fxInvocationHeight ;
                fbCastInvoq.transform.localScale = new Vector3(1 + tempsEcouleInvoq, 1 + tempsEcouleInvoq, 1 + tempsEcouleInvoq);
            }

            if (tempsEcouleInvoq > timeForInvoq)
            {
                
                if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Coloss))
                {
                    Instantiate(invoq1, fbCastInvoq.transform.position, fbCastInvoq.transform.rotation);
                    FMODUnity.RuntimeManager.PlayOneShot(invoqSound);
                    Debug.Log("Invoque Coloss");
                    tempsEcouleInvoq = 0;
                }
                else
                {
                    tempsEcouleInvoq = 0;
                }
            }



        }
        if(!activeRituel)
        {
            startInvoq =false;
            tempsEcouleInvoq = 0;
            fbCastInvoq.transform.position = transform.position;
            if(fbCastInvoq.transform.localScale.x > 0.1f && fbCastInvoq.activeInHierarchy)
            {
                fbCastInvoq.transform.position = new Vector3 (entityManage.pointToGo.transform.position.x ,fbCastInvoq.transform.position.y, entityManage.pointToGo.transform.position.z);
                fbCastInvoq.transform.localScale -= Vector3.one  *10*Time.deltaTime;  
            }
            else
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

    public void ActiveAutoDestruct()
    {
        for(int i = 0 ; i < childEntities.Length; i++)
        {   
                if (childEntities[i].GetComponent<StateOfEntity>() && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead &&  childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch )
                {
                    Vector3 testPosCamView = Camera.main.WorldToScreenPoint(childEntities[i].transform.position);
                    if(testPosCamView.x > 0 || testPosCamView.x < 1920 || testPosCamView.y < 0 || testPosCamView.y > 1080)
                    {
                        Debug.Log("Death");
                        childEntities[i].SetActive(false);
                        childEntities[i].GetComponent<StateOfEntity>().entity =  StateOfEntity.EntityState.Dead;
                    }

                }
        }
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


            Destroy(gameObject.transform.parent.gameObject);


        }
    }


    private void Formation()
    { 
        int numFor = 0;
       
          int vertical = 0;
          int horizontal = 0;
        for (int i = 0; i < childEntities.Length ; i++)
        {
            if (i >= numberByCircle * currentCircleNumber)
            {
                NewCircle();
            }
            
            if (activeCircle)
            {
                Vector3 pos = new Vector3(0, 0, 0);
                Vector3 transformFor = transform.forward;
               // pos = transform.position + (Quaternion.Euler(0, (angle) + (angleByCircle * i), 0)*  transform.forward * radiusUse);
                pos =  NewFormation(i,numberByCircle);
                if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead && childEntities[i].GetComponent<StateOfEntity>())
                {
                    
                    float distanceDestination = Vector3.Distance(childEntities[i].transform.position, pos); 
                    if (distanceDestination > 0.01f)
                    {
                        Vector3 dir = pos - childEntities[i].transform.position;
                        if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch)
                        {

                                
                            if (distanceDestination > 20)
                            {
                                Vector3 testPosCamView = Camera.main.WorldToScreenPoint(childEntities[i].transform.position);
                                if(testPosCamView.x > 0 || testPosCamView.x < 1920 || testPosCamView.y < 0 || testPosCamView.y > 1080)
                                {
                                    childEntities[i].transform.position += (dir.normalized * speedAgent * 4 * Time.deltaTime);
                                }
                                 if( StateOfGames.currentState != StateOfGames.StateOfGame.Transformation)
                                {
                                    childEntities[i].transform.position += (dir.normalized * speedAgent * Time.deltaTime);
                                }
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
                                if( StateOfGames.currentState != StateOfGames.StateOfGame.Transformation)
                                {       
                                    childEntities[i].transform.position = Vector3.Lerp(childEntities[i].transform.position, pos,Time.deltaTime);
                                }
                                    childEntities[i].transform.eulerAngles = Vector3.zero;
                                    childEntities[i].GetComponent<StateOfEntity>().entity = StateOfEntity.EntityState.Formation;

                                Vector3 dirProjection = entityManage.GetPointToGo() - childEntities[i].transform.position;
                
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
            else
            {
                Vector3 pos = new Vector3(0, 0, 0);
                Vector3 transformFor = transform.forward;
              
                if(i%5 == 0)
                {
                    vertical += 5;
                    horizontal = 0;
                }else
                {
                    horizontal +=3; 
                }
                pos = transform.position + posAlea[i] ;
                if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch)
                {
                    Vector3 dir = pos - childEntities[i].transform.position;
                    Vector3 testPosCamView = Camera.main.WorldToScreenPoint(childEntities[i].transform.position);
                    if(testPosCamView.x > 0 || testPosCamView.x < 1920 || testPosCamView.y < 0 || testPosCamView.y > 1080)
                    {
                      childEntities[i].transform.position = Vector3.Lerp(childEntities[i].transform.position, pos,Time.deltaTime);            
                        //childEntities[i].transform.position += (dir.normalized * speedAgent * 4 * Time.deltaTime);
                    }else
                    {
                        Debug.Log("Ok");
                        childEntities[i].transform.position = Vector3.Lerp(childEntities[i].transform.position, pos,Time.deltaTime);    
                    }
                 
                    childEntities[i].transform.eulerAngles = Vector3.zero;
                  
                    Vector3 orientationDir =  entityManage.GetPointToGo() - childEntities[i].transform.position;
                    if(Vector3.SignedAngle(Vector3.forward, orientationDir.normalized, Vector3.up)!=0)
                    {
                        float angle = Vector3.SignedAngle(Vector3.forward,orientationDir.normalized,Vector3.up);
                        childEntities[i].transform.eulerAngles =  new Vector3(0, angle,0);
                    }
                    float distanceDestination = Vector3.Distance(childEntities[i].transform.position, pos); 
                    if (distanceDestination < 6)
                    {

                        numFor++;
                    }
                }               


            }
        }

        if(numFor>5)
        {
            attack = 1;
        }
        if(numFor<5 && attack != 0) 
        {
            attack =-1;
        }
        ResetCircle();
    }

    public Vector3 NewFormation(int currentEntities, int divisionLine)
    {   
       int addPerLine = currentEntities/divisionLine;
       Vector3 post =  transform.position + (Quaternion.Euler(0, (angle) + (angleByCircle * currentEntities) + (decalageDegree*addPerLine), 0)*  transform.forward * (radiusUse+(addPerLine*sizeBetweenCircle)));

        return post;
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
      public void StopAnimRituel()
    {
        for(int i = 0 ; i< childEntities.Length;i++)
        {
            if(childEntities[i].GetComponent<StateOfEntity>().entity == StateOfEntity.EntityState.Formation)
            {
                childEntities[i].GetComponentInChildren<Anim_Cultist_States>().StopAnim();

            }
        }
    }
      public void StartAnimRituel()
    {
        for(int i = 0 ; i< childEntities.Length;i++)
        {
            if(childEntities[i].GetComponent<StateOfEntity>().entity == StateOfEntity.EntityState.Formation)
            {
                childEntities[i].GetComponentInChildren<Anim_Cultist_States>().StartAnim();

            }
        }
    }
}
