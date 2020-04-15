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
    static int nbCultiste;
    static int nbDistance;
    static int nbColoss;
    static int maxCultiste;
    static int maxDistance;
    static int maxColoss;
    // Start is called before the first frame update
    void Start()
    {
        maxCultiste = maxCultisteInspec;
        maxDistance = maxDistanceInspec;
        maxColoss = maxColossInspec;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
