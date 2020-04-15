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
    public float positionMouvement;
    public float rotateRituelSpeed = 50;
    public bool activeRituel;
    public float timeForInvoq;
    public bool startInvoq = false;
    public float tempsEcouleInvoq;

    private float radiusUse;
    private int currentCircleNumber;
    private float angleByCircle;
    private bool doDestruct;
    private float[] compteurOfMouvement;

    private float angle;

    public GameObject invoq1;
    public GameObject fbCastInvoq;
    void Start()
    {
        childEntities = new GameObject[transform.childCount - 2];
        for (int i = 0; i < transform.childCount - 2; i++)
        {

            childEntities[i] = transform.GetChild(i).gameObject;

        }
        compteurOfMouvement = new float[childEntities.Length];
    }

    // Update is called once per frame
    void Update()
    {

        if (StateOfGames.currentPhase != StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
            Formation();
        }
        else
        {
            RunPlayer();
        }
        if (startInvoq && activeRituel)
        {

            tempsEcouleInvoq += Time.deltaTime;
            if(fbCastInvoq.transform.localScale.x < 5)
            {
                fbCastInvoq.transform.localScale = new Vector3(1 + tempsEcouleInvoq, 1 + tempsEcouleInvoq, 1 + tempsEcouleInvoq);
            }

            if (tempsEcouleInvoq > timeForInvoq)
            {
                Debug.Log("INVOQ NOW");
                if (ManageEntity.CheckInstantiateInvoq(ManageEntity.EntityType.Coloss))
                {
                    Instantiate(invoq1, fbCastInvoq.transform.position, fbCastInvoq.transform.rotation);
                    tempsEcouleInvoq = 0;
                }
            }



        }
        Destruct();
    }


    private void Destruct()
    {
        doDestruct = true;
        for (int i = 0; i < childEntities.Length; i++)
        {
            if (childEntities[i].GetComponent<StateOfEntity>() && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
            {
                doDestruct = false;
                break;
            }

        }
        if (doDestruct)
        {

            Destroy(gameObject);
        }
    }


    private void Formation()
    {
        if (activeRituel)
        {
            angle += rotateRituelSpeed * Time.deltaTime;
        }
        for (int i = 0; i < childEntities.Length - 1; i++)
        {
            if (i >= numberByCircle * currentCircleNumber)
            {
                NewCircle();
            }
            Vector3 pos = new Vector3(0, 0, 0);
            Vector3 transformFor = transform.forward;
            pos = transform.position + (Quaternion.Euler(0, (angle) + (angleByCircle * i), 0) * transform.forward * radiusUse);
            if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead && childEntities[i].GetComponent<StateOfEntity>())
            {
                float distanceDestination = Vector3.Distance(childEntities[i].transform.position, pos);
                Debug.DrawLine(pos, childEntities[i].transform.position, Color.blue);
                if (distanceDestination > 0.01f)
                {
                    Vector3 dir = pos - childEntities[i].transform.position;
                    if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch)
                    {
                        /*if(activeRituel)
                        {
                            childEntities[i].transform.position += Quaternion.Euler(0,angle,0)*childEntities[i].transform.forward;
                            Quaternion.Euler(0,angle,0)*(dir.normalized* Vector3.Distance(childEntities[i].transform.position , transform.position))
                        }*/
                        if (distanceDestination > 1f)
                        {
                            childEntities[i].transform.position += (dir.normalized * speedAgent * Time.deltaTime);
                            childEntities[i].transform.eulerAngles = Vector3.zero;
                        }
                        else
                        {

                            childEntities[i].transform.position = Vector3.Lerp(childEntities[i].transform.position, pos, 10 * Time.deltaTime);
                            childEntities[i].transform.eulerAngles = Vector3.zero;
                        }
                    }


                }
            }
        }

        ResetCircle();
    }

    public void RunPlayer()
    {
        for (int i = 0; i < childEntities.Length; i++)
        {
            if (childEntities[i].GetComponent<StateOfEntity>() && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
            {
                childEntities[i].transform.position = Vector3.MoveTowards(childEntities[i].transform.position, PlayerMoveAlone.Player1.transform.position, 2 * speedAgent * Time.deltaTime);
                childEntities[i].transform.eulerAngles = Vector3.zero;
            }
        }
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
    public void CastInvoq()
    {
        if (startInvoq != true)
        {
            fbCastInvoq.SetActive(true);
            tempsEcouleInvoq = 0;
            startInvoq = true;
        }

    }
}
