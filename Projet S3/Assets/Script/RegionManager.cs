using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    public List<GameObject> allRegion = new List<GameObject>();
    public GameObject currentRegion;
    public List<GameObject> regionToLoad = new List<GameObject>();

    public LayerMask layerGround;
    public float radius;

    public bool checking = false;

    float tempsEcouleLoad = 0;
    public float tempsBtwLoad;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tempsEcouleLoad += Time.deltaTime;
        if(tempsBtwLoad > tempsEcouleLoad)
        {
            checking = true;
        }
        if(checking)
        {
            GenericLoad();
            checking = false;
        }
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, layerGround))
        //{
        //    if (hit.collider.transform.gameObject != currentRegion || currentRegion == null)
        //    {
        //        currentRegion = hit.collider.gameObject;
        //        regionToLoad.Clear();
        //        GenericLoad();
        //        Debug.Log("Je Reload Les voisins");
        //    }
        //
        //}
    }

    public void checkRegionAroundMe()
    {
        //regionToLoad = currentRegion.myNeighb;
        for (int j = 0; j < allRegion.Count; j++)
        {
            for (int i = 0; i < regionToLoad.Count; i++)
            {
                if (allRegion[j] == regionToLoad[i])
                {
                    regionToLoad[i].SetActive(false);
                }
                else if (allRegion[j] != regionToLoad[i])
                {
                    regionToLoad[i].SetActive(true);
                }

            }
        }

    }

    public void GenericLoad()
    {
        regionToLoad.Clear();
        for (int i = 0; i < allRegion.Count; i++)
        {
            allRegion[i].SetActive(true);
        }

            Collider[] regionFound = Physics.OverlapSphere(transform.position, radius, layerGround);
        for (int j = 0; j < regionFound.Length; j++)
        {
            regionToLoad.Add(regionFound[j].gameObject);
            regionFound[j].gameObject.SetActive(true);
        }
        //for(int i = 0; i < allRegion.Count; i++)
        //{
        //    for(int j = 0; j < regionToLoad.Count; j++)
        //    {
        //        if(allRegion[i] != regionToLoad[j])
        //        {
        //            allRegion[i].SetActive(false);
        //        }
        //        else if (allRegion[i] == regionToLoad[j])
        //        {
        //            allRegion[i].SetActive(true);
        //        }
        //    }
        //}
        UnloadRegion();

    }

    public void UnloadRegion()
    {
        for (int i = 0; i < allRegion.Count; i++)
        {
            for (int j = 0; j < regionToLoad.Count; j++)
            {
                if(allRegion[i] == regionToLoad[j])
                {
                    allRegion[i].SetActive(true);
                    j = regionToLoad.Count;
                }
                else
                {
                    allRegion[i].SetActive(false);
                }
            }
        }

        tempsEcouleLoad = 0;

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
