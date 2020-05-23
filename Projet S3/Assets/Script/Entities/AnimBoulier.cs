using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBoulier : MonoBehaviour
{
    public enum StateColoss {Idle, Preparation, Charge, Grap, Jet ,Projection}

    StateColoss colossAnim = StateColoss.Idle ; 
    public Animator controler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState( StateColoss state)
    {
        switch(state)
        {
            case(StateColoss.Idle) :
            
            controler.Play("Idle");

            break;
            case (StateColoss.Preparation) :

            controler.Play("FocusPlayer");

            break;
            case(StateColoss.Charge) :

            controler.Play("Charge");

            break;
            case(StateColoss.Grap) :

            controler.Play("AttrapePlayer");
            
            break;
            case(StateColoss.Jet) :

            controler.Play("TossPlayer");
            
            break;
            case(StateColoss.Projection) :
            
            controler.Play("Anim_Cultist_Percute_ChocV2");

            break;
        }
    }
}
