using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionProfile : MonoBehaviour
{
    public bool isActive = true;


    public List<GameObject> myNeighb = new List<GameObject>();
    //public List<detectionByWall> myWallDetector = new List<detectionByWall>();
    List<GameObject> tempNbCount = new List<GameObject>();
    bool getMyNb = false;
    // Start is called before the first frame update
    //void Start()
    //{


    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(!getMyNb)
    //    {
    //        launchGetMyNb();
    //    }
    //}

    //public void launchGetMyNb()
    //{
    //    for(int i = 0; i < myWallDetector.Count; i++)
    //    {
    //        bool isDetected = myWallDetector[i].launcDetection(false);
    //        if(!isDetected)
    //        {

    //        }
    //        else
    //        {
    //            for (int j = 0; j < myWallDetector[i].detected.Count; j++)
    //            {
    //                for(int h = 0; h < myNeighb.Count; h++)
    //                {
    //                    if(myNeighb[h].gameObject != myWallDetector[i].detected[j].gameObject || myNeighb.Count == 0)
    //                    {
    //                        myNeighb.Add(myWallDetector[i].detected[j].gameObject);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    getMyNb = true;
    //}
}
