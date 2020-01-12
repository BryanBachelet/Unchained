using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxChargeUpToUi : MonoBehaviour
{
    public GameObject target;
    public int speedParticle = 10;
    public int number; // 1= blue, 2 = orange, 3 = violet
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("UiBarre" + number);
        target = PlayerMoveAlone.Player1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speedParticle * Time.deltaTime);
        //transform.Rotate(transform.GetChild(0).position, 50);
        //transform.RotateAround(transform.GetChild(0).position, Vector3.forward, 50f);
        if(Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            //target.transform.parent.GetComponent<FillGrowEffect>().isPlayingAnim = true;
          
            Destroy(gameObject);
        }
    }
}
