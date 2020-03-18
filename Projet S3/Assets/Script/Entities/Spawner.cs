using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToInstantiate;
    [Header("Caractéristique du spawner")]
    public float timeOfSpawn;
    public GameObject target;
    public bool patrolMode;
    public Transform[] listOfPoint;
    public float speedOfAgent;

    private float compteur = 13;





    private void Start()
    {



    }
    void Update()
    {
        if(StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {

        }
        else
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
        manager.patrolMode = patrol;
        if (patrol)
        {
            manager.listOfPointOfPatrol = listOfPoint;
        }
        else
        {
            manager.pointToGo = target.transform;

        }

        manager.speedOfMouvement = speedOfAgent;
    }



}


