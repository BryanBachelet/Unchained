﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ManagerEntites managerEntites;
   [HideInInspector] public GameObject regionParent;
    private float compteur = 13;
    public List<GameObject> objectToInstantiate;
    public GameObject parentToSpawn;
    public float timeOfSpawn;
    [Header("Caractéristique du spawner")]
    public GameObject target;
    public float radius;
    public float speedOfAgent;


    public bool bigSpawn = false;
    [Range(1, 10)]
    public int nbrEntiteeToSpawn;
    [Range(1, 10)]
    public int nbrPointOfSpawn;

    public GameObject vfChargeBlue;
    public GameObject vfChargeViolet;
    public GameObject vfChargeJauge;
    CenterTag.Types typeToSpawn;
    private Color colorToSpawn;

    public bool instantiate;


    private void Start()
    {

        typeToSpawn = target.GetComponent<CenterTag>().centerTypes;

        if (typeToSpawn == CenterTag.Types.Blue)
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


        SpawnObject(typeToSpawn);




    }

    void SpawnObject(CenterTag.Types currentTag)
    {
        if (compteur > timeOfSpawn)
        {
            if (bigSpawn)
            {
                for (int i = 0; i < nbrPointOfSpawn; i++)
                {
                    Vector2 posToSpawn = Vector2.zero;
                    for (int j = 0; j < nbrEntiteeToSpawn; j++)
                    {

                        if (j == 0)
                        {
                            posToSpawn = Random.insideUnitCircle * radius;
                        }
                        else
                        {
                            posToSpawn = Random.insideUnitCircle * radius;
                        }

                        GameObject add = new GameObject();
                        int numberAgent = Random.Range(0, objectToInstantiate.Count);
                        if (instantiate)
                        {
                            add = Instantiate(objectToInstantiate[numberAgent], new Vector3(transform.position.x + posToSpawn.x, 1, transform.position.z + posToSpawn.y), transform.rotation);
                        }
                        if (typeToSpawn == CenterTag.Types.Blue)
                        {
                            if (!instantiate)
                            {
                                add = managerEntites.RemoveEntites(managerEntites.entitiesBlue);
                            }
                            EnnemiDestroy ennemiDestroyAdd = add.GetComponent<EnnemiDestroy>();
                            EntitiesTypes entitiesTypes = add.GetComponent<EntitiesTypes>();
                            add.transform.position = new Vector3(transform.position.x + posToSpawn.x, 1, transform.position.z + posToSpawn.y);
                            entitiesTypes.entitiesTypes = EntitiesTypes.Types.Blue;
                            ennemiDestroyAdd.vfxBlueUp = vfChargeBlue;
                            ennemiDestroyAdd.managerEntites = managerEntites;
                            ennemiDestroyAdd.i = 1;
                            ennemiDestroyAdd.activePull = instantiate;
                        }
                        else if (typeToSpawn == CenterTag.Types.Orange)
                        {
                            if (!instantiate)
                            {
                                add = managerEntites.RemoveEntites(managerEntites.entitiesBlue);
                            }
                            EnnemiDestroy ennemiDestroyAdd = add.GetComponent<EnnemiDestroy>();
                            EntitiesTypes entitiesTypes = add.GetComponent<EntitiesTypes>();
                            add.transform.position = new Vector3(transform.position.x + posToSpawn.x, 1, transform.position.z + posToSpawn.y);
                            add.GetComponent<EntitiesTypes>().entitiesTypes = EntitiesTypes.Types.Orange;
                            ennemiDestroyAdd.vfxBlueUp = vfChargeJauge;
                            ennemiDestroyAdd.managerEntites = managerEntites;
                            ennemiDestroyAdd.i = 2;
                            ennemiDestroyAdd.activePull = instantiate;
                        }
                        else if (typeToSpawn == CenterTag.Types.Violet)
                        {
                            if (!instantiate)
                            {
                                add = managerEntites.RemoveEntites(managerEntites.entitiesBlue);
                            }
                            EnnemiDestroy ennemiDestroyAdd = add.GetComponent<EnnemiDestroy>();
                            EntitiesTypes entitiesTypes = add.GetComponent<EntitiesTypes>();
                            add.transform.position = new Vector3(transform.position.x + posToSpawn.x, 1, transform.position.z + posToSpawn.y);
                            add.GetComponent<EntitiesTypes>().entitiesTypes = EntitiesTypes.Types.Violet;
                            ennemiDestroyAdd.vfxBlueUp = vfChargeViolet;
                            ennemiDestroyAdd.managerEntites = managerEntites;
                            ennemiDestroyAdd.i = 3;
                            ennemiDestroyAdd.activePull = instantiate;
                        }
                        EnnemiBehavior ennemiBehaviorAdd = add.GetComponent<EnnemiBehavior>();
                        ennemiBehaviorAdd.target = target;
                        ennemiBehaviorAdd.speedClassic = speedOfAgent;
                        compteur = 0;
                    }
                }
            }
            else
            {
                for (int j = 0; j < nbrEntiteeToSpawn; j++)
                {

                    Vector2 posToSpawn = Random.insideUnitCircle * radius;
                    int numberAgent = Random.Range(0, objectToInstantiate.Count);
                    GameObject add = Instantiate(objectToInstantiate[numberAgent], new Vector3(transform.position.x + posToSpawn.x, 1, transform.position.z + posToSpawn.y), transform.rotation);
                    if (typeToSpawn == CenterTag.Types.Blue)
                    {
                        add.GetComponent<EntitiesTypes>().entitiesTypes = EntitiesTypes.Types.Blue;
                        add.GetComponent<EnnemiDestroy>().vfxBlueUp = vfChargeBlue;
                    }
                    else if (typeToSpawn == CenterTag.Types.Orange)
                    {
                        add.GetComponent<EntitiesTypes>().entitiesTypes = EntitiesTypes.Types.Orange;
                        add.GetComponent<EnnemiDestroy>().vfxBlueUp = vfChargeJauge;
                    }
                    else if (typeToSpawn == CenterTag.Types.Violet)
                    {
                        add.GetComponent<EntitiesTypes>().entitiesTypes = EntitiesTypes.Types.Violet;
                        add.GetComponent<EnnemiDestroy>().vfxBlueUp = vfChargeViolet;
                    }
                    add.GetComponent<EnnemiBehavior>().target = target;
                    add.GetComponent<EnnemiBehavior>().speedClassic = speedOfAgent;
                    compteur = 0;
                }
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


