using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimState : MonoBehaviour
{
    public enum PlayerStateAnim {Idle,Shoot, Rotation, Projection,EntraveStart,EntraveFinish}
    public PlayerStateAnim playerCurrentState;

    public Animator animator;
    public bool right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ChangeStateAnim(PlayerStateAnim currentState)
    {
        if(playerCurrentState != currentState)
        {
            if(currentState == PlayerStateAnim.Idle)
            { 
                if(playerCurrentState == PlayerStateAnim.Projection )
                {
                    animator.Play("Anim_State_Chaine_Projection releve");
                }
                else
                {

                    animator.Play("Anim_State_Chaine_Idle");
                }
            }
            if(currentState == PlayerStateAnim.Shoot)
            {
                if(right)
                {
                    animator.Play("Anim_State_Chaine_LancéDroit");
                }
                else
                {
                    animator.Play("Anim_State_Chaine_LancéGauche");
                }
            }
            if(currentState == PlayerStateAnim.Rotation)
            {
                animator.Play("Anim_State_Chaine_StartRotate");
            }
            if(currentState == PlayerStateAnim.Projection)
            {
                animator.Play("Anim_State_Chaine_ProjectionStart");
            }
            if(currentState == PlayerStateAnim.EntraveStart)
            {
               animator.Play("Anim_State_Chaine_Entrave_Lutte");
            }
            if(currentState == PlayerStateAnim.EntraveFinish)
            {
                animator.Play("Anim_State_Chaine_Entrave_breakEntrave");
                
            }
            playerCurrentState = currentState;
        }
    }
}
