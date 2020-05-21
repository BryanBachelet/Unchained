using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    public bool ok;

    public float timefloat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void StopTime()
    {
        Time.timeScale = 0.5f; 
    }
    public static void RestartTime()
    {
        Time.timeScale = 1f;
    }
}
