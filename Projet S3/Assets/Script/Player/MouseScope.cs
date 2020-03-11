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
    public GameObject instanceBullet;
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
    public bool activeSnap;
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

    public RectTransform directionIMG;
    public Vector3 posConvert;
    public GameObject uIGOAim;
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
                            //ReturnState();
                        }
                        //else
                        //{
                        //    ReturnOrientation();
                        //}


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

                        // ReturnState();
                    }
                    if (projectils.returnBall)
                    {
                        //ReturnOrientation();
                    }
                }
            }

            //returnLine = true;
            //destructBool = false;

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
                if (Vector3.Distance(transform.position, instanceBullet.transform.position) < returnSpeed * Time.deltaTime && snapDeactive)
                {
                    DestroyBullet();

                }
            }
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
        if (Physics.Raycast(rayCheckEntity, out hitEntity, distanceMaxOfShoot, 1<<9))
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
            entitySnap.GetComponent<MeshRenderer>().material.color = Color.yellow;


        }
        else
        {
            Vector3 posHit = transform.position;
            Collider[] pos = Physics.OverlapSphere(posHit, distanceMaxOfShoot, 1 << 9);
            float disMin = distanceMaxOfShoot;
            int entity = 0;
            for (int i = 0; i < pos.Length; i++)
            {
                float currentdist = Vector3.Distance(posHit, pos[i].transform.position);
                Vector3 dirEntity = pos[i].transform.position - transform.position;

                if (currentdist < disMin && Vector3.Dot(directionManette.normalized, dirEntity.normalized) > 0.75f)
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
                entitySnap.GetComponent<MeshRenderer>().material.color = Color.yellow;
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


    private void InstantiateProjectile(Vector3 dir)
    {
        if (ennemiStock.ennemiStock == null && instanceBullet == null)
        {

            StateAnim.ChangeState(StateAnim.CurrentState.Tir);
            instanceBullet = Instantiate(bullet, transform.position + (dir).normalized, Quaternion.identity);
            //  meshBullet = Instantiate(Ambout, instanceBullet.transform.position, Quaternion.identity, instanceBullet.transform);
            float angle = Vector3.SignedAngle(transform.forward, (direction + directionManette).normalized, transform.up);

            // Vector3 eulers = new Vector3(Ambout.transform.eulerAngles.x, angle, Ambout.transform.eulerAngles.z);
            // meshBullet.transform.localRotation = Quaternion.Euler(eulers);

            _timerOfBullet = 0;


            projectils = instanceBullet.GetComponent<Projectils>();
            projectils.dir = dir.normalized; //(direction + directionManette).normalized;
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
        LayerMask mask = ~(1 << 11);
        Debug.DrawRay(Camera.main.transform.position, camera.direction * 100);
        if (Physics.Raycast(camera, out hit, Mathf.Infinity, mask))
        {
            posConvert = hit.point + Vector3.up;
        }
        Debug.DrawRay(Camera.main.transform.position, camera.direction * hit.distance);
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
