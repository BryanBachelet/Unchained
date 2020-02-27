using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiDestroy : MonoBehaviour
{
    [HideInInspector] public ManagerEntites managerEntites;
    [HideInInspector] public int i = 0;
    [HideInInspector] public bool isDestroying;
    [Header("Ejection")]
    public float angleRotate;
    public int offset;

    public Transform pivotTransform;
    public float timerBeforeDestroy = 2;
    private float compteur;
    bool enter = false;
    [HideInInspector] public GameObject vfxBlueUp;
    private GameObject player;
    [HideInInspector] public Vector3  dirHorizontalProjection;
    private Vector3 dirVertical;
    private Rigidbody ennemiRigidBody;

    [Header("AutoDestroy")]
    public float tpsDestroy = 90;
    public float tpsEcoule;

    Vector3 deltaRotation;
    private EnnemiBehavior ennemiBehavior;
    float rndX;
    [HideInInspector] public Vector3 destroyDir;
    [HideInInspector] public bool activePull;

    [Header("Sound")]
    [FMODUnity.EventRef]
    public string contact;
    //private FMOD.Studio.EventInstance contactSound;
    public float volume = 20;
    void Start()
    {
        ennemiBehavior = GetComponent<EnnemiBehavior>();
        ennemiRigidBody = GetComponent<Rigidbody>();
        player = PlayerMoveAlone.Player1;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isDestroying)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //  transform.Translate(0, 50 * Time.deltaTime, 0);
            transform.Translate(dirHorizontalProjection.normalized * 10 *Time.deltaTime);
            if (ennemiRigidBody.velocity.y < 0)
            {
                ennemiRigidBody.velocity += (Vector3.up * Physics.gravity.y * (5 - 1) *Time.deltaTime);
                Debug.Log(ennemiRigidBody.velocity);
            }
            if (ennemiRigidBody.velocity.y > 0)
            {
                ennemiRigidBody.velocity += (Vector3.up * Physics.gravity.y * (2.5f - 1) * Time.deltaTime);
            }
                if (!enter)
            {
                ennemiRigidBody.AddForce(Vector3.up * 25, ForceMode.Impulse);
            
                rndX = Random.Range(-1, 1);
                if (vfxBlueUp != null)
                {

                    Instantiate(vfxBlueUp, transform.position, transform.rotation, player.transform);
                }
                enter = true;
            }
            if (compteur > timerBeforeDestroy)
            {

            }

            //destroyDir = new Vector3(rndX, 1, 0);
            //transform.RotateAround(pivotTransform.position, Vector3.up, 360f * Time.deltaTime);
            //transform.Translate(destroyDir.normalized * 50 * Time.deltaTime);

            if (compteur > timerBeforeDestroy)
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

