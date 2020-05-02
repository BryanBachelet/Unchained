using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashingFeedback : MonoBehaviour
{
    public int numberTransformation ;
    public GameObject FirstVfxFeedback;
    public GameObject feedbackSecondTransformation;
    
    
    private GameObject fd;
    private float duration;

    public void Start()
    {
      
    }

   void Update()
    {
           if(fd != null)
        {
        fd.transform.position = transform.position; 
        }
    }
   

  public void ActiveFeedback()
   {
        bool activeTransformation =false;
        GetDuration();
        if(!activeTransformation)
        {
            numberTransformation++;
            if(numberTransformation == 1 )
            {
                Debug.Log("Instantiate"); 
                fd =Instantiate(FirstVfxFeedback, transform.position+Vector3.up,Quaternion.Euler( 0,0,0)); 
                fd.GetComponent<MagicalFX.FX_LifeTime>().LifeTime = duration;
            }
            if(numberTransformation==2)
            {
                fd =Instantiate(feedbackSecondTransformation, transform.position,Quaternion.Euler( -90,0,0));   
                fd.GetComponent<MagicalFX.FX_LifeTime>().LifeTime = duration;   
            }
            activeTransformation =true; 
        }       
     
   }
    public void GetDuration()
    {
         duration = Camera.main.GetComponent<CinematicCam>().durationTotal;
    }
}

