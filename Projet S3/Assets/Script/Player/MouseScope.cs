using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScope : MonoBehaviour
{

    [Header("Fonctionnement")]
    public GameObject prefabBullet;
    public GameObject uIGOAim;

    [Header("Input")]
    public bool controllerPc;
    [Tooltip("Only for Contoller")]
    public float deadZone = 0.1f;

    [Header("Debug")]
    public Material glMaterial;

    [Header("Carateristique Bullet")]
    public float timerOfBullet = 0.5f;
    public float speedOfBullet;



    [Header("Snap")]
    public bool activeSnap;
    public LayerMask layerMask;
    [Range(0.5f, 1)]
    public float angleSnap = 0.75f;
    public Color colorSnap;

    [Header("Tirer Sound")]
    [FMODUnity.EventRef]
    public string shootEvent;
    private FMOD.Studio.EventInstance shotSound;
    public float volume = 10;

    public int lastInput;
    [HideInInspector] public GameObject instanceBullet;
    [HideInInspector] public Vector3 aimDirection;


    private RectTransform directionIMG;
    private EnnemiStock ennemiStock;
    private LineRenderer lineRenderer;

    private float _timerOfBullet;

    private Projectils projectils;
    public bool resetShoot;
    private Vector3 posConvert;
    private bool snapDeactive;
    private GameObject entitySnap;
    private float frame;

    private float _compteurBetweenBullet;

   
    void Start()
    {
        Cursor.visible = true;
        directionIMG = uIGOAim.GetComponent<RectTransform>();
        ennemiStock = GetComponent<EnnemiStock>();
        lineRenderer = GetComponent<LineRenderer>();


        shotSound = FMODUnity.RuntimeManager.CreateInstance(shootEvent);
        shotSound.setVolume(volume);

    }

    
    void Update()
    {
        aimDirection = DirectionOfAim(controllerPc);


        if (ennemiStock.ennemiStock == null)
        {

            if (!resetShoot && FireInputActive(controllerPc))
            {
             
                if (!activeSnap)
                {
                    InstantiateProjectile(aimDirection.normalized);
                }
                else
                {
                    Snap(true);
                }
                if (FireInputValue(controllerPc) > 0)
                {
                    lastInput = 1;
                    resetShoot = true;
                }

                if (FireInputValue(controllerPc) < 0)
                {
                    lastInput = 0;
                    resetShoot = true;
                }

            }
        }
        Snap(false);

        if (!FireInputActive(controllerPc) &&resetShoot && instanceBullet == null)
        {
            
                ReactiveShot();
               
        }



        if (ennemiStock.ennemiStock == null && instanceBullet != null)
        {

            if (_timerOfBullet > timerOfBullet)
            {
                DestroyBullet();

            }
            else
            {
                _timerOfBullet += Time.deltaTime;
            }

        }


    }

    private Vector3 GetAimInputPC()
    {
        Vector3 aimInputDirection = Input.mousePosition;
        float aimX = (aimInputDirection.x / Screen.width);
        aimX = aimX > 0.5f ? (aimX - 0.5f) / 0.5f: ((aimX-0.5f)/0.5f);
        float aimY = (aimInputDirection.y / Screen.height);
        aimY = aimY > 0.5f ? (aimY - 0.5f) / 0.5f : ((aimY - 0.5f) / 0.5f);
        aimInputDirection = new Vector3(aimX, aimY, 0);
       
        return aimInputDirection;
    }

    private Vector3 GetAimInputController()
    {
        float aimHorizontal = Input.GetAxis("AimHorizontal1");
        float aimVertical = -Input.GetAxis("AimVertical1");
        Vector3 aimInputDirection = new Vector3(aimHorizontal, aimVertical, 0);
        return aimInputDirection.magnitude > deadZone ? aimInputDirection : Vector3.zero;
    }

    private Vector3 DirectionOfAim(bool PC)
    {
        Vector3 aimInput = Vector3.zero;
        if (!PC)
        {
            aimInput = GetAimInputController();
        }
        else
        {
            aimInput = GetAimInputPC();
        }
        directionIMG.localPosition = new Vector3(aimInput.x * 960, aimInput.y * 540, 0);
     
        Ray camera = Camera.main.ScreenPointToRay(directionIMG.position);
       RaycastHit hit;
        LayerMask mask = 1 << 12;

        if (Physics.Raycast(camera, out hit, Mathf.Infinity, mask))
        {

      
            posConvert = hit.point + Vector3.up;
        }
        Vector3 dir = (posConvert - transform.position).normalized;
        return dir;
    }

    public bool FireInputActive(bool PC)
    {
        bool getInput = false;

        if (!PC)
        {
            getInput = Input.GetAxis("ShootController") != 0 ? true : false;
        }
        else
        {
            
                Debug.Log(FireInputValue(controllerPc));
                getInput = Mathf.Abs(Input.GetAxis("ShootPC")) >0.5 ? true:false ;
         
       
        }
        return getInput;
    }

    public float FireInputValue(bool PC)
    {
        float valueInput = 0;

        if (!PC)
        {
            valueInput = Input.GetAxis("ShootController");
        }
        else
        {
            valueInput = Input.GetAxis("ShootPC");
        }
        return valueInput;
    }

    public void Snap(bool shoot)
    {

        snapDeactive = true;

        Ray rayCheckEntity = new Ray(transform.position, aimDirection.normalized);
        RaycastHit hitEntity = new RaycastHit();
        if (Physics.Raycast(rayCheckEntity, out hitEntity, DistanceMaxShoot(), layerMask))
        {
            if (shoot)
            {
                InstantiateProjectile(aimDirection.normalized);
            }
            FeedbackSnap(hitEntity.collider.gameObject);

        }
        else
        {
            Vector3 posHit = transform.position;
            Collider[] pos = Physics.OverlapSphere(posHit, DistanceMaxShoot(), layerMask);
            float disMin = DistanceMaxShoot();
            int entity = 0;
            for (int i = 0; i < pos.Length; i++)
            {
                float currentdist = Vector3.Distance(posHit, pos[i].transform.position);
                Vector3 dirEntity = pos[i].transform.position - transform.position;

                if (currentdist < disMin && Vector3.Dot(aimDirection.normalized, dirEntity.normalized) > angleSnap
                    && pos[i].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy &&pos[i].transform.position.y>1.5f) 
                {

                    disMin = currentdist;
                    entity = i;
                }
            }
            if (entity != 0 && disMin != DistanceMaxShoot())
            {
                FeedbackSnap(pos[entity].gameObject);
            }
            else
            {
                FeedbackSnap(null);
            }
            if (shoot)
            {
                if (entity != 0 && disMin != DistanceMaxShoot())
                {
                    Vector3 dir = pos[entity].transform.position - transform.position;
                    InstantiateProjectile(dir.normalized);
                    snapDeactive = false;
                }
                else
                {
                    InstantiateProjectile(aimDirection.normalized);
                }
            }


        }
    }

    private void FeedbackSnap(GameObject entityGive)
    {
        if (entitySnap != null)
        {
            entitySnap.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        if (entityGive != null)
        {
            entitySnap = entityGive;
            entitySnap.GetComponent<MeshRenderer>().material.color = colorSnap;
        }
    }

    private void ReactiveShot()
    {
        resetShoot = false;
        lastInput = 2;
    }

    private void InstantiateProjectile(Vector3 directionOfProjectileMouvement)
    {
        StateAnim.ChangeState(StateAnim.CurrentState.Tir);
        instanceBullet = Instantiate(prefabBullet, transform.position + (directionOfProjectileMouvement).normalized, Quaternion.identity);

        _timerOfBullet = 0;

        projectils = instanceBullet.GetComponent<Projectils>();
        projectils.dir = directionOfProjectileMouvement.normalized;
        projectils.player = gameObject;
        projectils.lineRenderer = lineRenderer;
        projectils.speed = speedOfBullet;
        projectils.moveAlone = GetComponent<PlayerMoveAlone>();

        shotSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        shotSound.start();

    }

    private void ReturnOrientation()
    {
        projectils.dir = transform.position - instanceBullet.transform.position;
        float angle = Vector3.SignedAngle(transform.forward, projectils.dir, transform.up);

    }

    public float DistanceMaxShoot()
    {
        return (speedOfBullet * timerOfBullet);
    }

    public void DestroyBullet()
    {
        StateAnim.ChangeState(StateAnim.CurrentState.Idle);
        Destroy(instanceBullet);
        lineRenderer.SetPosition(0, transform.position);
    }




    private void OnRenderObject()
    {
        if (Camera.current.name == "Camera" && StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {


            GL.Begin(GL.LINES);
            glMaterial.SetPass(0);

            GL.Color(Color.red);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + aimDirection.normalized * DistanceMaxShoot());
            GL.End();

            GL.Begin(GL.LINES);
            glMaterial.SetPass(0);

            GL.Color(Color.red);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + (Quaternion.Euler(0, Mathf.Rad2Deg * (1 - angleSnap), 0) * aimDirection.normalized) * DistanceMaxShoot());
            GL.End();

            GL.Begin(GL.LINES);
            glMaterial.SetPass(0);

            GL.Color(Color.red);
            GL.Vertex(transform.position);

            GL.Vertex(transform.position + (Quaternion.Euler(0, (Mathf.Rad2Deg * (-1 + angleSnap)), 0) * aimDirection.normalized) * DistanceMaxShoot());
            GL.End();

        }

    }

}
