using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering.PostProcessing;

public class KillCountPlayer : MonoBehaviour
{
    public int[] arrayOfKill = new int[0];
    public float timerOfKillCount = 5;
    public float count;
    public Text killCountUI;
    public float speedofDecreseas = 7;
    public static float killCount;

    public GameObject postProcesse;

    [HideInInspector] public bool activeDecrease;

    private static float timerOfKill;
    private static StepOfPlayerStates playerStates;

    private int frameDecreaseCondition;




    [FMODUnity.EventRef]
    public string Lose;
    private FMOD.Studio.EventInstance loseCondition;
    public float volume = 20;

    public void Awake()
    {
        killCount = 0;
        loseCondition = FMODUnity.RuntimeManager.CreateInstance(Lose);
        loseCondition.setVolume(volume);
    }
    public void Start()
    {
        timerOfKill = timerOfKillCount;
        playerStates = GetComponent<StepOfPlayerStates>();
    }

    void Update()
    {
        count = killCount;
        killCountUI.text = count.ToString("F0");
        if (activeDecrease)
        {
            if (frameDecreaseCondition == 0)
            {
                loseCondition.start();
                frameDecreaseCondition = 1;
            }
                postProcesse.GetComponent<PostProcessVolume>().enabled =true;
                DecreaseKillCount();

            if (killCount < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                loseCondition.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }else
        {
            frameDecreaseCondition = 0;
            postProcesse.GetComponent<PostProcessVolume>().enabled = false;
        }
    }

    public void DecreaseKillCount()
    {
        killCount -= speedofDecreseas * Time.deltaTime;
    }

    public static void AddList()
    {
        killCount++;
        playerStates.ResetTiming();
    }

    public static void CleanArray()
    {
        killCount = 0;
        playerStates.ResetTiming();
    }
}
