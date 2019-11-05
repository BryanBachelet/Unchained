using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum PlayerCurrentState { Libre, Jet, Vol, Stun, Attireur,Rotation,Slaming };
    public PlayerCurrentState currentState;
}
