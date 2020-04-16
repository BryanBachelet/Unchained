using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public enum BeheaviorCultiste{ RituelPoint,Patrol,Harass}
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
    void Start()
    {
        circle = GetComponent<CircleFormation>();
        cultistLaser = GetComponent<CultistLaser>();
    }


    void Update()
    {
        if(StateOfGames.currentPhase != StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
            MovementOfManager(patrolMode);
        }
    }


    public void MovementOfManager(bool patrol)
    {

        switch(cultisteBehavior) {
        case(BeheaviorCultiste.RituelPoint):

            if (Vector3.Distance(transform.position, pointToGo.transform.position) > distanceMinToGo)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointToGo.transform.position, speedOfMouvement*Time.deltaTime);
            }else
            {
                if(!circle.activeRituel)
                {
               
                circle.activeRituel = true;
                }
                circle.CastInvoq();
            }
        break;
        case(BeheaviorCultiste.Harass):
        
            
        cultistLaser.enabled = true;


        break;
        case(BeheaviorCultiste.Patrol) :
        
            if (Vector3.Distance(transform.position, listOfPointOfPatrol[indexOfPatrol].position) > distanceMinToGo)
            {
                transform.position = Vector3.MoveTowards(transform.position, listOfPointOfPatrol[indexOfPatrol].position, speedOfMouvement*Time.deltaTime);
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
