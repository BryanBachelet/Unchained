using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPlayer : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        int rnd = Random.Range(0, 2);
        if(rnd == 0)
        {
            target = PlayerCommands.player1.transform;
        }
        if (rnd == 1)
        {
            target = PlayerCommands.player2.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, 2 * Time.deltaTime);
    }
}
