using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanCamera : MonoBehaviour
{
    public int i = 3;

    public TestStuck[] activeGameObject = new TestStuck[1];


    public void Addition()
    {
        Debug.Log(activeGameObject[0].numberOfTarget);
        Debug.Log(i * 2);
    }
    [System.Serializable]
    public struct TestStuck
    {
        public GameObject myTarget;
        public int numberOfTarget;
    };
}
