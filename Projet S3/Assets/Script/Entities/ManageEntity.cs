using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEntity : MonoBehaviour
{
    public enum EntityType
    {
        Cultiste, Distance, Coloss, Patrole
    }
    public EntityType Type = EntityType.Cultiste;
    public int maxCultisteInspec;
    public int maxDistanceInspec;
    public int maxColossInspec;
    public int maxPatrolInspec;

    public static Transform[] ritualPoint = new Transform[8];
    public GameObject[] ritualPointPublic;


    public static List<GameObject> nonActiveRitualPoint;

    public static List<GameObject> activeRitualPoint = new List<GameObject>();

    public List<Spawner> spawnersList;

    public float spawnTime;

    private static int nbCultiste;
    private static int nbDistance;
    private static int nbColoss;
    private static int nbPatrol;
    private static int maxCultiste;
    private static int maxDistance;
    private static int maxColoss;
    private static int maxPatrol;

    public int currentNbCultiste;
    public int currentNbDistance;
    public int currentNbColoss;
    public int currentNbPatrol;

    public float _timeCounter;
    public int nbEntityInspec;
    public static int nbEntity;
    public static int nbEntityTotal;
    public static int PercentKill;
    public int nbEntityTotalInspec;
    public int PercentKillInspec;

    private static bool isCultistSpawn ;
    private static bool isDistanceSpawn;
    private static bool isPatrolSpawn;
    private static bool isColossSpawn;
    // Start is called before the first frame update
    void Start()
    {
        nbEntity = 0;
        nbCultiste = 0;
        nbDistance = 0;
        nbColoss = 0;
        nbPatrol = 0;
        maxCultiste = maxCultisteInspec;
        maxDistance = maxDistanceInspec;
        maxColoss = maxColossInspec;
        maxPatrol = maxPatrolInspec;

        for(int i = 0; i < ritualPointPublic.Length ; i++)
        {
            ritualPoint[i] = ritualPointPublic[i].transform;
        }

        //nonActiveRitualPoint = ;
        _timeCounter = spawnTime / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        ResetByFrame();
        nbEntityInspec = nbEntity;
        nbEntityTotalInspec = nbEntityTotal;
        PercentKillInspec = PercentKill;
        if (StateOfGames.currentPhase != StateOfGames.PhaseOfDefaultPlayable.Phase3)
        {
            if(nbEntityTotal != 0)
            {
                PercentKill = 100 - (nbEntity * 100 / nbEntityTotal);
            }
        }


        currentNbCultiste = nbCultiste;
        currentNbDistance = nbDistance;
        currentNbPatrol = nbPatrol;
        currentNbColoss = nbColoss;
    }


    public void ResetByFrame()
    {
        isColossSpawn =false;
        isCultistSpawn =false;
        isDistanceSpawn = false;
        isPatrolSpawn = false;
    }

    public static bool CheckInstantiateInvoq(EntityType typeToInstiate)
    {
        if (typeToInstiate == EntityType.Cultiste)
        {
            if (nbCultiste < maxCultiste && !isCultistSpawn)
            {
                nbCultiste++;
                isCultistSpawn =true;
                return true;
            }
            else
            {
                return false;
            }

        }
        if (typeToInstiate == EntityType.Distance)
        {
            if (nbDistance < maxDistance && !isDistanceSpawn)
            {
                nbDistance++;
                isDistanceSpawn =true;
                return true;
            }
            else
            {
                return false;
            }
        }
         if (typeToInstiate == EntityType.Coloss)
        {
            if (nbColoss < maxColoss && !isColossSpawn )
            {
                nbColoss++;
                isColossSpawn =true;
                return true;
            }
            else
            {
                return false;
            }
        }
        if (typeToInstiate == EntityType.Patrole)
        {
            if (nbPatrol < maxPatrol && !isPatrolSpawn)
            {
                isPatrolSpawn = true;
                nbPatrol++;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public static void DestroyEntity(EntityType typeToDestroy)
    {
        if (typeToDestroy == EntityType.Cultiste)
        {

            nbCultiste--;

        }
        else if (typeToDestroy == EntityType.Distance)
        {

            nbDistance--;

        }
        else if (typeToDestroy == EntityType.Coloss)
        {

            nbColoss--;

        }
        else if (typeToDestroy == EntityType.Patrole)
        {

            nbPatrol--;

        }
    }



    public static void SetActiveRitualPoint(GameObject ritualPointGive, bool active)
    {
        // if(active)
        // {
        //     nonActiveRitualPoint.RemoveAt(nonActiveRitualPoint.IndexOf(ritualPointGive));
        //     activeRitualPoint.Add(ritualPointGive);
        // }
        //
        // if(!active)
        // {
        //     activeRitualPoint.RemoveAt(activeRitualPoint.IndexOf(ritualPointGive));
        //     nonActiveRitualPoint.Add(ritualPointGive);
        // }
    }

    public void DirectiveInstantiate(GameObject ritualPointGive)
    {
        //int rand = Random.Range(0, spawnersList.Count);
        //spawnersList[rand].startBehavior = EntitiesManager.BeheaviorCultiste.RituelPoint;
        //spawnersList[rand].target = ritualPointGive;
    }

}
