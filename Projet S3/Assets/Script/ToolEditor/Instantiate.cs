using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Instantiate : MonoBehaviour
{
    public GameObject gameObjectToInstantiate;
    public Vector3 position;
    public int numberOfObject;
    public bool active;
   

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            active = false;
            for (int i = 0; i < numberOfObject; i++)
            {
                Instantiate(gameObjectToInstantiate, position, Quaternion.identity);
            }
        }
    }
}
