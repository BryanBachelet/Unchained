using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRend : MonoBehaviour
{
    public bool upProjection = true;
    [HideInInspector]
    public bool active;
    public LineRenderer lineRenderer;
    public BoxCollider box;
    private GameObject p1;
    private GameObject p2;
    private float dot;
    private float distance;
    private EnnemiStock ennemiStock;
    public GameObject particuleContact;

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
    public Text comboComptTxt;
    public Text lastComboTxt;
    public Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        comboComptTxt.enabled = false;
        lastComboTxt.enabled = false;
        if (transform.parent.GetComponent<EnnemiStock>())
        {
            ennemiStock = transform.parent.GetComponent<EnnemiStock>();
        }
        //Sound
        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(volume);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (ennemiStock != null)
        {
            if (ennemiStock.ennemiStock != null)
            {
                box.enabled = true;
                active = true;
            }
            else
            {
                active = false;
                box.enabled = false;
            }
        }

        if (active)
        {
            p2 = ennemiStock.ennemiStock;
            p1 = transform.parent.gameObject;
            SetLine();
            ColliderSize();

        }
        if(onCombo)
        {
            tempsEcouleCombo += Time.deltaTime;
            comboComptTxt.text = ("" + comboCompt);
            if (tempsEcouleCombo > timeOnCombo)
            {
                myAnimator.SetBool("OnActivation", true);
                lastComboValue = comboCompt;
                onCombo = false;
                comboCompt = 0;
                lastComboTxt.text = (""+ lastComboValue);
                lastComboTxt.enabled = true;
                comboComptTxt.enabled = false;
            }
        }
    }

    void SetLine()
    {
        lineRenderer.SetPosition(0, p2.transform.position);
        lineRenderer.SetPosition(1, p1.transform.position);

    }

    void ColliderSize()
    {
        distance = Vector3.Distance(p1.transform.position, p2.transform.position);
        Vector3 dir = p2.transform.position - p1.transform.position;
        dot = Vector3.SignedAngle(dir.normalized, Vector3.forward, Vector3.up);
        transform.position = p2.transform.position + (-dir.normalized * distance) / 2;
        transform.rotation = Quaternion.Euler(0, -dot, 0);
        box.size = new Vector3(1, 1, distance);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Ennemi")
        {
            if(lastComboTxt.enabled == true)
            {
                lastComboTxt.enabled = false;
            }
            comboComptTxt.enabled = true;
            if (!upProjection)
            {
                float sign = Mathf.Sign(Vector3.Angle(transform.position, collision.transform.position));
                collision.GetComponent<Rigidbody>().AddForce(sign * transform.right * 50, ForceMode.Impulse);
            }
            else
            {
                contactSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(collision.gameObject));
                float rndX = Random.Range(-15, 15);
                contactSound.start();
                Instantiate(particuleContact, collision.transform.position, Quaternion.identity);
                collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50 + new Vector3(rndX,0,0) , ForceMode.Impulse);
                Transform transfChild = collision.transform.GetChild(0);
                transfChild.gameObject.SetActive(true);


            }
            comboCompt++;
            if(!onCombo)
            {
                onCombo = true;
            }
            tempsEcouleCombo = 0;
            collision.GetComponent<EnnemiDestroy>().isDestroying = true;
        }
    }

}
