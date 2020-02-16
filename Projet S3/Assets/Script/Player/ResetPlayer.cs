using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{

    public Transform currentResetPosition;
    private EnnemiStock ennemiStock;
    private PlayerMoveAlone playerMove;
    private MouseScope mouseScope;
    private StepOfPlayerStates step;
    // Start is called before the first frame update
    void Start()
    {
        ennemiStock = GetComponent<EnnemiStock>();
        playerMove = GetComponent<PlayerMoveAlone>();
        mouseScope = GetComponent<MouseScope>();
        step = GetComponentInChildren<StepOfPlayerStates>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            ResetFonction();
        }
    }

    public void ResetFonction()
    {
        if (ennemiStock.ennemiStock != null)
        {
            ennemiStock.ResetPlayer();
        }
        mouseScope.DestroyBullet();
        step.ResetDefeatCondition();
        KillCountPlayer.CleanArray();
        playerMove.currentPowerOfProjection = 0;
        transform.position = currentResetPosition.position;
    }
}
