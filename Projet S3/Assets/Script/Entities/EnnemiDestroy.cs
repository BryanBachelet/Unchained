using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiDestroy : MonoBehaviour
{
    public bool isDestroying = false;
    [Header("Ejection")]
    public float angleRotate;
    public int offset;

    public Transform pivotTransform;
    public float timerBeforeDestroy = 2;

    public float fallMultiplier = 5f;
    public float lowMultiplier = 2.5f;
    public float currentForceOfEjection = 50;
    public float deccelerationOfForceOfEjection = 5;
    public float upForce = 25;

    [Header("AutoDestroy")]
    public float tpsDestroy = 90;
    public float tpsEcoule;

    [Header("Sound")]
    [FMODUnity.EventRef]
    public string contact;
    //private FMOD.Studio.EventInstance contactSound;
    public float volume = 20;

    [HideInInspector] public Vector3 destroyDir;
    [HideInInspector] public bool activePull;
    [HideInInspector] public ManagerEntites managerEntites;
    [HideInInspector] public int i = 0;
    [HideInInspector] public bool isExplosion = false;
    [HideInInspector] public Vector3 dirHorizontalProjection;
    [HideInInspector] public GameObject vfxBlueUp;


    private float compteur;
    private bool enter = false;
    private GameObject player;
    private Vector3 dirVertical;
    private Rigidbody ennemiRigidBody;
    private float currentForce = 0;
    private Vector3 deltaRotation;
    private StateOfEntity stateOfEntity;
    private int compteurDestroy;
    private float rndX;

    void Start()
    {
        stateOfEntity = GetComponent<StateOfEntity>();
        ennemiRigidBody = GetComponent<Rigidbody>();
        player = PlayerMoveAlone.Player1;
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (!isExplosion)
        {
            ProjectionAgent();

        }
        
        if (isExplosion)
        {

            if (compteur > 10)
            {
                EndAgent();
            }
            compteur += Time.deltaTime;
        }

        Ray ray = new Ray(transform.position, ennemiRigidBody.velocity.normalized);
        RaycastHit hit = new RaycastHit();


        if (Physics.Raycast(ray, out hit, 1.5f * ennemiRigidBody.velocity.magnitude * Time.deltaTime))
        {
            if (hit.collider.tag == "wall")
            {
                EndAgent();
            }
        }
    }

    public void ActiveProjection( Vector3 dir)
    {
        isExplosion = false;
        dirHorizontalProjection = dir.normalized;
    }

    public void ProjectionAgent()
    {
        ennemiRigidBody.velocity = (dirHorizontalProjection.normalized * currentForceOfEjection) + (Vector3.up * ennemiRigidBody.velocity.y);
        if (currentForceOfEjection > 0)
        {
            currentForceOfEjection -= deccelerationOfForceOfEjection * Time.deltaTime;
        }
        else
        {
            isDestroying = false;
            stateOfEntity.entity = StateOfEntity.EntityState.ReturnFormation;
        }

        if (ennemiRigidBody.velocity.y < 0)
        {
            ennemiRigidBody.velocity += (Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);

        }
        else
        {
            ennemiRigidBody.velocity += (Vector3.up * Physics.gravity.y * (lowMultiplier - 1) * Time.deltaTime);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDestroying && collision.collider.tag == "wall")
        {
            EndAgent();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (isDestroying && collision.collider.tag == "wall")
        {
            EndAgent();
        }
    }


    public void ActiveExplosion()
    {
        isDestroying = true;
        isExplosion = true;
    }

    private void EndAgent()
    {
        if (!activePull)
        {
            Transform transfChild = transform.GetChild(0);
            transfChild.gameObject.SetActive(false);
            enter = false;
            isDestroying = false;
            compteur = 0;
            tpsEcoule = 0;
            if (i == 1)
            {
                managerEntites.AddEntites(gameObject, managerEntites.entitiesBlue);
            }
            if (i == 2)
            {
                managerEntites.AddEntites(gameObject, managerEntites.entitiesOrange);
            }
            if (i == 3)
            {
                managerEntites.AddEntites(gameObject, managerEntites.entitiesViolet);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

