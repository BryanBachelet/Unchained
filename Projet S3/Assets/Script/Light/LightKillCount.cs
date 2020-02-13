using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightKillCount : MonoBehaviour
{
    public LightParameter[] lights = new LightParameter[0];
    public int i = 1;
    public float lightAdvancement;
    public float nextLightAdvancement;
    private Light lightDir;
    // Start is called before the first frame update
    void Start()
    {
        lightDir = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((KillCountPlayer.killCount.Count - lights[i - 1].killCapte)/(lights[i].killCapte - lights[i - 1].killCapte));
        nextLightAdvancement = (KillCountPlayer.killCount.Count - lights[i - 1].killCapte) / (lights[i].killCapte - lights[i - 1].killCapte);
        if (KillCountPlayer.killCount.Count <= lights[i - 1].killCapte)
        {
            nextLightAdvancement = 0;
        }


        lightAdvancement = Mathf.Lerp(lightAdvancement, nextLightAdvancement, Time.deltaTime);
        lightDir.color = Color.Lerp(lights[i - 1].lightColor, lights[i].lightColor, lightAdvancement);
        if (KillCountPlayer.killCount.Count >= lights[i].killCapte && i < lights.Length-1)
        {
            i++;
            lightAdvancement = 0;
        }
    }

    [System.Serializable]
    public struct LightParameter
    {
        public Color lightColor;
        public float killCapte;
    }
}
