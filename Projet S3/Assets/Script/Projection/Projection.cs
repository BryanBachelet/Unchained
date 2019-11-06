using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    private bool playerHere;
    private GameObject playerContact;
    private PlayerNumber playerNumber;
    private PlayerState playerState;
    private Opportunity opportunity;



    [Header("Caractéristiques de la projection")]
    public float speedOfFly;
    public float timeToFlight;
    [Range(0, 1)] public float opportunityWindow;
    public float ratioPerLevelOfPower;

    void Start()
    {
        playerNumber = GetComponentInParent<PlayerNumber>();
        playerState = GetComponentInParent<PlayerState>();
        opportunity = GetComponent<Opportunity>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        Jet();
    }


    public void ChangeState()
    {
        Vector3 dir = GetAimDirection();
        if (dir != Vector3.zero)
        {
            PlayerCommands.ChangePlayerState(transform.parent.gameObject, PlayerState.StateOfPlayer.Jet);

        }
        else
        {
            PlayerCommands.ChangePlayerState(transform.parent.gameObject, PlayerState.StateOfPlayer.Free);

        }
    }

    public void Jet()
    {
        if (playerHere && playerState.playerState == PlayerState.StateOfPlayer.Jet)
        {
            PlayerCommands.ChangePlayerState(PlayerCommands.OtherPlayer(transform.parent.gameObject), PlayerState.StateOfPlayer.Free);
            GetFly();
            playerHere = false;
        }
    }

    public Vector3 GetAimDirection()
    {
        Vector3 dir = Vector3.zero;
        float horizontal = Input.GetAxis("AimHorizontal" + playerNumber.playerNumber.ToString());
        float vertical = Input.GetAxis("AimVertical" + playerNumber.playerNumber.ToString());
        dir = new Vector3(horizontal, 0, vertical).normalized;
        return dir;
    }

    public void GetFly()
    {
        if (!playerContact.GetComponent<Fly>())
        {
            Fly flyScript = playerContact.AddComponent<Fly>();
            flyScript.direction = GetAimDirection();
            flyScript.timeToFlight = timeToFlight;
            flyScript.speedOfFight = speedOfFly + VitesseFunction.RatioAugmented(ratioPerLevelOfPower);
            flyScript.opportunityWindow = opportunityWindow;
        }
    }

    private void OnContact(Collider other)
    {
        if (other.tag == "Player" && playerState.playerState == PlayerState.StateOfPlayer.Jet)
        {
            playerHere = true;
            playerContact = other.gameObject;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnContact(other);
    }
    private void OnTriggerStay(Collider other)
    {
        OnContact(other);
    }

}
