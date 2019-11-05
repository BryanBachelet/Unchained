using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerFloorCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10, 9);
        GameObject gameObj = hit.collider.gameObject;
        if (gameObj.GetComponent<Corupt>())
        {
           // SceneManager.LoadScene(0);
            
        }
    }
}
