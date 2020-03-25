using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOfEntity : MonoBehaviour
{
    public enum EntityState
    {
        Formation, ReturnFormation, Catch, Destroy , Dead 
    }
    public EntityState entity = EntityState.Formation;

    private EntityState currentEntityState = EntityState.Destroy;
    private EnnemiDestroy ennemiDestroy;

    void Start()
    {
        ennemiDestroy = GetComponent<EnnemiDestroy>();
        //vfxCharge = transform.parent.GetComponent<CircleFormation>().vfxtoGive;
    }


    void Update()
    {
        if (entity != EntityState.Destroy && currentEntityState != entity)
        {
            currentEntityState = entity;
            ennemiDestroy.enabled = false;
        }

    }

    public void DestroyProjection(bool isProjection, Vector3 dir)
    {
            entity = EntityState.Destroy;
            currentEntityState = entity;
            ennemiDestroy.enabled = true;
            if (isProjection)
            {
                ennemiDestroy.ActiveProjection(dir);
            }
            else
            {
                ennemiDestroy.ActiveExplosion(dir);
            }
        
    }

}
