using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGestion : MonoBehaviour
{
   public Spawner[] listSpawner;
    public float radius;
    public float speedOfAgent;
    public float timerOfspaw;
    public GameObject regionParent;

    void Awake()
    {

        for (int i = 0; i < listSpawner.Length; i++)
        {
            listSpawner[i].radius = radius;
            listSpawner[i].speedOfAgent = speedOfAgent;
            listSpawner[i].timeOfSpawn = timerOfspaw;
            listSpawner[i].regionParent = regionParent;
        }
    }

 
}
