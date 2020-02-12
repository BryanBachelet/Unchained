using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEntites : MonoBehaviour
{
    public List<GameObject> entitiesBlue;
    public List<GameObject> entitiesOrange;
    public List<GameObject> entitiesViolet;

   

    public GameObject  RemoveEntites( List<GameObject> entities )
    {
        GameObject entitiesRemove;
        int id = Random.Range(0, entities.Count);
        entitiesRemove = entities[id];
        entitiesRemove.SetActive(true);
        entitiesRemove.GetComponent<Rigidbody>().detectCollisions = true;
        entities.RemoveAt(id);
        return entitiesRemove;
    }
    public void AddEntites(GameObject entiteAdd , List<GameObject> entities)
    {
        entiteAdd.transform.position = new Vector3(0, 1000, 0);
        entiteAdd.SetActive(false);
        entities.Add(entiteAdd);
    }
}
