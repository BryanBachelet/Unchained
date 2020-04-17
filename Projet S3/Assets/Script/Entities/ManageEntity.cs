using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEntity : MonoBehaviour
{
    public enum EntityType
    {
        Cultiste, Distance, Coloss
    }
    public EntityType Type = EntityType.Cultiste;

    public int maxCultisteInspec;
    public int maxDistanceInspec;
    public int maxColossInspec;

    public List<GameObject> ritualPoint;
    

    public static List<GameObject> nonActiveRitualPoint;

    public static List<GameObject> activeRitualPoint = new List<GameObject>();

    public List<Spawner> spawnersList;

    public float spawnTime;

    private static int nbCultiste;
    private static int nbDistance;
    private static int nbColoss;
    private static int maxCultiste;
    private static int maxDistance;
    private static int maxColoss;

    public float _timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        maxCultiste = maxCultisteInspec;
        maxDistance = maxDistanceInspec;
        maxColoss = maxColossInspec;

       nonActiveRitualPoint =  ritualPoint; 
       _timeCounter= spawnTime/1.5f;
    }

    // Update is called once per frame
    void Update()
    {
    
        if(nonActiveRitualPoint.Count>0 && _timeCounter>spawnTime)
        {
            if(nonActiveRitualPoint.Count>5)
            {
                for(int i=0; i<3;i++)
                {
                    DirectiveInstantiate(nonActiveRitualPoint[nonActiveRitualPoint.Count-1]);
                    SetActiveRitualPoint(nonActiveRitualPoint[nonActiveRitualPoint.Count-1],true);
                }
                _timeCounter = 0;
            }
            else
            {
                DirectiveInstantiate(nonActiveRitualPoint[nonActiveRitualPoint.Count-1]);
                SetActiveRitualPoint(nonActiveRitualPoint[nonActiveRitualPoint.Count-1],true);
                _timeCounter= 0;
            }
        }
        else
        {
            _timeCounter += Time.deltaTime;
        }
    }

    public static bool CheckInstantiateInvoq(EntityType typeToInstiate)
    {
        if(typeToInstiate == EntityType.Cultiste)
        {
            if(nbCultiste < maxCultiste)
            {
                
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
        else
        {
            return false;
        }
    }
    public static void DestroyEntity(EntityType typeToDestroy)
    {
        if (typeToDestroy == EntityType.Cultiste)
        {
            if (nbCultiste < maxCultiste)
            {
                nbCultiste--;
            }
        }
        else if (typeToDestroy == EntityType.Distance)
        {
            if (nbDistance < maxDistance)
            {
                nbDistance--;
            }
        }
        else if (typeToDestroy == EntityType.Coloss)
        {
            if (nbColoss < maxColoss)
            {
                nbColoss--;
            }
        }
    }



    public static void SetActiveRitualPoint(GameObject ritualPointGive, bool active)
    {
        if(active)
        {
            nonActiveRitualPoint.RemoveAt(nonActiveRitualPoint.IndexOf(ritualPointGive));
            activeRitualPoint.Add(ritualPointGive);
        }

        if(!active)
        {
            activeRitualPoint.RemoveAt(activeRitualPoint.IndexOf(ritualPointGive));
            nonActiveRitualPoint.Add(ritualPointGive);
        }
    }

    public void DirectiveInstantiate(GameObject ritualPointGive)
    {
            int rand = Random.Range(0, spawnersList.Count);
            spawnersList[rand].startBehavior = EntitiesManager.BeheaviorCultiste.RituelPoint;
            spawnersList[rand].target = ritualPointGive;
    }

}
