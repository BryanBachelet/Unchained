using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EntitiesManager.BeheaviorCultiste startBehavior;
    public GameObject objectToInstantiate;
    public GameObject target;
    [Header("Temps de spawn")]
    public float timeOfSpawnPhase1 = 30 ;

    public float timeOfSpawnPhase2 = 20;
    [Header("Vitesse Agent")]
    public float speedOfAgentCultist = 10;

    public float speedOfAgentLaser = 6;

    public float speedOfAgentRitualist = 4;

    private float compteur;
 
    private void Start()
    {
        compteur = timeOfSpawnPhase1 - 7;
    }

    void Update()
    {
        
        if (StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase2)
        {
            if (compteur > timeOfSpawnPhase2)
            {
                bool hasSpawn = SpawnEntities();
                if(hasSpawn)
                {
                    compteur = 0;
                }
            }
            else
            {
                compteur += Time.deltaTime;
            }
        }
        if (StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase1)
        {
            if (compteur > timeOfSpawnPhase1)
            {
                bool hasSpawn = SpawnEntities();
                if(hasSpawn)
                {
                    compteur = 0;
                }
            }
            else
            {
                compteur += Time.deltaTime;
            }
        }
    }


    private bool SpawnEntities()
    {
       
        if(ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Patrole))
        {  
            GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            ManageEntity.nbEntity += 20;
            ManageEntity.nbEntityTotal += 20;
            EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();

            manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.Patrol;  
            manager.listOfPointOfPatrol = ManageEntity.ritualPoint;
            manager.speedOfMouvement = speedOfAgentCultist;
            return true;
            
        }
          if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Distance) )
        {
            GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            ManageEntity.nbEntity += 20;
            ManageEntity.nbEntityTotal += 20;
            EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();

            manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.Harass;
            manager.speedOfMouvement = speedOfAgentLaser;
            return true;
        }
        if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Cultiste))
        {
            GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            ManageEntity.nbEntity += 20;
            ManageEntity.nbEntityTotal += 20;
            EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();

            manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.RituelPoint;
            manager.pointToGo = target.transform;
            manager.speedOfMouvement = speedOfAgentRitualist;
            return true;
        }
     

        return false;
    }



}


