using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScope : MonoBehaviour
{
    public GameObject bullet;
   // public GameObject spawn;
    private EnnemiStock ennemiStock;
    private Vector3 direction;
    private GameObject instanceBullet;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {

        ennemiStock = GetComponent<EnnemiStock>();
        lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        direction = DirectionSouris();
        if (Input.GetMouseButtonDown(0) /*|| Input.GetMouseButtonDown(2)*/ && ennemiStock.ennemiStock == null && instanceBullet == null)
        {
            instanceBullet = Instantiate(bullet, transform.position + direction.normalized * 0.5f, transform.rotation);
            Projectils projectils = instanceBullet.GetComponent<Projectils>();
            projectils.dir = direction;
            projectils.player = gameObject;
            projectils.moveAlone = GetComponent<PlayerMoveAlone>();

        }
        if (ennemiStock.ennemiStock == null && instanceBullet != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, instanceBullet.transform.position);
        }
        if (ennemiStock.ennemiStock == null && instanceBullet == null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }

    }

    private Vector3 DirectionSouris()
    {
        Ray camera = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rauEnter;

        if (ground.Raycast(camera, out rauEnter))
        {
            Vector3 pointToLook = camera.GetPoint(rauEnter);
            Vector3 posPlayer = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 dir = pointToLook - posPlayer;
            Debug.DrawRay(gameObject.transform.position + direction.normalized * 0.5f,  dir.normalized * 100, Color.red);
            ///Debug.Log(dir.normalized);
            return dir;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
