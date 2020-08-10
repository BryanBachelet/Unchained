using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpOnLimite : MonoBehaviour
{
    Vector3 myPosOnCollidLimite;
    Vector3 myEntitiStockPos;
    EnnemiStock myESscript;
    bool iHaveToTp = false;
    public float ajoutDist;
    Vector3 distMeEntiti;
    // Start is called before the first frame update
    void Start()
    {
        myESscript = gameObject.GetComponent<EnnemiStock>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(iHaveToTp)
        {
            transform.position = myPosOnCollidLimite;
            if (myESscript.ennemiStock != null)
            {
                myESscript.ennemiStock.transform.position = transform.position - distMeEntiti;
            }

            iHaveToTp = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            string posWall = collision.gameObject.GetComponent<wallPosition>().myPos;
            if (myESscript.ennemiStock != null)
            {
                distMeEntiti = myEntitiStockPos - transform.position ;
            }
            if (posWall == "North")
            {
                if(myESscript.ennemiStock != null)
                {
                    myEntitiStockPos = new Vector3(myESscript.ennemiStock.transform.position.x, myESscript.ennemiStock.transform.position.y, -myESscript.ennemiStock.transform.position.z);
                }
                myPosOnCollidLimite = new Vector3(transform.position.x, transform.position.y, -transform.position.z - ajoutDist);
            }
            else if (posWall == "South")
            {
                if (myESscript.ennemiStock != null)
                {
                    myEntitiStockPos = new Vector3(myESscript.ennemiStock.transform.position.x, myESscript.ennemiStock.transform.position.y, -myESscript.ennemiStock.transform.position.z);
                }
                myPosOnCollidLimite = new Vector3(transform.position.x, transform.position.y, -transform.position.z + ajoutDist);
            }
            else if (posWall == "West")
            {
                if (myESscript.ennemiStock != null)
                {
                    myEntitiStockPos = new Vector3(-myESscript.ennemiStock.transform.position.x, myESscript.ennemiStock.transform.position.y, myESscript.ennemiStock.transform.position.z);
                }
                myPosOnCollidLimite = new Vector3(-transform.position.x - ajoutDist, transform.position.y, transform.position.z);
            }
            else if (posWall == "East")
            {
                if (myESscript.ennemiStock != null)
                {
                    myEntitiStockPos = new Vector3(-myESscript.ennemiStock.transform.position.x, myESscript.ennemiStock.transform.position.y, myESscript.ennemiStock.transform.position.z);
                }
                myPosOnCollidLimite = new Vector3(-transform.position.x + ajoutDist, transform.position.y, transform.position.z);
            }
            iHaveToTp = true;
        }
    }
}
