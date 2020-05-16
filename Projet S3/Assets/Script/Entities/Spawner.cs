using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EntitiesManager.BeheaviorCultiste startBehavior;
    public GameObject objectToInstantiate;
    [Header("Caractéristique du spawner")]
    public float timeOfSpawn;
    public GameObject target;
    public bool patrolMode;
    public Transform[] listOfPoint;
    public float speedOfAgent;

    private float compteur;
    private bool followPlayer;

    private void Start()
    {
        compteur = timeOfSpawn - 7;


    }
    void Update()
    {
        if(StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
            
        }
        else if (StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase2)
        {
            if (compteur > timeOfSpawn - 10)
            {
                SpawnEntities(patrolMode);
                compteur = 0;
            }
            else
            {
                compteur += Time.deltaTime;
            }
        }
        else if (StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase1)
        {
            if (compteur > timeOfSpawn)
            {
                SpawnEntities(patrolMode);
                compteur = 0;
            }
            else
            {
                compteur += Time.deltaTime;
            }
        }
    }


    private void SpawnEntities(bool patrol)
    {
        bool checkCultiste = false;
        bool checkPatrol = false;
        if(ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Patrole))
        {
            GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            ManageEntity.nbEntity += 20;
            ManageEntity.nbEntityTotal += 20;
            EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();
            manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.Patrol;
            manager.pointToGo = target.transform;
            manager.listOfPointOfPatrol = ManageEntity.ritualPoint;
            manager.speedOfMouvement = speedOfAgent;
            checkPatrol = true;
        }
        if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Cultiste) && checkPatrol == false)
        {

            GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            ManageEntity.nbEntity += 20;
            ManageEntity.nbEntityTotal += 20;
            EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();
            manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.RituelPoint;
            manager.pointToGo = target.transform;
            manager.speedOfMouvement = speedOfAgent;
            checkCultiste = true;
        }
       if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Distance) && checkCultiste == false)
        {
            GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            ManageEntity.nbEntity += 20;
            ManageEntity.nbEntityTotal += 20;
            EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();
          
            manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.Harass;
            manager.pointToGo = target.transform;
            manager.speedOfMouvement = speedOfAgent;
        }
        else
        {
           // manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.Patrol;
        }

        



        // if (patrol)
        // {
        //     if()
        //     manager.cultisteBehavior  = EntitiesManager.BeheaviorCultiste.Patrol;
        //     manager.listOfPointOfPatrol = listOfPoint;
        // }
        // else
        // {
        //     manager.cultisteBehavior  = startBehavior;
        //     manager.pointToGo = target.transform;
        //     
        // }

        

    }



private bool CheckInterestPointFree(GameObject target)
{
    CenterTag tagCenter = target.GetComponent<CenterTag>();
    if(!tagCenter.isInvoking)
    {
        return false;
    }
    else
    {
        return true;
    }
}

}


