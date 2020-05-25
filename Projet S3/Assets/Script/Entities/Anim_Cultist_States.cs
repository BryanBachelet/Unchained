using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Anim_Cultist_States : MonoBehaviour
{
    public enum AnimCultistState { Spwan_Run, Spwan_Jump, Spawn_Landing, Run, Projection_Choc ,Projection_FallAir, Projection_FrontFloor, Projection_BackFloor,Entrave_Walk,Entrave_Idle, Entrave_Repousse, Invocation_Walk,Invocation_Idle,Invocation_Fin }

    public AnimCultistState animCultist =  AnimCultistState.Spwan_Run;

    private StateOfEntity stateOfEntity;

    private  Animator animator;

    private bool activeOnetime;

    public RuntimeAnimatorController[] controler;

    // Start is called before the first frame update
    void Start()
    {
        stateOfEntity = GetComponentInParent<StateOfEntity>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = controler[Random.Range(0,3)];
        animator.Play("Anim_Cultist_Run");
        animCultist = AnimCultistState.Run;
    }

    // Update is called once per frame
    void Update()
    {
       switch (animCultist)
       {
           case (AnimCultistState.Run):
           
               animator.Play("Anim_Cultist_Run");
              
           break;
           case (AnimCultistState.Invocation_Idle):
            
            animator.SetBool("Invocation", true);
          
           break;
        case (AnimCultistState.Projection_FallAir):
            
            if(!activeOnetime)
            {
                animator.Play("Anim_Cultist_Percute_Choc");
                activeOnetime = true;
            }
        break;
        case (AnimCultistState.Projection_BackFloor):
            
            if(!activeOnetime)
            {
                animator.Play("Anim_Cultist_Percute_HitGroundV1");
                activeOnetime = true;
            }
        break;
         case (AnimCultistState.Entrave_Idle):
            
            if(!activeOnetime)
            {
                animator.Play("Anim_Cultist_AttackChain_Entree");
                
                activeOnetime = true;
            }
        break;
        
        
        
       }
    }

    public void StopAnim()
    {
        animator.enabled =false;
    }

    public void StartAnim()
    {
        animator.enabled =true;
    }

    public void ChangeAnimState(AnimCultistState animToChange)
    {
        if(animCultist != animToChange)
        {
            animCultist =  animToChange;
            activeOnetime = false;
        }
            
    }

}

