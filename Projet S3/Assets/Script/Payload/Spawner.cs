using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float compteur;
    public GameObject objectToInstantiate;
    public GameObject parentToSpawn;
    public float timeOfSpawn;
    [Header("Caractéristique du spawner")]
    public GameObject target;

   
    void Update()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        if (compteur > timeOfSpawn)
        {
            GameObject add = Instantiate(objectToInstantiate, transform.position, transform.rotation, parentToSpawn.transform);
            add.GetComponent<EnnemiBehavior>().target = target;
            compteur = 0;
        }
        else
        {
            compteur += Time.deltaTime;
        }

    }
}
