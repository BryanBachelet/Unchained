using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCam : MonoBehaviour
{
    public enum NamePlan{ Plan1,Plan2,Plan3};
    public NamePlan currentPlan = NamePlan.Plan1;
    public static bool activeCinematicCam;
    public bool activeBehavior;

    public GameObject pointFocus;

    [Header("Plan 1")]
    public float timeOfPlan;

    public float numberofTurn;

    public Vector3 posFinish;

    [Header("Plan 2")]

    public float timeOfPlan2;

    public float numberofTurn2;

    public Vector3 posFinish2;

    [HideInInspector]
    public  float durationTotal;

    private float speedAngle;
    private float angleCompteur;
    private bool startMouvement;
    private float compteurTimePlan;
    private float ratioTimePlan;
    private float startDistance;
    private float finishDistance;
    private float currentDistance;
    private Vector3 startdirection;

    private Vector3 finishDirection;

    private Vector3 currentDirection;

    private TransformationAgent agentTransfo;

    private MashingTrans mashing;

    // Start is called before the first frame update
    void Start()
    {
        activeCinematicCam =false;
        mashing = pointFocus.GetComponent<MashingTrans>();
        agentTransfo = pointFocus.GetComponent<TransformationAgent>();
        durationTotal = timeOfPlan +timeOfPlan2;
    }

    // Update is called once per frame
    void Update()
    {
      //  activeCinematicCam = activeBehavior; 

        if(activeCinematicCam)
        {   
            pointFocus.transform.position= pointFocus.transform.position;
            Debug.DrawLine(transform.position,pointFocus.transform.position);
            if(!startMouvement)
            {
                SetPosition(true);
                startMouvement =true;
            }
            switch(currentPlan)
            {
                case(NamePlan.Plan1):
            
                currentDistance = Mathf.Lerp(startDistance,finishDistance,ratioTimePlan);
                currentDirection = Vector3.Lerp(startdirection, finishDirection,ratioTimePlan);
                Vector3 posAdd = Quaternion.Euler(0,angleCompteur ,0) *((-currentDirection) * currentDistance);
                transform.position  = pointFocus.transform.position + posAdd;
                
                if(compteurTimePlan>timeOfPlan)
                {
                    currentPlan =NamePlan.Plan2;
                    SetPosition(false);
                    compteurTimePlan = 0;
                }
                compteurTimePlan +=Time.deltaTime;
                ratioTimePlan = compteurTimePlan /timeOfPlan;
                angleCompteur += speedAngle*Time.deltaTime;
              
                transform.LookAt(pointFocus.transform);

                break;

                case(NamePlan.Plan2) :

                currentDistance = Mathf.Lerp(startDistance,finishDistance,ratioTimePlan);
                currentDirection = Vector3.Lerp(startdirection, finishDirection,ratioTimePlan);
                 posAdd = Quaternion.Euler(0,angleCompteur ,0) *((-currentDirection) * currentDistance);
                transform.position  = pointFocus.transform.position + posAdd;
                
                if(compteurTimePlan>timeOfPlan)
                {
                    currentPlan =NamePlan.Plan3;
                    // SetPosition(false);
                }
                compteurTimePlan +=Time.deltaTime;
                ratioTimePlan = compteurTimePlan /timeOfPlan;
                angleCompteur += speedAngle*Time.deltaTime;

                transform.LookAt(pointFocus.transform);

                break;

            }


        }
        else
        {
            currentPlan = NamePlan.Plan1;
            startMouvement =false;
            
        }
    }




    public void ChangePlan(NamePlan planName)
    {
         switch(currentPlan)
            {
                case(NamePlan.Plan2):
                

                break;
            }
    }

    public void SetPosition(bool plan1 )
    {
        if(plan1)
        {
            speedAngle = ((numberofTurn *360)/timeOfPlan) ; 
            startDistance = Vector3.Distance(pointFocus.transform.position,transform.position);
            finishDistance = Vector3.Distance(pointFocus.transform.position,pointFocus.transform.position + posFinish);
            currentDistance = startDistance;
            startdirection = (pointFocus.transform.position - transform.position).normalized;
            finishDirection = (pointFocus.transform.position - (pointFocus.transform.position + posFinish)).normalized;
            currentDirection =  startdirection;
           agentTransfo.startTranformationAnim(10);
        }
        else
        {
            mashing.ActiveMashing();
            speedAngle = ((numberofTurn2 *360)/timeOfPlan2) ; 
            startDistance = Vector3.Distance(pointFocus.transform.position,transform.position);
            finishDistance = Vector3.Distance(pointFocus.transform.position, pointFocus.transform.position + posFinish2);
            currentDistance = startDistance;
            startdirection = (pointFocus.transform.position - transform.position).normalized;
            finishDirection = (pointFocus.transform.position -(pointFocus.transform.position + posFinish2)).normalized;
            currentDirection =  startdirection;
            
        }
        compteurTimePlan =0;
    }


    public static void StartTransformation( bool active)
    {
        activeCinematicCam  =active;
               
    }


}
