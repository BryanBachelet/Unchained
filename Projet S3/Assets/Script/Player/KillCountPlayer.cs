using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCountPlayer : MonoBehaviour
{
    private static List <float> killCount = new List<float>();
    public float timerOfKillCount = 5;
    public int count;
    private static float timerOfKill;
    // Update is called once per frame

    public void Start()
    {
        timerOfKill = timerOfKillCount;
    }

    void Update()
    {
        count = killCount.Count;
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

}
