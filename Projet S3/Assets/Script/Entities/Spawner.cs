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

    private float compteSpawn;
    private float conditionSpawn;
 
    private void Start()
    {
        compteur = timeOfSpawnPhase1 - 7;
    }

    void Update()
    {
        if (StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
            if (compteur > timeOfSpawnPhase2)
            {
                if(ManageEntity.CheckNumber())
                {
                    bool hasSpawn = SpawnEntities();
                    if(hasSpawn)
                    {
                        compteur = 0;
                    }
                }
            }
            else
            {
                compteur += Time.deltaTime;
            }
        }
        
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
            
                compteSpawn++;
                GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
                ManageEntity.nbEntity += 20;
                ManageEntity.nbEntityTotal += 20;
                EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();

                manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.Patrol;  
                manager.listOfPointOfPatrol = ManageEntity.ritualPoint;
                manager.speedOfMouvement = speedOfAgentCultist;
                if(StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
                {
                    ManageEntity.CompteurPhase3();
                }
                return true;
            
            
        }
     
        if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Cultiste))
        {
            compteSpawn++;
            GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            ManageEntity.nbEntity += 20;
            ManageEntity.nbEntityTotal += 20;
            EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();

            manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.RituelPoint;
            manager.pointToGo = target.transform;
            manager.speedOfMouvement = speedOfAgentRitualist;
            return true;
        }
        
           if(compteSpawn>conditionSpawn)
        {
            if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Distance) )
            {
                compteSpawn++;
                GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
                ManageEntity.nbEntity += 20;
                ManageEntity.nbEntityTotal += 20;
                EntitiesManager manager = instantiate.GetComponentInChildren<EntitiesManager>();

                manager.cultisteBehavior = EntitiesManager.BeheaviorCultiste.Harass;
                manager.speedOfMouvement = speedOfAgentLaser;
                return true;
            }
        }
     

        return false;
    }



}


