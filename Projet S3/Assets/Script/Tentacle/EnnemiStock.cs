using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiStock : MonoBehaviour
{
    public GameObject ennemiStock;
    public Klak.Motion.SmoothFollow mySmoothFollow;
    private LineRenderer lineRenderer;
    private bool rotate;
    private bool slam;
    private RotationPlayer rotationPlayer;
    private SlamPlayer slamPlayer;
    [HideInInspector] public float powerOfProjection;
    [Header("Sound")]
    [FMODUnity.EventRef]
    public string contact;
    private FMOD.Studio.EventInstance contactSound;
    private bool startBool;
    public float volume = 20;
    [Header("Retour Sound")]
    [FMODUnity.EventRef]
    public string OrbitSound;
    private FMOD.Studio.EventInstance OrbitEvent;
    public float OrbitVolume = 10;

    public bool onHitEnter;
    public GameObject onHitEnemy;
    public Material enemyStockMat;
    public Texture ennemyStockTextChange;
    private Color baseColor;
    float myFOV;
    bool isOnZoom = false;
    public RippleEffect myRE;
    // Start is called before the first frame update
    void Start()
    {

        myFOV = Camera.main.fieldOfView;
        rotationPlayer = GetComponent<RotationPlayer>();
        slamPlayer = GetComponent<SlamPlayer>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(1, transform.position);
        //Sound
        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(volume);
        OrbitEvent = FMODUnity.RuntimeManager.CreateInstance(OrbitSound);
        OrbitEvent.setVolume(volume);

    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.fieldOfView = myFOV;
        contactSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (ennemiStock != null)
        {
            if (onHitEnter)
            {
                isOnZoom = true;
                Instantiate(onHitEnemy, ennemiStock.transform.position, transform.rotation /*, ennemiStock.transform */);
                baseColor = ennemiStock.gameObject.GetComponent<Renderer>().material.color;
                ennemiStock.gameObject.GetComponent<Renderer>().material.color = Color.red;
                onHitEnter = false;
            }
            if (!startBool)
            {
                ennemiStock.GetComponent<EnnemiBehavior>().isOnSlam = true;
                ennemiStock.gameObject.GetComponent<EnnemiBehavior>().imStock = true;
                contactSound.start();


                startBool = true;
            }
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, ennemiStock.transform.position);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if(isOnZoom)
                {
                    zoomOnHit();
                }
                FMOD.Studio.PLAYBACK_STATE orbitState;
                OrbitEvent.getPlaybackState(out orbitState);
                if (orbitState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    OrbitEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                    OrbitEvent.start();
                }
                mySmoothFollow.target = ennemiStock.gameObject.transform;
                rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", powerOfProjection);
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    rotationPlayer.ChangeRotationDirection();
                }
                //else if (Input.GetKeyUp(KeyCode.Mouse1))
                //{
                //    rotationPlayer.ChangeRotationDirection();
                //}
            }
            else
            {
                myRE.Emit();
                myFOV = 70;
                isOnZoom = false;
                ennemiStock.GetComponent<EnnemiBehavior>().imStock = false;
                mySmoothFollow.target = null;
                ennemiStock.gameObject.GetComponent<Renderer>().material.color = baseColor;
                ennemiStock = null;
                rotationPlayer.StopRotation();
                OrbitEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
            //if (!slam && !rotate)
            //{
            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        rotate = rotationPlayer.StartRotation(ennemiStock, gameObject, "Ennemi", 60);
            //    }
            //    if (Input.GetMouseButtonDown(1))
            //    {
            //        rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", 10);
            //    }
            //
            //    if (Input.GetKeyDown(KeyCode.A))
            //    {
            //        slam = slamPlayer.StartSlam(ennemiStock, gameObject);
            //    }
            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        slam = slamPlayer.StartSlam(gameObject, ennemiStock);
            //    }
            //}
        }
        else
        {

            rotate = false;
            slam = false;
            startBool = false;

        }
    }

    public void zoomOnHit()
    {
        if(myFOV == 70)
        {
            myFOV = 90;
        }
        if(myFOV > 70)
        {
            myFOV -= Time.deltaTime * 20;
        }
        else if(myFOV < 70)
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

