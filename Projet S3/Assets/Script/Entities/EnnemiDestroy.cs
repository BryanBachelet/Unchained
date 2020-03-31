using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiDestroy : MonoBehaviour
{

    [Header("Ejection")]
    public float angleRotate;
    public int offset;

    public Transform pivotTransform;
    public float timerBeforeDestroy = 2;
    public float fallMultiplier = 5f;
    public float lowMultiplier = 2.5f;
    public float ejectionPower = 50;
    public float currentForceOfEjection = 50;

    public float deccelerationOfForceOfEjection = 5;
    public float upForce = 25;

     public bool isExplosion = false;
    [HideInInspector] public Vector3 dirProjection;

    private float compteur;
    private GameObject player;
    private Rigidbody ennemiRigidBody;
    private float currentForce = 0;
    private StateOfEntity stateOfEntity;


    void Start()
    {
        stateOfEntity = GetComponent<StateOfEntity>();
        ennemiRigidBody = GetComponent<Rigidbody>();
        player = PlayerMoveAlone.Player1;
        currentForceOfEjection =  ejectionPower;
    }

    void Update()
    {

        if(StateOfGames.currentPhase == StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
                ejectionPower = 100;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (!isExplosion)
        {
            ProjectionAgent();

        }
      
        if (isExplosion)
        {
            ExplosionAgent();

            if (compteur > 4)
            {
                DestroyAgent();
            }
            compteur += Time.deltaTime;
       
        }

        DetectWall();
    }

    public void ActiveProjection(Vector3 dir)
    {
        isExplosion = false;
        dirProjection = dir.normalized;
        ennemiRigidBody.velocity = (upForce * Vector3.up);
    }

    public void ActiveExplosion(Vector3 dir)
    {
        isExplosion = true;
        dirProjection = dir.normalized;
    }

    public void ProjectionAgent()
    {
        ennemiRigidBody.useGravity = true;
        ennemiRigidBody.velocity = (dirProjection.normalized * currentForceOfEjection) + (Vector3.up * ennemiRigidBody.velocity.y);
        if (currentForceOfEjection > 0)
        {
            currentForceOfEjection -= deccelerationOfForceOfEjection * Time.deltaTime;
        }
        else
        {
            ResetAgent();
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

    public void ExplosionAgent()
    {
       
        ennemiRigidBody.velocity = (dirProjection.normalized * currentForceOfEjection);
    }

    private void DestroyAgent()
    {
        stateOfEntity.entity = StateOfEntity.EntityState.Dead;
        gameObject.SetActive(false);
        
    }

    private void ResetAgent()
    {
        currentForceOfEjection = ejectionPower;
        compteur = 0;
        stateOfEntity.entity = StateOfEntity.EntityState.ReturnFormation;
    }

    private void DetectWall()
    {
        Ray ray = new Ray(transform.position, ennemiRigidBody.velocity.normalized);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 1.5f * ennemiRigidBody.velocity.magnitude * 2 * Time.deltaTime))
        {
            if (hit.collider.tag == "wall")
            {
                DestroyAgent();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "wall")
        {
            DestroyAgent();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "wall")
        {
            DestroyAgent();
        }
    }
}

