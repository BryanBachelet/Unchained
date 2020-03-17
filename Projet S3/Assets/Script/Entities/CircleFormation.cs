using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFormation : MonoBehaviour
{
    public GameObject[] childEntities;
    public int numberByCircle;
    public float radiusAtBase;
    public float sizeBetweenCircle;
    public float speedAgent;

    private float radiusUse;
    private int currentCircleNumber;
    private float angleByCircle;
    private float[] compteurOfMouvement;
    public float positionMouvement;
    void Start()
    {
        childEntities = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {

            childEntities[i] = transform.GetChild(i).gameObject;

        }
        compteurOfMouvement = new float[childEntities.Length];
    }

    // Update is called once per frame
    void Update()
    {
        Formation();

    }


    private void Formation()
    {
        for (int i = 0; i < childEntities.Length; i++)
        {
            if (i >= numberByCircle * currentCircleNumber)
            {
                NewCircle();
            }
            Vector3 pos = new Vector3(0, 0, 0);

            pos = transform.position + (Quaternion.Euler(0, angleByCircle * i, 0) * transform.forward * radiusUse);
            if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
            {
                float distanceDestination = Vector3.Distance(childEntities[i].transform.position, pos);
                if (distanceDestination > 0.01f)
                {
                    Vector3 dir = pos - childEntities[i].transform.position;
                    if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy)
                    {
                        childEntities[i].transform.position += dir.normalized * speedAgent * Time.deltaTime;
                        childEntities[i].transform.eulerAngles = Vector3.zero;
                    }
                }
            }
        }

        ResetCircle();
    }

    private void NewCircle()
    {
        radiusUse = radiusAtBase + (sizeBetweenCircle * currentCircleNumber);
        angleByCircle = 360 / numberByCircle;
        currentCircleNumber++;
    }
    private void ResetCircle()
    {

        currentCircleNumber = 0;
    }


}
