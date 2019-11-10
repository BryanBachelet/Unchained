using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumber : MonoBehaviour
{
    public InputManager inputManager;
    public int playerNumber;
     public int manetteNumber;

    public void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        if (playerNumber == 1 && inputManager.controllerOne != 0)
        {
            manetteNumber = inputManager.controllerOne;
        }
        if (playerNumber == 2 && inputManager.controllerTwo != 0)
        {
            manetteNumber = inputManager.controllerTwo;
        }
    }

}
