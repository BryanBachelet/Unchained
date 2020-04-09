using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnVfx : MonoBehaviour
{
    public float freqSpawn;
    float tempsEcouleSpawn;
    public GameObject vfxToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        tempsEcouleSpawn = freqSpawn - 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        tempsEcouleSpawn += Time.deltaTime;
        if(tempsEcouleSpawn >= freqSpawn)
        {
            tempsEcouleSpawn = 0;
            Instantiate(vfxToSpawn, transform.position, transform.rotation);
        }
    }
}
