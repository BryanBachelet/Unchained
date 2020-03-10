using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashingTrans : MonoBehaviour
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
                    Debug.Log("Win");
                    if (!activeExplode)
                    {
                        text.gameObject.SetActive(false);
                        agentTransfo.ExploseAgent();
                        activeExplode = true;
                    }
                    if (compteur > timing + 1)
                    {
                        StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;
                    }

                }
                else
                {
                    text.gameObject.SetActive(false);
                    resetPlayerScript.ResetFonction(true);
                    Debug.Log("Lose");
                }
            }
            else
            {
                agentTransfo.startTranformationAnim(timing);
            }
                compteur += Time.deltaTime;
        }

    }
}
