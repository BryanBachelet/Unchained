using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    public float tpsDestroy = 1000;
    public float tpsEcoule;
    public bool isEntity = false;

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

        if(Vector3.Distance(transform.position, Vector3.zero) > 280 && isEntity)
        {
            gameObject.GetComponent<StateOfEntity>().entity = StateOfEntity.EntityState.Dead;
            gameObject.SetActive(false);
        }
    }
}
