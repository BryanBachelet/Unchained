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
    public float rotateRituelSpeed =50;
    public bool activeRituel;

    private float radiusUse;
    private int currentCircleNumber;
    private float angleByCircle;
    private bool doDestruct;
    private float[] compteurOfMouvement;
    
    private float angle;

    public bool attack;
    
    void Start()
    {
        childEntities = new GameObject[transform.childCount-2];
        for (int i = 0; i < transform.childCount-2; i++)
        {

            childEntities[i] = transform.GetChild(i).gameObject;

        }
        compteurOfMouvement = new float[childEntities.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
       if( StateOfGames.currentPhase != StateOfGames.PhaseOfDefaultPlayable.Phase3){
        Formation();
       }else
       {
           RunPlayer();
       }
        Destruct();
    }


    private void Destruct()
    {
        doDestruct = true;
        for (int i = 0; i < childEntities.Length; i++)
        {
            if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
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
        int numFor = 0;
        if(activeRituel)
        {     
            angle += rotateRituelSpeed*Time.deltaTime;
        }
        for (int i = 0; i < childEntities.Length; i++)
        {
            if (i >= numberByCircle * currentCircleNumber)
            {
                NewCircle();
            }
            Vector3 pos = new Vector3(0, 0, 0);
            Vector3 transformFor = transform.forward;
            pos = transform.position + (Quaternion.Euler(0, (angle) + (angleByCircle * i), 0) * transform.forward* radiusUse);
            if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
            {
                float distanceDestination = Vector3.Distance(childEntities[i].transform.position, pos);
                Debug.DrawLine(pos, childEntities[i].transform.position, Color.blue);
                if (distanceDestination > 0.01f)
                {
                    Vector3 dir = pos - childEntities[i].transform.position;
                    if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch )
                    {
                        /*if(activeRituel)
                        {
                            childEntities[i].transform.position += Quaternion.Euler(0,angle,0)*childEntities[i].transform.forward;
                            Quaternion.Euler(0,angle,0)*(dir.normalized* Vector3.Distance(childEntities[i].transform.position , transform.position))
                        }*/

                        

                         
                        if (distanceDestination > 1f)
                        {
                            childEntities[i].transform.position +=  (dir.normalized *speedAgent * Time.deltaTime);
                            childEntities[i].transform.eulerAngles = Vector3.zero;
                            childEntities[i].GetComponent<StateOfEntity>().entity = StateOfEntity.EntityState.ReturnFormation;
                        }
                        else
                        {
                            
                            childEntities[i].transform.position = Vector3.Lerp(childEntities[i].transform.position, pos,20*Time.deltaTime);
                            childEntities[i].transform.eulerAngles = Vector3.zero;
                            childEntities[i].GetComponent<StateOfEntity>().entity = StateOfEntity.EntityState.Formation;
                            numFor++;
                        }
                    }


                }
            }
        }

        if(numFor>10)
        {
            attack = true;
        }
        else
        {
            attack =false;
        }
        ResetCircle();
    }

   public void RunPlayer()
   {
         for (int i = 0; i < childEntities.Length; i++)
        { if (childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch && childEntities[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead)
                    {
                    childEntities[i].transform.position = Vector3.MoveTowards(childEntities[i].transform.position , PlayerMoveAlone.Player1.transform.position, 2*speedAgent*Time.deltaTime);
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


}
