using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject mobPrefab;
    public float tempsEntreSpawn;
    private float tempsEcouleSpawn;

    public float radius;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tempsEcouleSpawn += Time.deltaTime;
        if (tempsEcouleSpawn > tempsEntreSpawn)
        {
            Vector3 posToSpawn = Random.insideUnitCircle * radius;
            Instantiate(mobPrefab, new Vector3(posToSpawn.x, 0, posToSpawn.z), transform.rotation);
            tempsEcouleSpawn = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
