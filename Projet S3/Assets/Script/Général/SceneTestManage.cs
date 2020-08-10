using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTestManage : MonoBehaviour
{

    public GameObject[] situationOne = new GameObject[0];
    public GameObject[] situationTwo = new GameObject[0];

    public GameObject playerGO;

    private Vector3 beginPositionPlayer;
    
    private int step;
    
    // Start is called before the first frame update
    void Start()
    {
        beginPositionPlayer = playerGO.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(step==0)
            {
                ActiveArray(situationOne,false);
                ActiveArray(situationTwo,true);
                playerGO.transform.position = beginPositionPlayer;
            }
            if(step == 1)
            {
                SceneManager.LoadSceneAsync(2);
            }
            step++;
        }
    }

    public void ActiveArray(GameObject[] array, bool value)
    {
        for(int i = 0; i<array.Length;i++)
        {
            array[i].SetActive(value);
        }
    }
    
}
