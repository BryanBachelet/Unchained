using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float timeDestroy = 0;
    float timeEcouleDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeEcouleDeath += Time.deltaTime;
        if(timeEcouleDeath >= timeDestroy)
        {
            Destroy(gameObject);
        }
    }
}
