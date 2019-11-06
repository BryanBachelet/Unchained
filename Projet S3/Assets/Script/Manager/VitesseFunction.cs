using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitesseFunction : MonoBehaviour
{
    public float currentLv;
    public float currentFloat;
    public Text myUIText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myUIText.text = currentLv.ToString() + "," + (currentFloat * 100).ToString("F0");
        if(currentFloat > 0)
        {

            currentFloat -= Time.deltaTime / 15;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            vitesseChange(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            vitesseChange(false);
        }
    }

    public void vitesseChange(bool isAdd)
    {
        if(isAdd)
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
