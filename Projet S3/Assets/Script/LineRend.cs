using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRend : MonoBehaviour
{
    [Header("Options")]
    public bool activeParticle = true;
    public bool upProjection = true;
    [HideInInspector]
    public bool active;
    public LineRenderer lineRenderer;
    public BoxCollider box;
    public Vector3 p1;
    public Vector3 p2;
    private float dot;
    private float distance;
    private EnnemiStock ennemiStock;
    public GameObject particuleContact;

    public bool activeDetach = false;
    [Header("Sound")]
    [FMODUnity.EventRef]
    public string contact;
    private FMOD.Studio.EventInstance contactSound;


    public float volume = 20;

    public int comboCompt = 0;
    bool onCombo;
    public float timeOnCombo;
    float tempsEcouleCombo = 0;
    int lastComboValue;
    int lastFramCombo;
    public Text comboComptTxt;
    public Text lastComboTxt;
    public Text killPerTxt;
    public Animator myAnimator;
    private bool activeVelocity;
    private Velocity velocity;

    public float killPerSec;
    public List<float> timeKill = new List<float>();
    public float timeEcoule;
    public float paramValue;
    // Start is called before the first frame update
    void Start()
    {


        if (transform.parent.GetComponent<Velocity>())
        {
            activeVelocity = true;
            velocity = transform.parent.GetComponent<Velocity>();
        }
        if (comboComptTxt != null)
        {
            comboComptTxt.enabled = false;
        }
        if (lastComboTxt != null)
        {
            lastComboTxt.enabled = false;
        }
        if (transform.parent.GetComponent<EnnemiStock>())
        {
            ennemiStock = transform.parent.GetComponent<EnnemiStock>();
        }
        //Sound
        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(volume);

    }

    void Update()
    {

        timeEcoule = Time.time;
        for(int i = 0; i < timeKill.Count; i ++)
        {
            if(timeEcoule > timeKill[i] +3)
            {
                timeKill.RemoveAt(i);
                //Debug.Log("DEGAGE");
            }
            else
            {
                //Debug.Log("RESTE");
            }

        }
        killPerTxt.text = ("" + timeKill.Count);

    }
    // Update is called once per frame
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

            SetLine();
            // ColliderSize();

        }
        if (onCombo)
        {
            tempsEcouleCombo += Time.deltaTime;
            comboComptTxt.text = ("" + comboCompt);
            if (tempsEcouleCombo > timeOnCombo)
            {
                contactSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                // myAnimator.SetBool("OnActivation", true);
                lastComboValue = comboCompt;
                onCombo = false;
                comboCompt = 0;
                lastComboTxt.text = ("" + lastComboValue);
                lastComboTxt.enabled = true;
                comboComptTxt.enabled = false;
            }
        }
    }

    void SetLine()
    {
        //lineRenderer.SetPosition(0, ennemiStock.ennemiStock.transform.position);
        //lineRenderer.SetPosition(1, transform.parent.position);

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
        //contactSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        if (lastComboTxt.enabled == true)
        {
            lastComboTxt.enabled = false;
        }
        comboComptTxt.enabled = true;
        if (!upProjection)
        {
            float sign = Mathf.Sign(Vector3.Angle(transform.position, collision.transform.position));
            collision.GetComponent<Rigidbody>().AddForce(sign * transform.right * 35, ForceMode.Impulse);
        }
        else
        {
            //if(lastFramCombo < 6)
            //{
            //contactSound.getParameterByName("Entitipersec2", out paramValue);
            //contactSound.setParameterByName("Entitipersec2", timeKill.Count / 100);
            if (timeKill.Count < 40)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Mob/Toucher par le lien2", transform.parent.position);
            }

            //contactSound.start();
            //}
            //else
            //{
            //    if(lastFramCombo < 10 && timeKill.Count >= 10)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 20 && timeKill.Count >= 20)
            //    {
            //        contactSound.start();
            //    }
            //    else if(lastFramCombo < 30 && timeKill.Count >= 30)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 40 && timeKill.Count >= 40)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 50 && timeKill.Count >= 50)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 60 && timeKill.Count >= 60)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 70 && timeKill.Count >= 70)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 80 && timeKill.Count >= 80)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 90 && timeKill.Count >= 90)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 100 && timeKill.Count >= 100)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 110 && timeKill.Count >= 110)
            //    {
            //        contactSound.start();
            //    }
            //    else if (lastFramCombo < 120 && timeKill.Count >= 120)
            //    {
            //        contactSound.start();
            //    }
            //}
            //if (lastFramCombo <= 0 && timeKill.Count > 0)
            //{
            //    contactSound.getParameterByName("Entitipersec2", out paramValue);
            //    contactSound.setParameterByName("Entitipersec2", timeKill.Count / 100);
            //    contactSound.start();
            //
            //}
            //else if (lastFramCombo < 30 && timeKill.Count >= 30)
            //{
            //    contactSound.getParameterByName("Entitipersec2", out paramValue);
            //    contactSound.setParameterByName("Entitipersec2", timeKill.Count / 100);
            //    contactSound.start();
            //
            //}
            //else if (lastFramCombo < 60 && timeKill.Count >= 60)
            //{
            //    contactSound.getParameterByName("Entitipersec2", out paramValue);
            //    contactSound.setParameterByName("Entitipersec2", timeKill.Count / 100);
            //    contactSound.start();
            //}
            //else if (lastFramCombo >= 30 && timeKill.Count < 30)
            //{
            //    contactSound.getParameterByName("Entitipersec2", out paramValue);
            //    contactSound.setParameterByName("Entitipersec2", timeKill.Count / 100);
            //    contactSound.start();
            //}

            //contactSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(collision.gameObject));
            float rndX = Random.Range(-15, 15);


            //Instantiate(particuleContact, collision.transform.position, Quaternion.identity);
            if (!collision.GetComponent<EnnemiDestroy>().isDestroying)
            {
                collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 35 + new Vector3(rndX, 0, 0), ForceMode.Impulse);
            }
            if (activeParticle)
            {
                Transform transfChild = collision.transform.GetChild(0);
                transfChild.gameObject.SetActive(true);
            }

        }
        if (activeVelocity)
        {

            if (collision.GetComponent<EntitiesTypes>().entitiesTypes == EntitiesTypes.Types.Blue)
            {
                velocity.GetAddVelocityPoint(0);
            }
            if (collision.GetComponent<EntitiesTypes>().entitiesTypes == EntitiesTypes.Types.Orange)
            {
                velocity.GetAddVelocityPoint(1);

            }
            if (collision.GetComponent<EntitiesTypes>().entitiesTypes == EntitiesTypes.Types.Violet)
            {
                velocity.GetAddVelocityPoint(2);
            }
        }

        comboCompt++;
        if (!onCombo)
        {
            onCombo = true;
        }
        tempsEcouleCombo = 0;
        collision.attachedRigidbody.detectCollisions =false;
        collision.GetComponent<EnnemiDestroy>().isDestroying = true;
        lastFramCombo = timeKill.Count;
        timeKill.Add(Time.time);
    }
}


