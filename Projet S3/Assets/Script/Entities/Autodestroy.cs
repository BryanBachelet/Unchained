using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    public float tpsDestroy = 1000;
    public float tpsEcoule;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tpsEcoule += Time.deltaTime;
        
        if (tpsEcoule >= tpsDestroy)
        {
            Destroy(gameObject);
        }
    }
}
