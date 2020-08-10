﻿using System.Collections;
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

    public float  test;

    public bool isExplosion = false;
    [HideInInspector] public Vector3 dirProjection;

    private float compteur;
    private GameObject player;
    private Rigidbody ennemiRigidBody;
    private float currentForce = 0;
    private StateOfEntity stateOfEntity;

    private Anim_Cultist_States anim_Cultist_States;

 

    void Awake()
    {
        stateOfEntity = GetComponent<StateOfEntity>();
        ennemiRigidBody = GetComponent<Rigidbody>();
        player = PlayerMoveAlone.Player1;
        currentForceOfEjection =  ejectionPower;
        if(GetComponentInChildren<Anim_Cultist_States>() != null)
        {
            anim_Cultist_States  =  GetComponentInChildren<Anim_Cultist_States>();
        }
    }

    void Update()
    {

       
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
        if(ennemiRigidBody ==  null )
        {
            ennemiRigidBody = GetComponent<Rigidbody>();
        }
        ennemiRigidBody.velocity = (upForce * Vector3.up);
        currentForceOfEjection  = ejectionPower;
    }

    public void ActiveExplosion(Vector3 dir)
    {
        isExplosion = true;
        dirProjection = dir.normalized;
        currentForceOfEjection = ejectionPower;
    }

    public void ProjectionAgent()
    {
        ennemiRigidBody.useGravity = true;
        ennemiRigidBody.velocity = (dirProjection.normalized * currentForceOfEjection) + (Vector3.up * ennemiRigidBody.velocity.y);
        if (currentForceOfEjection > 0)
        {
            currentForceOfEjection -= deccelerationOfForceOfEjection * Time.deltaTime;
            RotateAgent();
            if(transform.position.y>1.5f)
            {
                if(anim_Cultist_States != null && stateOfEntity.entity != StateOfEntity.EntityState.Catch )
                {
                    anim_Cultist_States.ChangeAnimState( Anim_Cultist_States.AnimCultistState.Projection_FallAir);
                }
            }
           else 
            {
                if(anim_Cultist_States != null && stateOfEntity.entity != StateOfEntity.EntityState.Catch )
                {
                    anim_Cultist_States.ChangeAnimState( Anim_Cultist_States.AnimCultistState.Projection_BackFloor);
                }
                
            }
        }
        else
        {
        test=  ennemiRigidBody.velocity.y;
            if(ennemiRigidBody.velocity.y< 1 && ennemiRigidBody.velocity.y>-1)
            {
            ResetAgent();
            }
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
        if(anim_Cultist_States != null)
        {
            anim_Cultist_States.ChangeAnimState( Anim_Cultist_States.AnimCultistState.Projection_FallAir);
        } 
    }

    private void DestroyAgent()
    {
        stateOfEntity.entity = StateOfEntity.EntityState.Dead;
        DataPlayer.entityKill++;
        DataPlayer.ChangeScore(100,true);
        gameObject.SetActive(false);
        
        
    }

    private void ResetAgent()
    {
        currentForceOfEjection = ejectionPower;
        compteur = 0;
        stateOfEntity.entity = StateOfEntity.EntityState.ReturnFormation; 
        if(GetComponentInChildren<Anim_Cultist_States>() != null)
        {
          anim_Cultist_States.ChangeAnimState( Anim_Cultist_States.AnimCultistState.Run);
        }
    }

    private void DetectWall()
    {
        Ray ray = new Ray(transform.position, ennemiRigidBody.velocity.normalized);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 1.5f * ennemiRigidBody.velocity.magnitude * 2 * Time.deltaTime))
        {
            if (hit.collider.tag == "wall" &&  stateOfEntity.entity ==StateOfEntity.EntityState.Destroy)
            {
                DestroyAgent();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "wall" && stateOfEntity.entity ==StateOfEntity.EntityState.Destroy)
        {
            DestroyAgent();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "wall" && stateOfEntity.entity ==StateOfEntity.EntityState.Destroy)
        {
            DestroyAgent();
        }
    }

    public void RotateAgent()
    {
        if(Vector3.Angle(transform.forward, -dirProjection.normalized)>1)
        {
         
            float angle =   Vector3.SignedAngle(transform.forward,-dirProjection.normalized, Vector3.up);
           transform.eulerAngles =  new Vector3(0, angle,0);
        }
    }
}

