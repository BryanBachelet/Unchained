using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashingFeedback : MonoBehaviour
{
    public int numberTransformation ;
    public GameObject feedbackSecondTransformation;
    
    
    private GameObject fd;

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
        if(!activeTransformation)
        {
            numberTransformation++;
            if(numberTransformation==2)
            {
             fd =Instantiate(feedbackSecondTransformation, transform.position,Quaternion.Euler( -90,0,0));       
            }
            activeTransformation =true; 
        }       
     
   }
}
