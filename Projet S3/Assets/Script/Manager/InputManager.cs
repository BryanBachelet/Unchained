using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public string[] inputNumber;
    public int controllerOne;
    public int controllerTwo;

    void Awake()
    {
        this.inputNumber = Input.GetJoystickNames();
        for (int i = 0; i < inputNumber.Length; i++)
        {
            if (inputNumber[i] != "")
            {
                controllerOne = i + 1;
                break;
            }
        }
        for (int i = 0; i < inputNumber.Length; i++)
        {
            if (inputNumber[i] != "" && i > controllerOne - 1)
            {
                controllerTwo = i + 1;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
