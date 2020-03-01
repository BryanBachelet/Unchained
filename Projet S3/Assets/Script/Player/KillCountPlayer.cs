using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillCountPlayer : MonoBehaviour
{
    public int[] arrayOfKill = new int[0];
    public float timerOfKillCount = 5;
    public float count;
    public Text killCountUI;
    public float speedofDecreseas = 7;
    public static float killCount;

    [HideInInspector] public bool activeDecrease;

    private static float timerOfKill;
    private static StepOfPlayerStates playerStates;

    public void Awake()
    {
        killCount = 0;
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
            DecreaseKillCount();
            if (killCount < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
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
