﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float compteur = 13;
    public GameObject objectToInstantiate;
    public GameObject parentToSpawn;
    public float timeOfSpawn;
    [Header("Caractéristique du spawner")]
    public GameObject target;
    public float radius;

    [Range(1, 10)]
    public int nbrEntiteeToSpawn;
   
    void Update()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        if (compteur > timeOfSpawn)
        {
            for(int i = 0; i < nbrEntiteeToSpawn; i++)
            {
                Vector2 posToSpawn = Random.insideUnitCircle * radius;
                GameObject add = Instantiate(objectToInstantiate, new Vector3(transform.position.x + posToSpawn.x, 1, transform.position.z + posToSpawn.y), transform.rotation, parentToSpawn.transform);
                add.GetComponent<EnnemiBehavior>().target = target;
                compteur = 0;
            }

        }
        else
        {
            compteur += Time.deltaTime;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
