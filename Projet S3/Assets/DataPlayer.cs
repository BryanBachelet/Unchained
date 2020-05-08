using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataPlayer : MonoBehaviour
{
    
    static public float tempsEcoulePartie;
    static public int entityHit;
    static public int entityKill;
    static public float aimPercent;
    static public float tempsAccroche;
    static public int entityKillBySlam;
    static public float tempsEcouleMashing;

    static float afficheTempsEcoulePartie;
    static float afficheEntityHit;
    static float afficheKill;
    static float affichePercentAim;

    static Text dataText;
    public Text goText;
    public Text scoringText;
    public Text scoreMultiplyText;
    static float scoreToGo;
    public float scoreCurrent;
    static bool actualiseScore = false;
    static public bool isCountingTime = false;

    static public bool isGivingData = false;
    public int nbHit;
    public int nbKill;
    static public int nbShot;
    static public int nbShotHit;
    static float tempsEcouleWin;

    static List<GameObject> uiScoreToAdd = new List<GameObject>();
    static List<int> intScoreToAdd = new List<int>();
    static GameObject uiTextAdd;
    public GameObject uiTextAddTaken;
    public GameObject goScoreUIContainer;
    static GameObject goScoreUiContainerSttic;
    public Vector3 posToStopMoving;
    bool checkAddScore = false;
    public static int comboMultiplier = 1;
    public static int killCountEnnemi = 0;
    static public float tempsEcouleCombo;
    public float tempsAvantResetCombo = 3;
    // Start is called before the first frame update
    void Start()
    {
        if(uiTextAddTaken != null)
        {
            uiTextAdd = uiTextAddTaken;
        }
        if(goScoreUIContainer != null)
        {
            goScoreUiContainerSttic = goScoreUIContainer;
        }
        tempsEcoulePartie = 0;
        entityHit = 0;
        entityKill = 0;
        aimPercent = 0;
        tempsAccroche = 0;
        entityKillBySlam = 0;
        tempsEcouleMashing = 0;
        dataText = goText;
    }

    // Update is called once per frame
    void Update()
    {
        nbHit = entityHit;
        nbKill = entityKill;
        if(StateOfGames.currentState == StateOfGames.StateOfGame.DefaultPlayable)
        {
            tempsEcouleCombo += Time.deltaTime;
            if(tempsEcouleCombo > tempsAvantResetCombo)
            {
                comboMultiplier = 1;
            }
            else
            {
                comboMultiplier = 1 + killCountEnnemi / 40;
                Debug.Log(comboMultiplier + "MON CUL LE SCORE D ECOMBO IL EST PETER");
            }
        }
        if(scoringText != null)
        {
            if(actualiseScore)
            {
                if(Vector3.Distance(uiScoreToAdd[0].transform.position, posToStopMoving) > 1f)
                {
                    uiScoreToAdd[0].transform.position = Vector3.Lerp(uiScoreToAdd[0].transform.position, posToStopMoving, (10f + uiScoreToAdd.Count/3) * Time.deltaTime);
                    for(int i = 1; i < uiScoreToAdd.Count; i++)
                    {
                        uiScoreToAdd[i].transform.position = Vector3.Lerp(uiScoreToAdd[i].transform.position, new Vector3(600 + (i - 1) * 12, 1000 + (i - 1) * 12, 1), (10f + uiScoreToAdd.Count / 3) * Time.deltaTime);
                    }
                }
                else
                {
                    if(!checkAddScore)
                    {
                        scoreToGo += intScoreToAdd[0];
                        checkAddScore = true;
                    }
                    if(checkAddScore)
                    {
                        scoreCurrent = Mathf.Lerp(scoreCurrent, scoreToGo, 15 * Time.deltaTime);
                        if (scoreCurrent >= scoreToGo - 5)
                        {
                            scoreCurrent = scoreToGo;
                            Destroy(uiScoreToAdd[0]);
                            uiScoreToAdd.RemoveAt(0);
                            intScoreToAdd.RemoveAt(0);
                            checkAddScore = false;

                            for (int i = 0; i < 10; i++)
                            {
                                float a = 100 - i * 10;
                                uiScoreToAdd[i].GetComponent<Text>().color = new Vector4(1, 1, 1, a / 100);
                            }
                            if(uiScoreToAdd.Count <= 0)
                            {
                                actualiseScore = false;
                            }

                        }
                    }

                }

            }
            scoringText.text = "Score : " + scoreCurrent;
        }

        if(scoreMultiplyText != null) //)
        {
            scoreMultiplyText.text = "x " + (1 + ((comboMultiplier * 2) / 10));
        }
        if(isCountingTime)
        {
            tempsEcoulePartie += Time.deltaTime;
        }
        if(isGivingData)
        {
            GiveData();
        }
    }

    static public void GiveData()
    {
        isCountingTime = false;
        tempsEcouleWin += 0.1f * Time.deltaTime;
        afficheTempsEcoulePartie = Mathf.Lerp(afficheTempsEcoulePartie, tempsEcoulePartie, tempsEcouleWin);
        afficheEntityHit = Mathf.Lerp(afficheEntityHit, entityHit, tempsEcouleWin);
        afficheKill = Mathf.Lerp(afficheKill, entityKill, tempsEcouleWin);
        affichePercentAim = Mathf.Lerp(affichePercentAim,nbShotHit * 100 / nbShot,tempsEcouleWin);
        dataText.text = "Temps : " + (afficheTempsEcoulePartie / 60).ToString("F2") + "min" + "\nEntitées frappées : " + afficheEntityHit + "\nEntitées tuées : " + afficheKill + "\nAiming : " + affichePercentAim + " %" + "\nRythmeKill : " + ManageEntity.PercentKill + " %";
        //dataText.text = "Temps : " + tempsEcoulePartie / 60 + "min" + "\nEntitées frappées : " + entityHit + "\nEntitées tuées : " + entityKill;
    }

    static public void shotCount()
    {
        nbShot++;
    }

    static public void ChangeScore(int scoreToAdd)
    {
        if(uiTextAdd != null)
        {
            GameObject uiInstantiate = Instantiate(uiTextAdd, new Vector3(600 + uiScoreToAdd.Count * 12, 1000 + uiScoreToAdd.Count * 12, 1), Quaternion.identity, goScoreUiContainerSttic.transform); //253.81f --> X, 31.88f --> Y
            uiScoreToAdd.Add(uiInstantiate);
            uiInstantiate.GetComponent<Text>().text = "+" + scoreToAdd;
            float vCanalInHSVColor = (100 - uiScoreToAdd.Count * 10);
            Debug.Log(vCanalInHSVColor);
            if (vCanalInHSVColor < 0) vCanalInHSVColor = 0;
            uiInstantiate.GetComponent<Text>().color = new Vector4(1, 1, 1, vCanalInHSVColor / 100);
        }

        intScoreToAdd.Add(scoreToAdd * (1 + ((comboMultiplier * 2) / 10)));
        actualiseScore = true;
    }

}
