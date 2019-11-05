using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public GameObject inst;
    public GameObject target;
    public float spawnTime;
    private float compteur;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (compteur > spawnTime)
        {
            GameObject newEnn = Instantiate(inst, transform.position, transform.rotation);
            newEnn.GetComponent<EnnemiBehavior>().target = target;
            compteur = 0;
        }
        else
        {
            compteur += Time.deltaTime;
        }

    }
}
