using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRend : MonoBehaviour
{
    [Header("Options")]
    public bool activeParticle = true;
    public bool upProjection = true;
    public bool activeDetach = false;
    [HideInInspector]
    public bool active;
    private LineRenderer lineRenderer;
    private BoxCollider box;
    [HideInInspector]
    public Vector3 p1;
    [HideInInspector]
    public Vector3 p2;
    private float dot;
    private float distance;
    private EnnemiStock ennemiStock;


    [Header("Feedback")]
    public GameObject particuleContact;



    [Header("Sound")]
    [FMODUnity.EventRef]
    public string contact;
    private FMOD.Studio.EventInstance contactSound;
    public float volume = 20;

    [HideInInspector]
    public float strenghOfExpulsion;

    private PlayerMoveAlone moveAlone;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = transform.parent.GetComponent<LineRenderer>();
        moveAlone = transform.parent.GetComponent<PlayerMoveAlone>();
        
        box = GetComponent<BoxCollider>();
        if (transform.parent.GetComponent<EnnemiStock>())
        {
            ennemiStock = transform.parent.GetComponent<EnnemiStock>();
        }

        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(volume);

    }



    void FixedUpdate()
    {
        if (ennemiStock != null)
        {
            if (ennemiStock.ennemiStock != null)
            {
                if (!active)
                {
                    box.enabled = true;
                    active = true;
                }
            }
            else
            {
                active = false;
                box.enabled = false;
            }
        }
        if (active)
        {
            ColliderSize();
        }

    }


    public void ColliderSize()
    {
        distance = Vector3.Distance(p1, p2);
        Vector3 dir = p2 - p1;
        dot = Vector3.SignedAngle(dir.normalized, Vector3.forward, Vector3.up);
        transform.position = p1 + (dir.normalized * distance) / 2;
        transform.rotation = Quaternion.Euler(0, -dot, 0);
        box.size = new Vector3(1, 1, distance);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Ennemi")
        {
            Collision(collision);
        }
        if (activeDetach)
        {
            if (collision.transform.tag == "wall")
            {
                if (ennemiStock.ennemiStock != collision.gameObject && ennemiStock.ennemiStock != null)
                {
                    ennemiStock.DetachPlayer();
                }
            }
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Ennemi")
        {
            Collision(collision);
        }
        if (activeDetach)
        {
            if (collision.transform.tag == "wall")
            {
                if (ennemiStock.ennemiStock != collision.gameObject && ennemiStock.ennemiStock != null)
                {
                    ennemiStock.DetachPlayer();
                }
            }
        }
    }

    void Collision(Collider collision)
    {

        if (!upProjection)
        {
            float sign = Mathf.Sign(Vector3.Angle(transform.position, collision.transform.position));
            collision.GetComponent<Rigidbody>().AddForce(sign * transform.right * 35, ForceMode.Impulse);
        }
        else
        {
            float rndX = Random.Range(-15, 15);
            if (!collision.GetComponent<EnnemiDestroy>().isDestroying)
            {

               // collision.GetComponent<Rigidbody>().detectCollisions = false;
            }
            if (activeParticle)
            {
                Transform transfChild = collision.transform.GetChild(0);
                transfChild.gameObject.SetActive(true);
            }

        }
       
        //collision.attachedRigidbody.detectCollisions = false;
        EnnemiDestroy ennemi = collision.GetComponent<EnnemiDestroy>();
        if (!ennemi.isDestroying)
        {
            ennemi.isDestroying = true;
            KillCountPlayer.AddList();
            Vector3 dir = p2 - p1;
            ennemi.dirHorizontalProjection = Vector3.Cross(Vector3.up, dir.normalized);
            ennemi.currentForceOfEjection = moveAlone.expulsionStrengh;
        }
    }

    //void Collision(Collider collision)
    //{
    //    Rigidbody rb;
    //    rb = collision.GetComponent<Rigidbody>();
    //    if (!upProjection)
    //    {
    //        float sign = Mathf.Sign(Vector3.Angle(transform.position, collision.transform.position));
    //        collision.transform.eulerAngles = new Vector3(0.0f, 15f, 0.0f);
    //        rb.AddRelativeForce(Vector3.forward * 20.0f + Vector3.up * 100.0f, ForceMode.Impulse);
    //        collision.GetComponent<EnnemiBehavior>().beenKicked = true;
    //    }
    //    else
    //    {
    //        float rndX = Random.Range(-15, 15);
    //        if (!collision.GetComponent<EnnemiDestroy>().isDestroying)
    //        {
    //
    //            collision.GetComponent<Rigidbody>().detectCollisions = false;
    //        }
    //        if (activeParticle)
    //        {
    //            Transform transfChild = collision.transform.GetChild(0);
    //            transfChild.gameObject.SetActive(true);
    //        }
    //
    //    }
    //    KillCountPlayer.AddList();
    //    collision.attachedRigidbody.detectCollisions = false;
    //    collision.GetComponent<EnnemiDestroy>().isDestroying = true;
    //
    //}
}


