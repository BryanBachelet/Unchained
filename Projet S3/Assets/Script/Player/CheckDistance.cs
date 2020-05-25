using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    public static bool activeCenter;

    public static Vector3 dir;
    
    public static Vector3 center = new Vector3(3.6f,0,35.1f);
    // Start is called before the first frame update
  public static void DistanceTransformation(GameObject player)
  {
     float distance = Vector3.Distance(player.transform.position,center);
     if(distance<100)
     {  
         activeCenter =true;
     }
     else
     {
         dir =  center - player.transform.position;
         activeCenter =false;
     }
  }
}
