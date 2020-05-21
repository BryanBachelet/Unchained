using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionFormationGroup : MonoBehaviour
{
    public static List<GameObject> formationList =new List<GameObject>(0);
    private static int[] childCountPerFormation =  new int[10];
    private static int currentEtape;
    private static bool checkReformation;

    private GameObject reformation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkReformation)
        {
            if(CountEntities())
            {
                
            }
            
            checkReformation =false;
        }
    }
    
    private void ActiveReformation()
   {
   // {
    //     reformation =  formationList[0];
    //     CircleFormation currentCircle = reformation.GetComponent<CircleFormation>();
    //     for(int i = 1; i<formationList.Count;i++)
    //     {
    //         CircleFormation circle = formationList[i].GetComponent<CircleFormation>();
    //         for(int j = 0 ; j<circle.childEntities.Count;i++)
    //         {
    //            if( circle.childEntities[j].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Dead &&  circle.childEntities[j].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Destroy &&  circle.childEntities[j].GetComponent<StateOfEntity>().entity != StateOfEntity.EntityState.Catch)
    //            {
    //                 circle.childEntities[j].transform.SetParent(reformation.transform);
    //                 currentCircle.childEntities.Add(circle.childEntities[j]);
    //                 circle.childEntities.RemoveAt(j);
    //            } 
    //         }
    //     }
    }

    private bool CountEntities()
    {   
        bool activeReformation = false;
        int numberOfAllEntities = 0;
        for(int i = 0 ; i<childCountPerFormation.Length;i++)
        {
          numberOfAllEntities +=  childCountPerFormation[i];
        }
        if(numberOfAllEntities>10)
        {
            activeReformation = true;
        }
        else
        {
            activeReformation =false;
        }
        return activeReformation ;
    }


    public static void AddToReformation(GameObject formation)
    {
        CircleFormation circle = formation.GetComponent<CircleFormation>();
        childCountPerFormation[currentEtape] = circle.CurrentActiveChild();
        currentEtape++;
        checkReformation =true;
    }

}
