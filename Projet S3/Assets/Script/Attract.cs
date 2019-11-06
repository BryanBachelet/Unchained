using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{

    public float speedOfAttrack;
    public Transform otherPlayer;
    public bool trigSkill = false;
    public bool isArrived = true;
    public PlayerNumber playerNumber;



    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
    }

    // Update is called once per frame
    void Update()
    {
        float hit = Input.GetAxis("Attract" + playerNumber.playerNumber.ToString());
        if (hit != 0 && isArrived)
        {
            isArrived = false;
            PlayerCommands.ActiveOpportunityWindow(gameObject);
        }

        if (!isArrived)
        {
            AttractSkill();
            if (Vector3.Distance(transform.position, otherPlayer.position) < 1f)
            {
                isArrived = true;
            }
        }
    }

    public void AttractSkill()
    {
        transform.position = Vector3.MoveTowards(transform.position, otherPlayer.position, speedOfAttrack * Time.deltaTime);
    }
}
