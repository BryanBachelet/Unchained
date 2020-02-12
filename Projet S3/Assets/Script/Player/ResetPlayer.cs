using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{

    public Transform currentResetPosition;
    private EnnemiStock ennemiStock;
    private PlayerMoveAlone playerMove;
 
    // Start is called before the first frame update
    void Start()
    {
        ennemiStock = GetComponent<EnnemiStock>();
        playerMove = GetComponent<PlayerMoveAlone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if (ennemiStock.ennemiStock != null)
            {
                ennemiStock.ResetPlayer();
                playerMove.currentPowerOfProjection = 0;
            }
            transform.position = currentResetPosition.position;
        }
    }
}
