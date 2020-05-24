using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public enum BeheaviorCultiste { RituelPoint, Patrol, Harass }
    public BeheaviorCultiste cultisteBehavior = BeheaviorCultiste.RituelPoint;
    public Transform pointToGo;

    public float distanceActiveRituel =1;

    public float distanceChangeFormation = 2;  

    [Header("Patrol")]
    public bool patrolMode;
    public Transform[] listOfPointOfPatrol = new Transform[0];
    public float distanceMinToGo = 1;
    [Header("Feature of mouvement")]
    public float speedOfMouvement = 4;

    private CircleFormation circle;
    private CultistLaser cultistLaser;

    private int indexOfPatrol = 0;
    private int indexCircleEntities;
    public bool autoDestruct;
    private int countOfDeath;

    public int autoDestruction = 15;

    public float circleDistance = 20;
    private bool transformation;

    private Vector3 dirTrans;


    void Start()
    {
        circle = GetComponent<CircleFormation>();
        cultistLaser = GetComponent<CultistLaser>();
    }


    void Update()
    {
        if (StateOfGames.currentPhase != StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
            MovementOfManager(patrolMode);
        }else
        {
            cultistLaser.enabled = true;
            circle.activeRituel = false;
        }

        transform.position =  new Vector3(transform.position.x, 0, transform.position.z);
    }


    public void MovementOfManager(bool patrol)
    {

    if(StateOfGames.currentState !=  StateOfGames.StateOfGame.Transformation)
    {
        switch (cultisteBehavior)
        {
            case (BeheaviorCultiste.RituelPoint):
               
                   transformation =false;

                if (circle.attack >= 0)
                {
                    if (Vector3.Distance(transform.position, pointToGo.transform.position) > distanceActiveRituel)
                    {
                        if (circle.activeRituel)
                        {
                            circle.activeRituel = false;
                            circle.activeCircle =false;
                        }
                        transform.position = Vector3.MoveTowards(transform.position, pointToGo.transform.position, speedOfMouvement * Time.deltaTime);

                        if(circle.activeRituel)
                        {     circle.activeCircle =false;
                            circle.activeRituel = false;                        
                        }
                        transform.position = Vector3.MoveTowards(transform.position, pointToGo.transform.position, speedOfMouvement*Time.deltaTime);
                        circle.AnimRituel(Anim_Cultist_States.AnimCultistState.Run);
                    
                    }
                    else
                    {
                        circle.CastInvoq();
                    }
                    if (Vector3.Distance(transform.position, pointToGo.transform.position) < distanceChangeFormation)
                    {
                        if (!circle.activeRituel)
                        {

                            circle.AnimRituel(Anim_Cultist_States.AnimCultistState.Invocation_Idle);
                            circle.activeRituel = true;
                            circle.activeCircle =true;
                        }
                    }
                }
                if (circle.attack == -1)
                {
                    if (circle.activeRituel)
                    {
                        circle.activeRituel = false;
                         circle.activeCircle =false;
                    }
                    if (!autoDestruct)
                    {

                        if (Vector3.Distance(transform.position, circle.childEntities[indexCircleEntities].transform.position) > 20 &&
                        circle.childEntities[indexCircleEntities].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
                        {
                            Debug.DrawLine(transform.position, circle.childEntities[indexCircleEntities].transform.position, Color.green);
                            Vector3 posToGoEntity = new Vector3(circle.childEntities[indexCircleEntities].transform.position.x, 1, circle.childEntities[indexCircleEntities].transform.position.z);
                            transform.position = Vector3.MoveTowards(transform.position, circle.childEntities[indexCircleEntities].transform.position, speedOfMouvement * Time.deltaTime);
                        }
                        else
                        {
                            countOfDeath = 0;
                            for (int i = 0; i < circle.childEntities.Length; i++)
                            {
                                if (circle.childEntities[i].GetComponent<StateOfEntity>() == null)
                                {
                                    Debug.Log(circle.childEntities[i]);
                                    Debug.Break();
                                }
                                if (circle.childEntities[i].GetComponent<StateOfEntity>().entity == StateOfEntity.EntityState.Dead)
                                {
                                    countOfDeath++;
                                }
                                if (countOfDeath > autoDestruction)
                                {
                                    autoDestruct = true;
                                    break;
                                }
                            }

                            if (indexCircleEntities < circle.childEntities.Length - 1)
                            {
                                indexCircleEntities++;
                            }
                            else
                            {
                                indexCircleEntities = 0;
                            }
                        }
                    }
                    else
                    {
                        circle.ActiveRunPlayer();
                    }
                    }
                
                

                break;
            case (BeheaviorCultiste.Harass):

               
                cultistLaser.enabled = true;


                break;
            case (BeheaviorCultiste.Patrol):

                Vector3 currentPoint = new Vector3 (listOfPointOfPatrol[indexOfPatrol].position.x, 0 ,listOfPointOfPatrol[indexOfPatrol].position.z);
                if (Vector3.Distance(transform.position, currentPoint) > distanceMinToGo)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentPoint, speedOfMouvement * Time.deltaTime);
                }
                else
                {
                    ChangeIndex();
                }
                break;
            }      

        }
        else{
                if(!transformation)
                {   
                    if(CheckDistance.activeCenter)
                    {
                      dirTrans  = PlayerMoveAlone.playerPos-  transform.position;
                      dirTrans = dirTrans.normalized*circleDistance;
                    }else
                    {
                        dirTrans = CheckDistance.dir;
                        dirTrans = Quaternion.Euler(0,Random.Range(-30,30),0)* dirTrans;
                        dirTrans = dirTrans.normalized *circleDistance;
                    }
                    transformation =true;

                }
                if(Vector3.Distance(transform.position,PlayerMoveAlone.playerPos)<circleDistance)
                {
                    circle.activeRituel = false;
                    circle.AnimRituel(Anim_Cultist_States.AnimCultistState.Run);
                    transform.position = Vector3.MoveTowards(transform.position, dirTrans, speedOfMouvement *5 * Time.deltaTime);
                }
            }
     
    }

    public Vector3 GetPointToGo()
    {
        if(cultisteBehavior == BeheaviorCultiste.RituelPoint)
        {
            return pointToGo.position;
        }
        else if(cultisteBehavior == BeheaviorCultiste.Patrol)
        {
            return listOfPointOfPatrol[indexOfPatrol].position;
        }
        else
        {
            return cultistLaser.moveTo;
        }

    }

    private void ChangeIndex()
    {
        if (indexOfPatrol < listOfPointOfPatrol.Length - 1)
        {
            indexOfPatrol++;
        }
        else
        {
            indexOfPatrol = 0;
        }

    }




}
