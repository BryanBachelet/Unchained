using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public Transform pointToGo;
    [Header("Patrol")]
    public bool patrolMode;
    public Transform[] listOfPointOfPatrol = new Transform[0];
    public float distanceMinToGo = 1;
    [Header("Feature of mouvement")]
    public float speedOfMouvement = 4;


    private int indexOfPatrol = 0;
    void Start()
    {

    }


    void Update()
    {
        MovementOfManager(patrolMode);
    }


    public void MovementOfManager(bool patrol)
    {
        if (!patrol)
        {
            if (Vector3.Distance(transform.position, pointToGo.transform.position) > distanceMinToGo)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointToGo.transform.position, speedOfMouvement*Time.deltaTime);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, listOfPointOfPatrol[indexOfPatrol].position) > distanceMinToGo)
            {
                transform.position = Vector3.MoveTowards(transform.position, listOfPointOfPatrol[indexOfPatrol].position, speedOfMouvement*Time.deltaTime);

            }
            else
            {

                ChangeIndex();
            }
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
