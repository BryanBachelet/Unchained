using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouvement : MonoBehaviour
{

    public bool smoothTransition = false;
    public List<CamBehavior[]> camTab = new List<CamBehavior[]>(0);

    public Dictionary<int, CamBehavior[]> camTan = new Dictionary<int, CamBehavior[]>(3);

    public List<CamBehavior> cams = new List<CamBehavior>(0);
    [Space]
    public int i;
    private float compteurStart;
    private float compteurDep;
    private bool startMouvement;

    private float rotatePast;
    private float dist;
    private Vector3 dir;
    private float angleSpeed;

    private CameraAction cameraAc;
    public bool posStart;
    private Vector3 startPosCam;
    private Vector3 startEulerCam;
    // Start is called before the first frame update
    void Start()
    {
        if (!smoothTransition)
        {
            transform.position = cams[i].startPos.position;
            transform.eulerAngles = cams[i].startPos.eulerAngles;
            transform.LookAt(cams[i].pointOfPivot);
        }
        else
        {
          
            startPosCam = transform.position;
            startEulerCam = transform.eulerAngles;
        }
           // cameraAc = GetComponent<CameraAction>();
          
           
    }

    // Update is called once per frame
    void Update()
    {
        if (cams[i].type == CamBehavior.TypeMovement.Translation)
        {
            Translation();

        }
        if (cams[i].type == CamBehavior.TypeMovement.Rotation)
        {
            Rotation();

        }
    }



    public void Rotation()
    {
        if (compteurStart > cams[i].timeBeforeStart)
        {
            if (!startMouvement)
            {
                dist = (cams[i].pointOfPivot.position - transform.position).magnitude;
                dir = (transform.position - cams[i].pointOfPivot.position).normalized;

            }
            startMouvement = true;

        }
        else compteurStart += Time.deltaTime;



        if (startMouvement)
        {
            float distance = Vector3.Distance(transform.position, cams[i].pointOfPivot.position);

            Vector3 nextPos = cams[i].pointOfPivot.position + Quaternion.Euler(0, rotatePast, 0) * dir * dist;
            transform.position = Vector3.Lerp(transform.position, nextPos, 1);
            transform.LookAt(cams[i].pointOfPivot);
            rotatePast += angleSpeed * Time.deltaTime;
            angleSpeed = cams[i].angleToRotate / cams[i].timeOfDeplacement;


        }
        if (rotatePast >= cams[i].angleToRotate)
        {
            if (i >= (cams.Count - 1))
            {
                StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;
                //cameraAc.enabled = true;
                this.enabled = false;
            }
            if (i < (cams.Count - 1))
            {
                i++;
            }
            startMouvement = false;
            compteurDep = 0;
            compteurStart = 0;
            rotatePast = 0;
            angleSpeed = 0;
            dir = Vector3.zero;
            dist = 0;

        }
    }

    public void Translation()
    {
        if (smoothTransition == true && posStart == false)
        {

            float compt = compteurDep / cams[i].timeOfDeplacement;
            transform.position = Vector3.Lerp(startPosCam,cams[i].startPos.position, compt);
            transform.eulerAngles = Vector3.Lerp(startEulerCam,cams[i].startPos.eulerAngles, compt);
            transform.LookAt(cams[i].pointOfPivot);
            compteurDep += Time.deltaTime;
            if (transform.position == cams[i].startPos.position)
            {
                
                posStart = true;
                compteurDep = 0;
                return;
            }

        }
        if (posStart == true || smoothTransition == false)
        {
            if (i > 0 && cams[i - 1].type == CamBehavior.TypeMovement.Rotation)
            {
                if (!startMouvement)
                {
                   
                    cams[i].startPos.position = transform.position;
                    cams[i].startPos.eulerAngles = transform.eulerAngles;
                }
            }

            if (compteurStart > cams[i].timeBeforeStart) startMouvement = true;

            else
            {
                compteurStart += Time.deltaTime;
                cams[i].destination.LookAt(cams[i].pointOfPivot);
            }
            if (startMouvement)
            {
                float compt = compteurDep / cams[i].timeOfDeplacement;
                transform.position = Vector3.Lerp(cams[i].startPos.position, cams[i].destination.position, compt);
                transform.eulerAngles = Vector3.Lerp(cams[i].startPos.eulerAngles, cams[i].destination.eulerAngles, compt);
                transform.LookAt(cams[i].pointOfPivot);
                compteurDep += Time.deltaTime;

            }
            NextStep();
        }


    }

    public void NextStep()
    {
        if (transform.position == cams[i].destination.position)
        {
            if (i >= (cams.Count - 1))
            {
                //cameraAc.enabled = true;
                this.enabled = false;
                StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;
            }
            if (i < (cams.Count - 1))
            {
                i++;
            }
            startMouvement = false;
            compteurDep = 0;
            compteurStart = 0;
        }

    }



    [System.Serializable]
    public struct CamBehavior
    {
        public enum TypeMovement { Translation, Rotation, Both }

        public TypeMovement type;
        public Transform startPos;
        public float timeBeforeStart;
        public float timeOfDeplacement;
        public Transform destination;
        [Header("Rotation")]
        public Transform pointOfPivot;
        public float angleToRotate;
    };

}
