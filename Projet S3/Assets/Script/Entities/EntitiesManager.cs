using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public enum BeheaviorCultiste { RituelPoint, Patrol, Harass }
    public BeheaviorCultiste cultisteBehavior = BeheaviorCultiste.RituelPoint;
    public Transform pointToGo;
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
    private bool runPlayer;
    private int countOfDeath;

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

        transform.position =  new Vector3(transform.position.x, 0, transform.position.y);
    }


    public void MovementOfManager(bool patrol)
    {

        switch (cultisteBehavior)
        {
            case (BeheaviorCultiste.RituelPoint):
                if (circle.attack >= 0)
                {
                    if (Vector3.Distance(transform.position, pointToGo.transform.position) > distanceMinToGo)
                    {
                        if (circle.activeRituel)
                        {
                            circle.activeRituel = false;
                        }
                        transform.position = Vector3.MoveTowards(transform.position, pointToGo.transform.position, speedOfMouvement * Time.deltaTime);

                    if(circle.activeRituel)
                    {  
                        circle.activeRituel = false;                        
                    }
                    transform.position = Vector3.MoveTowards(transform.position, pointToGo.transform.position, speedOfMouvement*Time.deltaTime);
                            circle.AnimRituel(Anim_Cultist_States.AnimCultistState.Run);
                    
                    }
                    else
                    {
                        if (!circle.activeRituel)
                        {

                            circle.AnimRituel(Anim_Cultist_States.AnimCultistState.Invocation_Idle);
                            circle.activeRituel = true;
                        }
                        circle.CastInvoq();
                    }
                }
                if (circle.attack == -1)
                {
                    if (circle.activeRituel)
                    {
                        circle.activeRituel = false;
                    }
                    if (!runPlayer)
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
                                if (countOfDeath > 10)
                                {
                                    runPlayer = true;
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

                if (Vector3.Distance(transform.position, listOfPointOfPatrol[indexOfPatrol].position) > distanceMinToGo)
                {
                    transform.position = Vector3.MoveTowards(transform.position, listOfPointOfPatrol[indexOfPatrol].position, speedOfMouvement * Time.deltaTime);
                }
                else
                {
                    ChangeIndex();
                }
                break;

        }
        if (!patrol)
        {

        }
        else
        {

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
