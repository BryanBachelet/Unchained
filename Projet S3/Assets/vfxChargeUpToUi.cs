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
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speedParticle * Time.deltaTime);
        if(Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target.transform.parent.GetComponent<FillGrowEffect>().isPlayingAnim = true;
          
            Destroy(gameObject);
        }
    }
}
