using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionOfPlayer : MonoBehaviour
{
    public int sizeOfArray;
    public float[] statesOfExpulsionStrengh = new float[0];
    public float[] statesRotation = new float[0];

    private PlayerMoveAlone player;
    private RotationPlayer rotationPlayer;
    private int currentStates;
    private int currentSizeOfArray;

    private void Start()
    {
        player = GetComponent<PlayerMoveAlone>();
        rotationPlayer = GetComponent<RotationPlayer>();
    }

  


    [ContextMenu("ChangeArrayRange")]
    public void ActiveArray()
    {
        if (sizeOfArray != currentSizeOfArray)
        {
            currentSizeOfArray = sizeOfArray;
            statesOfExpulsionStrengh = new float[sizeOfArray];
            statesRotation = new float[sizeOfArray];
        }
    }

    public void ChangeState(bool rotate)
    {
        if (currentStates < sizeOfArray - 1)
        {
            currentStates++;
            StrenghExpulsionAugmentation();
            if (rotate)
            {
                IncreaseRotationSpeed();
            }
        }
    }

    private void StrenghExpulsionAugmentation()
    {
        player.expulsionStrengh = statesOfExpulsionStrengh[currentStates];
    }
    private void IncreaseRotationSpeed()
    {
        rotationPlayer.angleSpeed = statesRotation[currentStates];
    }




}
