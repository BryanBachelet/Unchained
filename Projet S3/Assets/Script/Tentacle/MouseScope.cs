using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScope : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawn;
    public Vector3 direction;
    public EnnemiStock ennemiStock;
    private GameObject instanceBullet;
    // Start is called before the first frame update
    void Start()
    {
        ennemiStock = GetComponent<EnnemiStock>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = DirectionSouris();
        if (Input.GetMouseButtonDown(0) && ennemiStock.ennemiStock == null && instanceBullet == null)
        {
            instanceBullet = Instantiate(bullet, transform.position + direction.normalized * 0.5f, transform.rotation);
            Projectils projectils = instanceBullet.GetComponent<Projectils>();
            projectils.dir = direction;
            projectils.player = gameObject;

        }

    }

    private Vector3 DirectionSouris()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 hitPoint =  hit.point;
         hitPoint += (-ray.direction.normalized); // new Vector3(hit.point.x, 1, hit.point.z);
   
        Vector3 dir = hitPoint - gameObject.transform.position;

        Debug.DrawRay(gameObject.transform.position, dir.normalized * 100, Color.red);
        Debug.Log(dir.normalized);
        return dir.normalized;
    }
}
