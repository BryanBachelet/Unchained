using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillGrowEffect : MonoBehaviour
{
    public float toScaleX;
    public float toScaleY;
    float currentScaleX;
    float currentScaleY;
    float timeToMax;
    public float timeToPlayAnim;

    public bool isPlayingAnim;
    RectTransform myRT;

    float currentTime;

    public Color col1;
    public Color col2;
    // Start is called before the first frame update
    void Start()
    {
        myRT = gameObject.GetComponent<RectTransform>();
        timeToMax = timeToPlayAnim / 2;    
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayingAnim)
        {
            VfxHit();
        }
    }

    public void VfxHit()
    {
        if (Time.fixedTime % .5 < .2)
        {
            gameObject.GetComponent<Image>().color = col1;
        }
        else
        {
            gameObject.GetComponent<Image>().color = col2;
        }
        currentTime += Time.deltaTime;
        if(currentTime < timeToMax)
        {
            if (currentScaleX < toScaleX)
            {
                currentScaleX += Time.deltaTime;
            }
            if (currentScaleY < toScaleY)
            {
                currentScaleY += Time.deltaTime;
            }
        }
        else if (currentTime > timeToMax)
        {
            if (currentScaleX > 1)
            {
                currentScaleX -= Time.deltaTime;
            }
            if (currentScaleY > 1)
            {
                currentScaleY -= Time.deltaTime;
            }
        }
        if(currentTime >= timeToPlayAnim)
        {
            isPlayingAnim = false;
            currentTime = 0;
            currentScaleX = 1;
            currentScaleY = 1;
            gameObject.GetComponent<Image>().color = col1;
        }
        //myRT.rect.Set(myRT.rect.x, myRT.rect.y, currentScaleX, -currentScaleY);
        myRT.localScale = new Vector3(currentScaleX, currentScaleY, 0);
    }
}
