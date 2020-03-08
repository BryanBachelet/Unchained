using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{

    public Transform currentResetPosition;
    private KillCountPlayer countPlayer;
    private EnnemiStock ennemiStock;
    private PlayerMoveAlone playerMove;
    private MouseScope mouseScope;

    // Start is called before the first frame update
    void Start()
    {
        countPlayer = GetComponentInChildren<KillCountPlayer>();
        ennemiStock = GetComponent<EnnemiStock>();
        playerMove = GetComponent<PlayerMoveAlone>();
        mouseScope = GetComponent<MouseScope>();
      
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
        countPlayer.ResetTiming();
        playerMove.currentPowerOfProjection = 0;
        transform.position = currentResetPosition.position;
      
    }
}
