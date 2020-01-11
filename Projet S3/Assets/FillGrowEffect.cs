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
    public bool secondVersion;
    float currentTime;

    public Image image;
    public Color baseColor;
    public Color increaseColor;
    //Variable Second Version
    public Slider slider;
    public float timeOfFeedback = 0.5f;
    public float gainDeScale;
    private float _timeOfFeedback;
    private float sliderValue;
    private bool coloration;
    private Vector3 startFillPosition;
    Rect rect;
    // Start is called before the first frame update
    void Start()
    {
        startFillPosition = transform.position;
        myRT = gameObject.GetComponent<RectTransform>();
        rect = myRT.rect;
        image = GetComponent<Image>();
        timeToMax = timeToPlayAnim / 2;
        slider = GetComponentInParent<Slider>();
        sliderValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (!secondVersion)
        {
            if (isPlayingAnim)
            {
                VfxHit();
            }
        }
        if (secondVersion)
        {
            ColorIncrease();
            ScaleIncrease();
          
        }
    }

    public void VfxHit()
    {
        if (Time.fixedTime % .5 < .2)
        {
            image.color = baseColor;
        }
        else
        {
            image.color = increaseColor;
        }
        currentTime += Time.deltaTime;
        if (currentTime < timeToMax)
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
        if (currentTime >= timeToPlayAnim)
        {
            isPlayingAnim = false;
            currentTime = 0;
            currentScaleX = 1;
            currentScaleY = 1;
            gameObject.GetComponent<Image>().color = baseColor;
        }
        //myRT.rect.Set(myRT.rect.x, myRT.rect.y, currentScaleX, -currentScaleY);
        myRT.localScale = new Vector3(currentScaleX, currentScaleY, 0);
    }

    public void ScaleIncrease()
    {
        if (!coloration)
        {
            if (sliderValue < slider.value)
            {
                coloration = true;
                sliderValue = slider.value;
           myRT.sizeDelta = new Vector2(myRT.sizeDelta.x, gainDeScale);
            }
            else
            {
                sliderValue = slider.value;
                myRT.sizeDelta = new Vector2(myRT.sizeDelta.x, 0    );
             
            }
        }
        else
        {
        
          
            if (_timeOfFeedback < timeOfFeedback)
            {
                _timeOfFeedback += Time.deltaTime;
            }
            else
            {
                _timeOfFeedback = 0;
                coloration = false;
            }
        }
    }
    public void ColorIncrease()
    {
        if (!coloration)
        {
            if (sliderValue < slider.value)
            {
                coloration = true;
                sliderValue = slider.value;
            }
            else
            {
                sliderValue = slider.value;
                image.color = baseColor;
            
            }

        }
        else
        {
            image.color = increaseColor;
            if (_timeOfFeedback < timeOfFeedback)
            {
                _timeOfFeedback += Time.deltaTime;
            }
            else
            {
                _timeOfFeedback = 0;
                coloration = false;
            }
        }
    }

}
