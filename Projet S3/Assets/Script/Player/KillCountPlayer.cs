using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountPlayer : MonoBehaviour
{
    public int[] arrayOfKill = new int[0];
    public static List <float> killCount = new List<float>();
    public float timerOfKillCount = 5;
    public int count;
    private static float timerOfKill;
    public Text killCountUI;

    public void Start()
    {
        timerOfKill = timerOfKillCount;
    }

    void Update()
    {
        count = killCount.Count;
        killCountUI.text = count.ToString("F0");
        DecreaseKillCount();
    }

    public void DecreaseKillCount()
    {
        for (int i = 0; i < killCount.Count; i++)
        {
            killCount[i] -= Time.deltaTime;
            if (killCount[i] < 0)
            {
                killCount.RemoveAt(i);
            }
        }
    }

    public static void AddList()
    {
        killCount.Add(timerOfKill);
    }

    public static void CleanArray()
    {
        killCount.Clear();
    }
}
