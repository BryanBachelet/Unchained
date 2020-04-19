using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{   
    public LayerMask fallTriggerLayer; 
   
   public LayerMask deadMaskLayer;
    public bool activeFall; 
    private MouseScope mouse;
    private PlayerMoveAlone playerMove;

    private Rigidbody rigidPlayer;

    private KillCountPlayer killCountPlayer;

private Vector3 dirProj;
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMoveAlone>();
        mouse = GetComponent<MouseScope>();
        killCountPlayer =  GetComponentInChildren<KillCountPlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(activeFall&& mouse.instanceBullet == null )
        {
            DeactiveController();
            dirProj = new Vector3(dirProj.x,0,dirProj.z);
            rigidPlayer.velocity += new Vector3(0,-10*Time.deltaTime,0); 
            rigidPlayer.velocity = dirProj.normalized * 25 + new Vector3(0, rigidPlayer.velocity.y,0);
        }
   
    }

    public void OnTriggerExit(Collider collider)
    {
      CollideWallActive(collider);
    }
     public void OnTriggerStay(Collider collider)
    {
        CollideWallActive(collider);
    }
   private void OnTriggerEnter(Collider collider)
    {
       CollideWallActive(collider);
    }


    public void  CollideWallActive(Collider collider)
    {
        if(collider.gameObject.layer == 13)
        {
        Debug.Log(collider.gameObject);

        }
     
        if(collider.gameObject.layer == 13 && !activeFall && StateAnim.state !=StateAnim.CurrentState.Rotate && mouse.instanceBullet == null )
        {
            //Debug.Break();
            dirProj= playerMove.DirProjection;
           
            rigidPlayer.velocity = Vector3.zero;
            activeFall = true;  

        }
        
        if(collider.gameObject.layer == 11   && activeFall && StateAnim.state !=StateAnim.CurrentState.Rotate  &&  mouse.instanceBullet == null )
        {
            killCountPlayer.ActiveDeathCondition();   
        }

    }

    public void DeactiveController()
    {
        
        mouse.enabled =false;
        playerMove.enabled = false;
    }

}
