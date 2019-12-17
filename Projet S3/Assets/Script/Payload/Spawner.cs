using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject regionParent;
    private float compteur = 13;
    public GameObject objectToInstantiate;
    public GameObject parentToSpawn;
    public float timeOfSpawn;
    [Header("Caractéristique du spawner")]
    public GameObject target;
    public float radius;
    public float speedOfAgent;

    [Range(1, 10)]
    public int nbrEntiteeToSpawn;

    CenterTag.Types typeToSpawn;
    public Color colorToSpawn;
    private void Start()
    {
        typeToSpawn = target.GetComponent<CenterTag>().centerTypes;
        if(typeToSpawn == CenterTag.Types.Blue)
        {
            colorToSpawn = Color.blue;
        }
        else if (typeToSpawn == CenterTag.Types.Orange)
        {
            colorToSpawn = Color.yellow;
        }
        else if (typeToSpawn == CenterTag.Types.Violet)
        {
            colorToSpawn = Color.magenta;
        }
    }
    void Update()
    {

        if(regionParent != null)
        {
            if (regionParent.activeSelf)
            {
                SpawnObject();
            }

        }
        else
        {
            SpawnObject();
        }

    }

    void SpawnObject()
    {
        if (compteur > timeOfSpawn)
        {
            for(int i = 0; i < nbrEntiteeToSpawn; i++)
            {
                Vector2 posToSpawn = Random.insideUnitCircle * radius;
                GameObject add = Instantiate(objectToInstantiate, new Vector3(transform.position.x + posToSpawn.x, 1, transform.position.z + posToSpawn.y), transform.rotation);
                if (typeToSpawn == CenterTag.Types.Blue)
                {
                    add.GetComponent<EntitiesTypes>().entitiesTypes = EntitiesTypes.Types.Blue;
                }
                else if (typeToSpawn == CenterTag.Types.Orange)
                {
                    add.GetComponent<EntitiesTypes>().entitiesTypes = EntitiesTypes.Types.Orange;
                }
                else if (typeToSpawn == CenterTag.Types.Violet)
                {
                    add.GetComponent<EntitiesTypes>().entitiesTypes = EntitiesTypes.Types.Violet;
                }
                add.GetComponent<EnnemiBehavior>().target = target;
                add.GetComponent<EnnemiBehavior>().speed = speedOfAgent;
                compteur = 0;
            }

        }
        else
        {
            compteur += Time.deltaTime;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
