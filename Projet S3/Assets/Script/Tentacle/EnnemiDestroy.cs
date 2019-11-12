using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiDestroy : MonoBehaviour
{
    public bool isDestroying;
    public float timerToDestro = 2;
    private float compteur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroying)
        {
            if (compteur > timerToDestro)
            {
                Destroy(gameObject);
            }
            else
            {
                compteur += Time.deltaTime ;
            }
        }
        
    }
}
