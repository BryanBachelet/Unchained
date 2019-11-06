using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{

    public float speedOfAttrack;
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
        if (hit != 0 && PlayerCommands.CheckPlayerState(gameObject, PlayerState.StateOfPlayer.Free))
        {
            PlayerCommands.ChangePlayerState(gameObject, PlayerState.StateOfPlayer.Attract);
            PlayerCommands.ActiveOpportunityWindow(gameObject);
        }

        if (PlayerCommands.CheckPlayerState(gameObject, PlayerState.StateOfPlayer.Attract))
        {
            AttractSkill();
            
        }
    }

    public void AttractSkill()
    {
        GameObject otherPlayer = PlayerCommands.OtherPlayer(gameObject);
        Debug.Log(otherPlayer);
        transform.position = Vector3.MoveTowards(transform.position, otherPlayer.transform.position, speedOfAttrack * Time.deltaTime);
        if (Vector3.Distance(transform.position, otherPlayer.transform.position) < 1f)
        {
            PlayerCommands.ChangePlayerState(gameObject, PlayerState.StateOfPlayer.Free);
            
        }
    }
}
