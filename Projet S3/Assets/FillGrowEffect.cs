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
<<<<<<< HEAD

    float currentTime;

    public Color col1;
    public Color col2;
    // Start is called before the first frame update
    void Start()
    {
        myRT = gameObject.GetComponent<RectTransform>();
        timeToMax = timeToPlayAnim / 2;    
=======
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
>>>>>>> origin/BryanWork2
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if(isPlayingAnim)
        {
            VfxHit();
=======
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
          
>>>>>>> origin/BryanWork2
        }
    }

    public void VfxHit()
    {
        if (Time.fixedTime % .5 < .2)
        {
<<<<<<< HEAD
            gameObject.GetComponent<Image>().color = col1;
        }
        else
        {
            gameObject.GetComponent<Image>().color = col2;
        }
        currentTime += Time.deltaTime;
        if(currentTime < timeToMax)
=======
            image.color = baseColor;
        }
        else
        {
            image.color = increaseColor;
        }
        currentTime += Time.deltaTime;
        if (currentTime < timeToMax)
>>>>>>> origin/BryanWork2
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
<<<<<<< HEAD
        if(currentTime >= timeToPlayAnim)
=======
        if (currentTime >= timeToPlayAnim)
>>>>>>> origin/BryanWork2
        {
            isPlayingAnim = false;
            currentTime = 0;
            currentScaleX = 1;
            currentScaleY = 1;
<<<<<<< HEAD
            gameObject.GetComponent<Image>().color = col1;
=======
            gameObject.GetComponent<Image>().color = baseColor;
>>>>>>> origin/BryanWork2
        }
        //myRT.rect.Set(myRT.rect.x, myRT.rect.y, currentScaleX, -currentScaleY);
        myRT.localScale = new Vector3(currentScaleX, currentScaleY, 0);
    }
<<<<<<< HEAD
=======

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

>>>>>>> origin/BryanWork2
}
