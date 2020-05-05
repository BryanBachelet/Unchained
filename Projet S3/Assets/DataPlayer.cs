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
    static public bool isCountingTime = false;

    static public bool isGivingData = false;
    public int nbHit;
    public int nbKill;
    static public int nbShot;
    static public int nbShotHit;
    static float tempsEcouleWin;

    // Start is called before the first frame update
    void Start()
    {
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
}
