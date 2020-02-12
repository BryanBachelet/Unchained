using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScope : MonoBehaviour
{

    public Material line;
    public GameObject bullet;
    [HideInInspector] public GameObject Ambout;

    private EnnemiStock ennemiStock;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 directionManette;
    [HideInInspector] public GameObject instanceBullet;
    private LineRenderer lineRenderer;
    private bool returnLine;
    private bool destructBool;
    private Vector3 ballPos;
    private Vector3 returnPos;
    private float distanceReturn;
    private Vector3 dirReturn;
    [Header("Carateristique Bullet")]
    public float timerOfBullet = 0.5f;
    public float speedOfBullet;
    public float timeBetweenShoot = 0.4f;
    private float _timerOfBullet;
    public float returnSpeed = 50;
    public float distanceMaxOfShoot = 75;
    private GameObject meshBullet;
    Projectils projectils;
    [Header("Options")]
    public bool distanceDestruct;
    public bool activePC;
    [Header("Retour Sound")]
    [FMODUnity.EventRef]
    public string returnSound;
    private FMOD.Studio.EventInstance returnEvent;
    public float returnVolume = 10;
    [HideInInspector] public bool lastInput;
    [Header("Tirer Sound")]
    [FMODUnity.EventRef]
    public string contact;
    private FMOD.Studio.EventInstance contactSound;
    public float volume = 10;
    private bool resetShoot;
    // Start is called before the first frame update
    void Start()
    {

        ennemiStock = GetComponent<EnnemiStock>();
        lineRenderer = GetComponent<LineRenderer>();
        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(volume);
        returnEvent = FMODUnity.RuntimeManager.CreateInstance(returnSound);
        returnEvent.setVolume(returnVolume);
        speedOfBullet = distanceMaxOfShoot / timerOfBullet;
        returnSpeed = distanceMaxOfShoot / timeBetweenShoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (activePC)
        {
            direction = DirectionSouris();
        }

        if (DirectionManette() != Vector3.zero)
        {
            directionManette = DirectionManette();
        }
        //if (directionManette != Vector3.zero)
        //{
        //    direction = Vector3.zero;
        //}
        float input = Input.GetAxis("Attract1");

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || resetShoot && input != 0)
        {
            resetShoot = false;
            InstantiateProjectile();
        }

        if (input == 0)
        {
            resetShoot = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            lastInput = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            lastInput = false;
        }

        if (ennemiStock.ennemiStock == null && instanceBullet != null)
        {


            if (!distanceDestruct)
            {
                if (_timerOfBullet > timerOfBullet)
                {
                    if (!projectils.returnBall)
                    {
                        ReturnState();
                    }
                    else
                    {
                        ReturnOrientation();
                    }


                }
                else
                {
                    _timerOfBullet += Time.deltaTime;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, instanceBullet.transform.position) >= distanceMaxOfShoot && !projectils.returnBall)
                {
                    ReturnState();
                }
                if (projectils.returnBall)
                {
                    ReturnOrientation();
                }
            }
            returnLine = true;
            destructBool = false;

            ballPos = instanceBullet.transform.position;

        }

        if (ennemiStock.ennemiStock == null && instanceBullet == null)
        //Return de line renderer après le tir;
        {
            if (returnLine)
            {
                if (!destructBool)
                {
                    returnPos = ballPos;
                    returnEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

                    returnEvent.start();
                    destructBool = true;
                }


                returnLine = false;
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }


            }
        }
        if (ennemiStock.ennemiStock == null && instanceBullet != null)
        {

            if (projectils.returnBall)
            {
                projectils.dir = -(projectils.transform.position - transform.position);
                if (Vector3.Distance(transform.position, instanceBullet.transform.position) < returnSpeed * Time.deltaTime)
                {

                    StateAnim.ChangeState(StateAnim.CurrentState.Idle);
                    Destroy(instanceBullet);
                }
            }
        }
    }

    private void OnRenderObject()
    {
        if (Camera.current.name == "Camera")
        {
            if (ennemiStock.ennemiStock == null && instanceBullet == null)
            {
                GL.Begin(GL.LINES);
                line.SetPass(0);

                GL.Color(Color.red);
                GL.Vertex(transform.position);
                GL.Vertex(transform.position + (direction + directionManette).normalized * distanceMaxOfShoot);
                GL.End();
            }

        }

    }


    private void ReturnState()
    {
        projectils.returnBall = true;
        projectils.dir = -projectils.dir;
        projectils.speed = returnSpeed;
    }
    private void ReturnOrientation()
    {
        projectils.dir = transform.position - instanceBullet.transform.position;
        float angle = Vector3.SignedAngle(transform.forward, projectils.dir, transform.up);
        //Vector3 eulers = new Vector3(meshBullet.transform.eulerAngles.x, angle, meshBullet.transform.eulerAngles.z);
        // meshBullet.transform.localRotation = Quaternion.Euler(eulers);
    }


    private void InstantiateProjectile()
    {
        if (ennemiStock.ennemiStock == null && instanceBullet == null)
        {

            StateAnim.ChangeState(StateAnim.CurrentState.Tir);
            instanceBullet = Instantiate(bullet, transform.position + (direction + directionManette).normalized, Quaternion.identity);
            //  meshBullet = Instantiate(Ambout, instanceBullet.transform.position, Quaternion.identity, instanceBullet.transform);
            float angle = Vector3.SignedAngle(transform.forward, (direction + directionManette).normalized, transform.up);

            // Vector3 eulers = new Vector3(Ambout.transform.eulerAngles.x, angle, Ambout.transform.eulerAngles.z);
            // meshBullet.transform.localRotation = Quaternion.Euler(eulers);

            _timerOfBullet = 0;


            projectils = instanceBullet.GetComponent<Projectils>();
            projectils.dir = (direction + directionManette).normalized;
            projectils.player = gameObject;
            projectils.lineRenderer = lineRenderer;
            projectils.speed = speedOfBullet;
            projectils.moveAlone = GetComponent<PlayerMoveAlone>();
            contactSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            contactSound.start();

        }

    }

    private Vector3 DirectionSouris()
    {
        Ray camera = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rauEnter;

        if (ground.Raycast(camera, out rauEnter))
        {
            Vector3 pointToLook = camera.GetPoint(rauEnter);
            Vector3 posPlayer = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 dir = pointToLook - posPlayer;
            return dir;
        }
        else
        {
            return Vector3.zero;
        }
    }
    private Vector3 DirectionManette()
    {
        float aimHorizontal = Input.GetAxis("AimHorizontal1");
        float aimVertical = -Input.GetAxis("AimVertical1");

        Vector3 dir = new Vector3(aimHorizontal, 0, aimVertical);
        return dir.normalized;
    }
}
