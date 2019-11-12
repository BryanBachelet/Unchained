using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMouv : MonoBehaviour
{

    public float destroyAfter;
    private float tempsEcouleVie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempsEcouleVie += Time.deltaTime;

        if(tempsEcouleVie > destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}
