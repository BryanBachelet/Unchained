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

        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 hitPoint = hit.point;
        Vector2 mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2);

        Vector3 dir = new Vector3(mousePos.x, 1, mousePos.y);
      

        Debug.DrawRay(gameObject.transform.position, dir.normalized * 100, Color.red);
        Debug.Log(dir.normalized);
        return dir.normalized;
    }
}
