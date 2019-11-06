using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumber : MonoBehaviour
{
    private InputManager inputManager;
    public int playerNumber;
    [HideInInspector] public int manetteNumber;

    public void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        if (playerNumber == 1)
        {
            manetteNumber = inputManager.controllerOne;
        }
        if (playerNumber == 2)
        {
            manetteNumber = inputManager.controllerTwo;
        }
    }

}
