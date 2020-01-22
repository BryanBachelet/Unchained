using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxChargeUpToUi : MonoBehaviour
{
    public GameObject target;
    public int speedParticle = 10;
    public float goUp = 5;
    public int number; // 1= blue, 2 = orange, 3 = violet
    public float step;
    public Vector3 moveTo;
    bool goDown = false;
    public Collider[] listVfxProx;
    public Collider[] recupVfxProx;
    public LayerMask lMvfx;
    public float radiusProx;
    public Vector3 moyennePos = Vector3.zero;

    public Transform pivotPos;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("UiBarre" + number);
        target = PlayerMoveAlone.Player1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //recupVfxProx = Physics.OverlapSphere(transform.position, radiusProx, lMvfx);
        //listVfxProx = recupVfxProx;
        //////////////////////CalculPivotPos(listVfxProx);

        if (step > 0)
        {
            if (step > goUp)
            {
                goDown = true;
            }
            if (goDown)
            {
                step -= step * Time.deltaTime * 3;
            }
            else
            {
                step += step * Time.deltaTime * 3;
            }
        }
        else
        {
            step = 0;
        }


        moveTo = new Vector3(target.transform.position.x, target.transform.position.y + step, target.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, moveTo, speedParticle * Time.deltaTime);
        ////////////////////transform.position = Vector3.MoveTowards(transform.position, transform.GetChild(0).position, speedParticle * Time.deltaTime);
        ////////////////////transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position, moveTo, speedParticle * Time.deltaTime);
        //transform.Rotate(transform.GetChild(0).position, 50);
        transform.RotateAround(pivotPos.position, Vector3.forward, 5f);
        if(Vector3.Distance(transform.position, target.transform.position) < 2f)
        {
            //target.transform.parent.GetComponent<FillGrowEffect>().isPlayingAnim = true;
          
            Destroy(gameObject);
        }
    }

    public void CalculPivotPos(Collider[] listVFX)
    {
        if (listVFX.Length > 1)
        {
            for(int i = 0; i < listVFX.Length; i++)
            {
                if(listVFX[i].transform.position != pivotPos.position)
                {
                    moyennePos += pivotPos.position - listVFX[i].transform.position;
                }

            }
            //transform.GetChild(0).position =  transform.position - moyennePos;
        }
        
    }
}
