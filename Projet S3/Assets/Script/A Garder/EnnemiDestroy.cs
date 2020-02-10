using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiDestroy : MonoBehaviour
{
    public ManagerEntites managerEntites;
    public int i = 0;
    public bool isDestroying;
    public float timerToDestro = 2;
    private float compteur;
    bool enter = false;
    public GameObject vfxBlueUp;
    public GameObject players;

    [Header("AutoDestroy")]
    public float tpsDestroy = 90;
    public float tpsEcoule;


    [Header("Sound")]
    [FMODUnity.EventRef]
    public string contact;
    //private FMOD.Studio.EventInstance contactSound;



    public float volume = 20;
    public float angleRotate;
    public int offset;

    public Transform pivotTransform;

    Vector3 deltaRotation;
    private EnnemiBehavior ennemiBehavior;
    float rndX;
    public Vector3 destroyDir;
    [HideInInspector] public bool activePull;
    void Start()
    {   ennemiBehavior = GetComponent<EnnemiBehavior>();
    players = PlayerMoveAlone.Player1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroying)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.Translate(0, 50 * Time.deltaTime, 0);

            if (!enter)
            {
                rndX = Random.Range(-1, 1);
                if (vfxBlueUp != null)
                {
                   
                    Instantiate(vfxBlueUp, transform.position, transform.rotation, players.transform);
                }
                enter = true;
            }
            if (compteur > timerToDestro)
            {

            }

            destroyDir = new Vector3(rndX, 1, 0);
            transform.RotateAround(pivotTransform.position, Vector3.up, 360f * Time.deltaTime);
            transform.Translate(destroyDir.normalized * 50 * Time.deltaTime);

            if (compteur > timerToDestro)
            {

                EndAgent();
               
            }
            else
            {
                compteur += Time.deltaTime;
            }
        }
        if (!ennemiBehavior.imStock)
        {
            tpsEcoule += Time.deltaTime;

            if (tpsEcoule >= tpsDestroy)
            {
                EndAgent();
            }
        }
    }

   private void EndAgent()
    {
        if (!activePull)
        {
            Transform transfChild = transform.GetChild(0);
            transfChild.gameObject.SetActive(false);
            enter = false;
            isDestroying = false;
            compteur = 0;
            tpsEcoule = 0;
            if (i == 1)
            {
                managerEntites.AddEntites(gameObject, managerEntites.entitiesBlue);
            }
            if (i == 2)
            {
                managerEntites.AddEntites(gameObject, managerEntites.entitiesOrange);
            }
            if (i == 3)
            {
                managerEntites.AddEntites(gameObject, managerEntites.entitiesViolet);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

