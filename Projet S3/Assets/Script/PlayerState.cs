using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum StateOfPlayer { Free, Jet, Fly }
    public  StateOfPlayer playerState = StateOfPlayer.Free;
    public enum OpportunityState { In, Out }
    public OpportunityState opportunityState = OpportunityState.Out;
}
