using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Zone : MonoBehaviour
{
    public GameObject objectToInstantiate;

    public GameObject parentToSpawn;
/*
    public GameObject parentToSpawn1;
    public GameObject parentToSpawn2;
    public GameObject parentToSpawn3;
    public GameObject parentToSpawn4;
    public GameObject parentToSpawn5;
    public GameObject parentToSpawn6;
    */
    public GameObject target;

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;
    public GameObject target6;

    public bool spawning = true;

    public int randomTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (spawning == true)
        {

        SpawnObject();

        randomTarget = Random.Range(1, 7);
        if (randomTarget == 1)
        {
            target = target1;
        }
        if (randomTarget == 2)
        {
            target = target2;
        }
        if (randomTarget == 3)
        {
            target = target3;
        }
        if (randomTarget == 4)
        {
            target = target4;
        }
        if (randomTarget == 5)
        {
            target = target5;
        }
        if (randomTarget == 6)
        {
            target = target6;
        }

            spawning = false;
        }
    }


    void SpawnObject()
    {
            GameObject add = Instantiate(objectToInstantiate, transform.position, transform.rotation, parentToSpawn.transform);
            add.GetComponent<EnnemiBehavior>().target = target;
    }
}
