using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashingTransSimple : MonoBehaviour 
{
    public int i;
    public float timing;
    public int numberToAim;

    public Text text;
    public CamMouvement camMouvement;

    private float compteur;
    private TransformationAgent agentTransfo;
    private ResetPlayer resetPlayerScript;
    private bool activeExplode;
    private bool activationTransformation;
    void Start()
    {
        resetPlayerScript = GetComponent<ResetPlayer>();
        agentTransfo = GetComponent<TransformationAgent>();
    }

    void Update()
    {
        if (camMouvement.i >= camMouvement.cams.Count)
        {
            text.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                i++;
            }
            if (compteur > timing)
            {
                if (i > numberToAim)
                {

                    if (!activeExplode)
                    {
                        text.gameObject.SetActive(false);
                        agentTransfo.ActiveExplosion();
                        activeExplode = true;
                    }
                    Physics.IgnoreLayerCollision(9, 9, false);
                    Physics.IgnoreLayerCollision(9, 10, false);
                    if (compteur > timing + 0.7f)
                    {
                        transform.GetComponent<PlayerMoveAlone>().enabled = true;

                        StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;
                    }

                }
                else
                {
                    Physics.IgnoreLayerCollision(9, 9, false);
                    Physics.IgnoreLayerCollision(9, 10, false);
                    text.gameObject.SetActive(false);
                    resetPlayerScript.ResetFonction(true);
                    Debug.Log("Lose");
                }
            }
            else
            {
                if (!activationTransformation)
                {
                    agentTransfo.startTranformationAnim(timing);
                    activationTransformation = true;
                }
            }

            compteur += Time.deltaTime;
        }

    }
}
