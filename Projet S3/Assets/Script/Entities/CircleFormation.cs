using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFormation : MonoBehaviour
{
    public GameObject[] childEntities;
    public int numberByCircle;
    public float radiusAtBase;
    public float sizeBetweenCircle;


    private float radiusUse;
    private int currentCircleNumber;
    private float angleByCircle;
    // Start is called before the first frame update
    void Start()
    {
        childEntities = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {

            childEntities[i] = transform.GetChild(i).gameObject;

        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < childEntities.Length; i++)
        {
            if (i >= numberByCircle * currentCircleNumber)
            {
                NewCircle();
            }
            Vector3 pos = new Vector3(0, 0, 0);

            pos = transform.position + (Quaternion.Euler(0, angleByCircle * i, 0) * transform.forward * radiusUse);
            if (!childEntities[i].GetComponent<EnnemiDestroy>().isDestroying)
            {
                childEntities[i].transform.position = Vector3.Lerp(childEntities[i].transform.position, pos, Time.deltaTime);
                childEntities[i].transform.eulerAngles = Vector3.zero;
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
