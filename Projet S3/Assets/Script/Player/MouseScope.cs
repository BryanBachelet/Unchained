using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScope : MonoBehaviour
{

    [Header("Fonctionnement")]
    public GameObject bullet;
    public RectTransform directionIMG;
    private Vector3 posConvert;
    public GameObject uIGOAim;

    
    [Header("Debug")]
    public Material line;
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
    
    public float distanceMaxOfShoot = 75;
    private GameObject meshBullet;
    Projectils projectils;
    [Header("Options")]
    public bool distanceDestruct;
    public bool activePC;
    public bool activeSnap;
    [Header("Snap")]
    public LayerMask layerMask;
    [Range(0.5f, 1)]
    public float angleSnap = 0.75f;
    public Color colorSnap;
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

    private bool snapDeactive;
    private GameObject entitySnap;

    private float frame;
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

        float input = Input.GetAxis("Attract1");

        if (StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || !resetShoot && input != 0)
            {
                frame = 0;
                if (!activeSnap)
                {
                    InstantiateProjectile(directionManette.normalized);
                }
                else
                {
                    Snap(true);
                }
            }
            if (!resetShoot && input == 0)
            {
                Snap(false);
            }
        }

        if (input == 0)
        {
            frame++;
            if (frame > 1)
            {

                resetShoot = false;
            }
        }

        if (resetShoot == false)
        {
            if (Input.GetMouseButtonDown(0) || input < 0)
            {
                lastInput = true;
                resetShoot = true;
            }

            if (Input.GetMouseButtonDown(1) || input > 0)
            {
                lastInput = false;
                resetShoot = true;
            }
        }

        if (ennemiStock.ennemiStock == null && instanceBullet != null)
        {

            if (snapDeactive)
            {

                if (!distanceDestruct)
                {
                    if (_timerOfBullet > timerOfBullet)
                    {
                        if (!projectils.returnBall)
                        {
                            Destroy(instanceBullet);
                            lineRenderer.SetPosition(0, transform.position);
                         
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
                        Destroy(instanceBullet);
                    }
                 
                }
            }

            

            ballPos = instanceBullet.transform.position;

        }

       
       
    }




    public void Snap(bool active)
    {
        if (active)
        {
            snapDeactive = true;
        }
        Ray rayCheckEntity = new Ray(transform.position, directionManette.normalized);
        RaycastHit hitEntity = new RaycastHit();
        if (Physics.Raycast(rayCheckEntity, out hitEntity, distanceMaxOfShoot, layerMask))
        {
            if (active)
            {
                InstantiateProjectile(directionManette.normalized);
            }

            if (entitySnap != null)
            {
                entitySnap.GetComponent<MeshRenderer>().material.color = Color.white;
            }

            entitySnap = hitEntity.collider.gameObject;
            entitySnap.GetComponent<MeshRenderer>().material.color = colorSnap;


        }
        else
        {
            Vector3 posHit = transform.position;
            Collider[] pos = Physics.OverlapSphere(posHit, distanceMaxOfShoot, layerMask);
            float disMin = distanceMaxOfShoot;
            int entity = 0;
            for (int i = 0; i < pos.Length; i++)
            {
                float currentdist = Vector3.Distance(posHit, pos[i].transform.position);
                Vector3 dirEntity = pos[i].transform.position - transform.position;

                if (currentdist < disMin && Vector3.Dot(directionManette.normalized, dirEntity.normalized) > angleSnap)
                {

                    disMin = currentdist;
                    entity = i;
                }
            }

            if (entitySnap != null)
            {
                entitySnap.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            if (entity != 0 && disMin != distanceMaxOfShoot)
            {
                entitySnap = pos[entity].gameObject;
                entitySnap.GetComponent<MeshRenderer>().material.color = colorSnap;
            }

            if (active)
            {
                if (entity != 0 && disMin != distanceMaxOfShoot)
                {
                    Vector3 dir = pos[entity].transform.position - transform.position;
                    InstantiateProjectile(dir.normalized);
                    snapDeactive = false;
                }
                else
                {
                    InstantiateProjectile(directionManette.normalized);
                }
            }

        }
    }

    public void DestroyBullet()
    {
        StateAnim.ChangeState(StateAnim.CurrentState.Idle);
        Destroy(instanceBullet);
    }

    private void OnRenderObject()
    {
        if (Camera.current.name == "Camera" && StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {

            GL.Begin(GL.LINES);
            line.SetPass(0);

            GL.Color(Color.red);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + (direction + directionManette).normalized * distanceMaxOfShoot);
            GL.End();

            GL.Begin(GL.LINES);
            line.SetPass(0);

            GL.Color(Color.red);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + (Quaternion.Euler(0, 24, 0) * (direction + (directionManette)).normalized) * distanceMaxOfShoot);
            GL.End();

            GL.Begin(GL.LINES);
            line.SetPass(0);

            GL.Color(Color.red);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + (Quaternion.Euler(0, -24, 0) * (direction + directionManette).normalized) * distanceMaxOfShoot);
            GL.End();
            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, (direction + directionManette).normalized * distanceMaxOfShoot);
    }
   
    private void ReturnOrientation()
    {
        projectils.dir = transform.position - instanceBullet.transform.position;
        float angle = Vector3.SignedAngle(transform.forward, projectils.dir, transform.up);
        
    }


    private void InstantiateProjectile(Vector3 dir)
    {
        if (ennemiStock.ennemiStock == null && instanceBullet == null)
        {

            StateAnim.ChangeState(StateAnim.CurrentState.Tir);
            instanceBullet = Instantiate(bullet, transform.position + (dir).normalized, Quaternion.identity);
           

            _timerOfBullet = 0;


            projectils = instanceBullet.GetComponent<Projectils>();
            projectils.dir = dir.normalized; 
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
        Ray camera = Camera.main.ScreenPointToRay(uIGOAim.transform.position);
        RaycastHit hit;
        LayerMask mask = 1<<12;

        if (Physics.Raycast(camera, out hit, Mathf.Infinity, mask))
        {

           
            posConvert = hit.point + Vector3.up;
        }
       
        float aimHorizontal = Input.GetAxis("AimHorizontal1");
        float aimVertical = -Input.GetAxis("AimVertical1");

        Vector3 dir = (posConvert - transform.position).normalized;
        if (dir.magnitude < 0.1f)
        {
            dir = Vector3.zero;
        }
        directionIMG.localPosition = new Vector3(aimHorizontal * 370, aimVertical * 370, 0);
        return dir;
    }
}
