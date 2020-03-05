using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOfGames : MonoBehaviour
{
    public enum StateOfGame { DefaultPlayable, Cinematic, Transformation };
    public static  StateOfGame currentState = StateOfGame.Cinematic;
}
