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
private float input ;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRend>();
        mouse = GetComponent<MouseScope>();
        mySmoothFollow = GetComponent<Klak.Motion.SmoothFollow>();
        myFOV = Camera.main.fieldOfView;
        rotationPlayer = GetComponent<RotationPlayer>();
        playerRigid = GetComponent<Rigidbody>();
        

// Line Renderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
// --------------------        
        //Sound
        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(ContactVolume);
        OrbitEvent = FMODUnity.RuntimeManager.CreateInstance(OrbitSound);
        slamTry = GetComponent<SlamTry>();

    }

    // Update is called once per frame
    void Update()
    {
        
        ActivationLien();
        tempsEcoule += Time.deltaTime;

        if (tempsEcoule > 1.2)
        {
            tempsEcoule = 0;
        }
        OrbitEvent.setVolume(curveVolumeOrbitation.Evaluate(tempsEcoule));
        contactSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        input = Input.GetAxis("ShootController");

        if (ennemiStock != null)
        {
           ActivationLien();
           ActiveSlam();

            
            if(!isSlaming)
            {
                FeedbackDuringRotation();
                InputCheck();
                DeactiveLien();
            }

        }
        else
        {
         ResetAttach();
        }
    }




    public void ActivationLien()
    {
        if (onHitEnter)
        {
            FeedbackHit();
            ChangeAgentHit();
            PlayerChange();
            InputRegister();
            ActiveRotation();
            onHitEnter = false;
        }
    }

    private void ChangeAgentHit()
    {
    ennemiStock.tag = "Untagged";
    stateOfEntity =ennemiStock.GetComponent<StateOfEntity>();
    stateOfEntity.entity = StateOfEntity.EntityState.Catch;
    }

    private void PlayerChange()
    {
    line.active = true;
    if (StateAnim.state == StateAnim.CurrentState.Tir)
        {
            StateAnim.ChangeState(StateAnim.CurrentState.Rotate);
        }
    }

    private void InputRegister()
    {
    float input = Input.GetAxis("ShootController");
    lastInputRotation = mouse.lastInput;

    if (input < 0)
    {
        right = true;
    }
    else
    {
        right = false;
    }
    }

    private void ActiveRotation()
    {
    if (mouse.lastInput)
    {
        rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", powerOfProjection, false);
    }
    else
    {
        rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", powerOfProjection, true);
    }
    }

    private void FeedbackHit()
    {
    Instantiate(onHitEnemy, ennemiStock.transform.position, transform.rotation /*, ennemiStock.transform */);
    baseColor = ennemiStock.gameObject.GetComponent<Renderer>().material.color;
    ennemiStock.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    #region  Son
    contactSound.start();
    #endregion
    }

    private void ActiveSlam()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2)|| Input.GetKeyDown(KeyCode.A))
        {
            slamTry.StartSlam(ennemiStock);
            rotationPlayer.StopRotateSlam();
            isSlaming =true;
        }
    }

    private void ResetAttach()
    {
    rotate = false;
    slam = false;
    startBool = false;
    }

    private void FeedbackDuringRotation()
    {
    FMOD.Studio.PLAYBACK_STATE orbitState;
    OrbitEvent.getPlaybackState(out orbitState);
    if (orbitState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
    {
        OrbitEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        OrbitEvent.start();
    }
    }

    private void InputCheck()
    {
    if (input < 0)
    {
        currentRight = true;
    }
    else
    {
        currentRight = false;
    }
    }

    private void  DeactiveLien()
{
     if (!Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
                {
                    if (right != currentRight || input == 0)
                    {

                        DetachPlayer();
                        // Lien renderer 
                        lineRenderer.SetPosition(0, transform.position);
                        lineRenderer.SetPosition(1, transform.position);
                    }
                }
}

    public void DetachPlayer()
    {
        stateOfEntity.entity = StateOfEntity.EntityState.Destroy;
        if(ennemiStock!=null) 
        {
            ennemiStock.gameObject.GetComponent<Renderer>().material.color = baseColor;
        }
        rotationPlayer.StopRotation(true);
        isSlaming =false;
        ennemiStock = null;
        OrbitEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

   public void DetachPlayer(Vector3 dir)
    {
        stateOfEntity.entity = StateOfEntity.EntityState.Destroy;
        ennemiStock.gameObject.GetComponent<Renderer>().material.color = baseColor;
        rotationPlayer.StopRotation(dir);
        isSlaming =false;
        ennemiStock = null;
        OrbitEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    
    public void ResetPlayer()
    {
        DetachPlayer();
        playerRigid.velocity = Vector3.zero;
    }

    public void StopRotate()
    {
        rotate = false;
        ennemiStock = null;
    }
    
    
}

