using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOfGames : MonoBehaviour
{
    public enum StateOfGame { DefaultPlayable, Cinematic, Transformation };
    public static StateOfGame currentState = StateOfGame.Cinematic;
    public StateOfGame currentStateOfGame;

    public enum PhaseOfDefaultPlayable { Phase1, Phase2,Phase3 };
    public static PhaseOfDefaultPlayable currentPhase = PhaseOfDefaultPlayable.Phase1;
    public PhaseOfDefaultPlayable currentPhaseOfDefaultPlayable;

#if UNITY_EDITOR
    private void Update()
    {
        currentStateOfGame = currentState;
        currentPhaseOfDefaultPlayable = currentPhase;
    }
#endif
}


