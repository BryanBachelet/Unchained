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

        //if(nonActiveRitualPoint.Count>0 && _timeCounter>spawnTime)
        //{
        //    if(nonActiveRitualPoint.Count>5)
        //    {
        //        for(int i=0; i<3;i++)
        //        {
        //            DirectiveInstantiate(nonActiveRitualPoint[nonActiveRitualPoint.Count-1]);
        //            SetActiveRitualPoint(nonActiveRitualPoint[nonActiveRitualPoint.Count-1],true);
        //        }
        //        _timeCounter = 0;
        //    }
        //    else
        //    {
        //        DirectiveInstantiate(nonActiveRitualPoint[nonActiveRitualPoint.Count-1]);
        //        SetActiveRitualPoint(nonActiveRitualPoint[nonActiveRitualPoint.Count-1],true);
        //        _timeCounter= 0;
        //    }
        //}
        //else
        //{
        //    _timeCounter += Time.deltaTime;
        //}

        currentNbCultiste = nbCultiste;
        currentNbDistance = nbDistance;
        currentNbColoss = nbColoss;
    }

    public static bool CheckInstantiateInvoq(EntityType typeToInstiate)
    {
        if (typeToInstiate == EntityType.Cultiste)
        {
            if (nbCultiste < maxCultiste)
            {
                nbCultiste++;
                return true;
            }
            else
            {
                return false;
            }

        }
        else if (typeToInstiate == EntityType.Distance)
        {
            if (nbDistance < maxDistance)
            {
                nbDistance++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (typeToInstiate == EntityType.Coloss)
        {
            if (nbColoss < maxColoss)
            {
                nbColoss++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (typeToInstiate == EntityType.Patrole)
        {
            if (nbPatrol < maxPatrol)
            {
                nbPatrol++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
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
