using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOcclusion : MonoBehaviour
{
    public float radius;
    public bool isChecking = false;
    public LayerMask layerRegion;
    public Collider[] SpawnerToEnable;
    public List<GameObject> spawnerList = new List<GameObject>();
    public GameObject[] spawnerTab;
    public float tempsBtwnCheck;
    float tempsEcouleCheck = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnerTab = GameObject.FindGameObjectsWithTag("Spawner");
        CheckSpawner3();
        //SpawnerToEnable = Physics.OverlapSphere(transform.position, radius, layerRegion);
    }

    // Update is called once per frame
    void Update()
    {
        tempsEcouleCheck += Time.deltaTime;
        if(tempsEcouleCheck > tempsBtwnCheck)
        {
            CheckSpawner3();
        }
        else
        {
            SpawnerToEnable = Physics.OverlapSphere(transform.position, radius, layerRegion);
        }

    }

    public void CheckSpawner()
    {
        SpawnerToEnable = Physics.OverlapSphere(transform.position, radius, layerRegion);
        for (int i = 0; i < SpawnerToEnable.Length; i++)
        {
            //spawnerList.Add(SpawnerToEnable[i].gameObject);
            for (int j = 0; j < spawnerTab.Length; j++)
            {
                if (SpawnerToEnable[i].gameObject == spawnerTab[j].gameObject)
                {
                    SpawnerToEnable[i].gameObject.SetActive(true);
                    j = spawnerTab.Length;
                    //i++;
                }
                else if(spawnerTab[j].gameObject != SpawnerToEnable[i].gameObject && j == spawnerTab.Length -1)
                {

                    spawnerTab[j].gameObject.SetActive(false);
                }
            }
        }
        tempsEcouleCheck = 0;
    }

    void CheckSpawner2()
    {
        SpawnerToEnable = Physics.OverlapSphere(transform.position, radius, layerRegion);
        for (int j = 0; j < spawnerTab.Length; j++)
        {
            for(int i = 0; i < SpawnerToEnable.Length; i++)
            {
                if (spawnerTab[j].gameObject != SpawnerToEnable[i].gameObject )
                {
                    spawnerTab[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    SpawnerToEnable[i].transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    void CheckSpawner3()
    {
        for(int i = 0; i < spawnerTab.Length; i++)
        {
            if(Vector3.Distance(transform.position, spawnerTab[i].transform.position) < radius)
            {
                spawnerTab[i].gameObject.SetActive(true);
            }
            else
            {
                spawnerTab[i].gameObject.SetActive(false);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
