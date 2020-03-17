using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOfEntity : MonoBehaviour
{
    public enum EntityState {Formation,ReturnFormation, DestoyProjection, DestroyExplosion }
    public EntityState entity = EntityState.Formation;

    private EntityState currentEntityState = EntityState.Formation;
    private EnnemiDestroy ennemiDestroy;
    void Start()
    {
        ennemiDestroy = GetComponent<EnnemiDestroy>();
    }

    
    void Update()
    {
        
    }

   public void DestroyProjection(Vector3 dir)
    {
        entity = EntityState.DestoyProjection;
        ennemiDestroy.ActiveProjection(dir);
    }
}
