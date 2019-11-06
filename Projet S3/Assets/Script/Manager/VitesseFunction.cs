using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitesseFunction : MonoBehaviour
{
    public static float currentLv;
    public static float currentFloat;
    public Text myUIText;

    public float currentlvl;
    public float currentPercent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (myUIText != null)
        {
            myUIText.text = currentLv.ToString() + "," + (currentFloat * 100).ToString("F0");
        }
        if (currentFloat > 0)
        {

            currentFloat -= Time.deltaTime / 15;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            VitesseChange(true);
        }
        currentlvl = currentLv;
        currentPercent = currentFloat;
    }

    public static float RatioAugmented(float ratio)
    {

        return ratio * currentLv;

    }

    public static void VitesseChange(bool isAdd)
    {
        if (isAdd)
        {
            currentFloat += 1 - (currentLv - 1) / currentLv;
            if (currentFloat >= 1)
            {
                currentLv++;
                currentFloat = 0;
            }
        }
        else
        {
            if (currentFloat <= 0)
            {
                currentLv--;
                currentFloat = 0.99f;
            }
            currentFloat -= 1 - (currentLv - 1) / currentLv;
        }
    }
}
