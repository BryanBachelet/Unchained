using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colorful;

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

    public float stop;
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

    private MashingFeedback mashingFeedback;

    private ParticleSystem roche;

    private Threshold threshold;
    private Negative negative;

    private ComicBook comicBook;
    private ShadowsMidtonesHighlights shadows;
    private int frame;

    public float magnitudeShake;


    // Start is called before the first frame update
    void Start()
    {
        activeCinematicCam =false;
        mashing = PlayerMoveAlone.Player1.GetComponent<MashingTrans>();
        mashingFeedback = PlayerMoveAlone.Player1.GetComponent<MashingFeedback>();
        agentTransfo = PlayerMoveAlone.Player1.GetComponent<TransformationAgent>();
        threshold = GetComponent<Threshold>();
        negative = GetComponent<Negative>();
        comicBook = GetComponent<ComicBook>();
        shadows = GetComponent<ShadowsMidtonesHighlights>();
        durationTotal = timeOfPlan +timeOfPlan2;
    }

    // Update is called once per frame
    void Update()
    {
      //  activeCinematicCam = activeBehavior; 

          if(Input.GetKeyDown(KeyCode.C))
          {
              if(threshold.enabled == true)
              {
                  threshold.enabled =false;
              }else
              {
                    threshold.enabled =true;
              }
          }
           if(Input.GetKeyDown(KeyCode.X))
          {
              if(comicBook.enabled == true)
              {
                  comicBook.enabled =false;
              }else
              {
                    comicBook.enabled =true;
              }
          }
          if(Input.GetKeyDown(KeyCode.V))
          {
             if(negative.enabled == true)
              {
                  negative.enabled =false;
                  shadows.enabled = false;
              }else
              {     shadows.enabled =true;
                    negative.enabled =true;
              }
          }
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
                
                if(compteurTimePlan-stop>timeOfPlan)
                {
                    currentPlan =NamePlan.Plan2;
                    SetPosition(false);
                    compteurTimePlan = 0;
                    mashing.activeMash = true; 
                }
                if(compteurTimePlan<stop)
                {
                    frame++;
                    if(frame % 5==0)
                    {
                        float x = Random.Range(-1,1) * magnitudeShake;
                        float y = Random.Range(-1,1) * magnitudeShake;

                        transform.position += new Vector3(x,y,0); 
                        frame = 0;
                    }
                    compteurTimePlan +=Time.deltaTime;
                }else
                {
                    compteurTimePlan +=Time.deltaTime;
                    ratioTimePlan = (compteurTimePlan-stop) /timeOfPlan;
                    angleCompteur += speedAngle*Time.deltaTime * 1;
                }
              
                transform.LookAt(pointFocus.transform);

                break;

                case(NamePlan.Plan2) :

                currentDistance = Mathf.Lerp(startDistance,finishDistance,ratioTimePlan);
                currentDirection = Vector3.Lerp(startdirection, finishDirection,ratioTimePlan);
                posAdd = Quaternion.Euler(0,angleCompteur ,0) *((-currentDirection) * currentDistance);
                transform.position  = pointFocus.transform.position + posAdd;
                frame++;
                if(frame % 5==0)
                {
                    float x = Random.Range(-1,1) * magnitudeShake;
                    float y = Random.Range(-1,1) * magnitudeShake;

                    transform.position += new Vector3(x,y,0); 
                    frame = 0;
                }
                
                if(compteurTimePlan>timeOfPlan)
                {
                    currentPlan =NamePlan.Plan3;
                    // SetPosition(false);
                }
                compteurTimePlan +=Time.deltaTime;
                ratioTimePlan = compteurTimePlan /timeOfPlan;
                angleCompteur += speedAngle*Time.deltaTime * 1;

                transform.LookAt(pointFocus.transform);

                break;

            }


        }
        else
        {
            currentPlan = NamePlan.Plan1;
            startMouvement =false;
            compteurTimePlan =0;
            ratioTimePlan =0;
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
        activeCinematicCam  = active;
               
    }


}
