using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiStock : MonoBehaviour
{

    public GameObject ennemiStock;
    [HideInInspector] public Vector3 pos;
    public int frameForChangeRotation = 10;

    [HideInInspector] public float powerOfProjection;
    [HideInInspector] public bool onHitEnter;
    [Header("Feedback")]
    public GameObject onHitEnemy;

    private MouseScope mouse;
    [HideInInspector] public bool inputNeed;

    [Header("Contact Sound")]
    [FMODUnity.EventRef]
    public string contact;
    public float ContactVolume = 20;
    [Header("Retour Sound")]
    [FMODUnity.EventRef]
    public string OrbitSound;
    public AnimationCurve curveVolumeOrbitation;
    public float OrbitVolume = 10;

    private Klak.Motion.SmoothFollow mySmoothFollow;
    private LineRenderer lineRenderer;
    private bool rotate;
    private bool slam;
    private RotationPlayer rotationPlayer;
    private int frameNoInput;
    private bool lastInputRotation;
    private Material enemyStockMat;
    private Texture ennemyStockTextChange;
    private Color baseColor;
    private float myFOV;
    private bool isOnZoom = false;
    private FMOD.Studio.EventInstance OrbitEvent;
    private FMOD.Studio.EventInstance contactSound;
    private bool startBool;
    private Rigidbody playerRigid;
    private float tempsEcoule;
    private bool right;
    private bool currentRight;
    private LineRend line;
    
    private SlamTry slamTry;
    private bool isSlaming;
    private StateOfEntity  stateOfEntity;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRend>();
        mouse = GetComponent<MouseScope>();
        mySmoothFollow = GetComponent<Klak.Motion.SmoothFollow>();
        myFOV = Camera.main.fieldOfView;
        rotationPlayer = GetComponent<RotationPlayer>();
        playerRigid = GetComponent<Rigidbody>();
        


        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        //Sound
        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(ContactVolume);
        OrbitEvent = FMODUnity.RuntimeManager.CreateInstance(OrbitSound);
        slamTry = GetComponent<SlamTry>();

    }

    // Update is called once per frame
    void Update()
    {
        tempsEcoule += Time.deltaTime;

        if (tempsEcoule > 1.2)
        {
            tempsEcoule = 0;
        }
        OrbitEvent.setVolume(curveVolumeOrbitation.Evaluate(tempsEcoule));
        Camera.main.fieldOfView = myFOV;
        float input = Input.GetAxis("ShootController");


        contactSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        if (ennemiStock != null)
        {

            if (onHitEnter)
            {

                if (StateAnim.state == StateAnim.CurrentState.Tir)
                {
                    StateAnim.ChangeState(StateAnim.CurrentState.Rotate);
                }
                isOnZoom = true;
                Instantiate(onHitEnemy, ennemiStock.transform.position, transform.rotation /*, ennemiStock.transform */);
                baseColor = ennemiStock.gameObject.GetComponent<Renderer>().material.color;
                ennemiStock.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                onHitEnter = false;
                line.active = true;
                frameNoInput = 0;
                ennemiStock.tag = "Untagged";
                stateOfEntity =ennemiStock.GetComponent<StateOfEntity>();
               stateOfEntity.entity = StateOfEntity.EntityState.Catch;
                lastInputRotation = mouse.lastInput;
                if (input < 0)
                {
                    right = true;
                }
                else
                {
                    right = false;
                }
                if (mouse.lastInput)
                {
                    if (ennemiStock.tag == "wall")
                    {
                        rotate = rotationPlayer.StartRotationWall(gameObject, pos, ennemiStock, powerOfProjection, false);

                    }
                    else
                    {
                        rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", powerOfProjection, false);

                    }
                }
                else
                {
                    if (ennemiStock.tag == "wall")
                    {
                        rotate = rotationPlayer.StartRotationWall(gameObject, pos, ennemiStock, powerOfProjection, true);

                    }
                    else
                    {
                        rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", powerOfProjection, true);

                    }

                }
                if (ennemiStock.gameObject.GetComponent<EnnemiBehavior>())
                {
                    ennemiStock.gameObject.GetComponent<EnnemiBehavior>().imStock = true;

                }
                contactSound.start();
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                slamTry.StartSlam(ennemiStock);
                rotationPlayer.StopRotateSlam();
                isSlaming =true;
            }
            if(!isSlaming)
            {
            /* if (isOnZoom)
                {
                    zoomOnHit();
                }*/
                FMOD.Studio.PLAYBACK_STATE orbitState;
                OrbitEvent.getPlaybackState(out orbitState);
                if (orbitState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    OrbitEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                    OrbitEvent.start();
                }
                mySmoothFollow.target = ennemiStock.gameObject.transform;

                if (input < 0)
                {
                    currentRight = true;
                }
                else
                {
                    currentRight = false;
                }

                if (!Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
                {
                    if (right != currentRight || input == 0)
                    {
                        // myRE.Emit();
                        DetachPlayer();
                        lineRenderer.SetPosition(0, transform.position);
                        lineRenderer.SetPosition(1, transform.position);
                    }
                }
                if (input == 0)
                {
                    frameNoInput++;
                }
            }

        }
        else
        {

            rotate = false;
            slam = false;
            startBool = false;

        }
    }


    public void DetachPlayer()
    {
        myFOV = 70;
        isOnZoom = false;
       
           stateOfEntity.entity = StateOfEntity.EntityState.ReturnFormation;
        if (ennemiStock != null && ennemiStock.gameObject.GetComponent<EnnemiBehavior>())
        {
            ennemiStock.GetComponent<EnnemiBehavior>().imStock = false;

        }
        mySmoothFollow.target = null;
        ennemiStock.gameObject.GetComponent<Renderer>().material.color = baseColor;
        if (ennemiStock.tag == "wall")
        {
            rotationPlayer.StopRotation(false);
        }
        else
        {
            rotationPlayer.StopRotation(true);

        }
           isSlaming =false;

        ennemiStock = null;
        OrbitEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

  public void DetachPlayer(Vector3 dir)
    {
        myFOV = 70;
        isOnZoom = false;
       
           stateOfEntity.entity = StateOfEntity.EntityState.ReturnFormation;
        if (ennemiStock != null && ennemiStock.gameObject.GetComponent<EnnemiBehavior>())
        {
            ennemiStock.GetComponent<EnnemiBehavior>().imStock = false;

        }
        mySmoothFollow.target = null;
        ennemiStock.gameObject.GetComponent<Renderer>().material.color = baseColor;
        if (ennemiStock.tag == "wall")
        {
            rotationPlayer.StopRotation(false);
        }
        else
        {
            rotationPlayer.StopRotation(dir);

        }
           isSlaming =false;

        ennemiStock = null;
        OrbitEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public void ResetPlayer()
    {
        DetachPlayer();
        playerRigid.velocity = Vector3.zero;
    }


    public void zoomOnHit()
    {
        if (myFOV == 70)
        {
            myFOV = 90;
        }
        if (myFOV > 70)
        {
            myFOV -= Time.deltaTime * 20;
        }
        else if (myFOV < 70)
        {
            myFOV = 70;
            isOnZoom = false;
        }
    }
    public void StopRotate()
    {
        rotate = false;
        ennemiStock = null;
    }
    public void StopSlam()
    {
        slam = false;
        ennemiStock = null;
    }
}

