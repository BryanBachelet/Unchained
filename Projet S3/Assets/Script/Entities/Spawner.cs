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

    private float compteur = 13;
    private bool followPlayer;

    private void Start()
    {



    }
    void Update()
    {
        if(StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
            
        }
        else if (StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase2)
        {
            if (compteur > timeOfSpawn + 10)
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
        GameObject instantiate = Instantiate(objectToInstantiate, transform.position, transform.rotation);
        EntitiesManager manager = instantiate.GetComponent<EntitiesManager>();
   

        if (patrol)
        {
            manager.cultisteBehavior  = EntitiesManager.BeheaviorCultiste.Patrol;
            manager.listOfPointOfPatrol = listOfPoint;
        }
        else
        {
            manager.cultisteBehavior  = startBehavior;
            manager.pointToGo = target.transform;
            
        }

        manager.speedOfMouvement = speedOfAgent;
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


