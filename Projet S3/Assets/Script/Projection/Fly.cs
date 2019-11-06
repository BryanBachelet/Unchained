using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float timeToFlight;
    [HideInInspector] public float speedOfFight;
    [HideInInspector] public float opportunityWindow;

    public float compteurOfFlyTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (compteurOfFlyTime > timeToFlight)
        {
            Destroy(this);
        }
        else
        {
            transform.position += direction * speedOfFight * Time.deltaTime;
            compteurOfFlyTime += Time.deltaTime;
        }
    }
}
