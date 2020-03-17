using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashingTrans : MonoBehaviour
{
    [Header("Paramètre")]
    public int minNumberToAim = 0;
    public int maxNumberToAim = 10;
    public int currentmax;
    public int ratioMashingToEntities = 4;
    [Range(0, 1)]
    public float ratioMinimumMashing = 0.20f;
    public float timeToDeacrese = 1;

    private float timeSave;

    [Header("Debug")]
    public int numberToAim;
    public int numberInput;

    public float debugMinRatio;

    List<float> i = new List<float>();
    public float timing;

    public Text text;
    public CamMouvement camMouvement;

    private float compteur;
    private TransformationAgent agentTransfo;
    private ResetPlayer resetPlayerScript;
    private PlayerMoveAlone moveAlone;
    private bool activeExplode;
    private bool activationTransformation;

    private float shortestDistance;

    private List<Collider> colliders = new List<Collider>(0);
    private Collider[] hitColliders;
    void Start()
    {
        resetPlayerScript = GetComponent<ResetPlayer>();
        agentTransfo = GetComponent<TransformationAgent>();
        currentmax = maxNumberToAim;
        moveAlone = GetComponent<PlayerMoveAlone>();
    }

    private void OnEnable()
    {
        if (this.enabled == true)
        {
            activationTransformation = false;
            i.Clear();
            currentmax = maxNumberToAim;
            hitColliders = new Collider[0];
        }
    }

    void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 3);

        for (int j = 0; j < i.Count; j++)
        {
            if (i[j] + 1 < Time.time)
            {
                i.RemoveAt(j);
            }

        }
        numberInput = i.Count;
        numberToAim = hitColliders.Length / ratioMashingToEntities;
        numberToAim = Mathf.Clamp(numberToAim, minNumberToAim, currentmax);
        debugMinRatio = numberToAim * ratioMinimumMashing;

        if (camMouvement.i >= camMouvement.cams.Count)
        {
            if (!activationTransformation)
            {
                agentTransfo.startTranformationAnim(timing);
                activationTransformation = true;
            }
            if (hitColliders.Length > 7)
            {
                if (timeSave + timeToDeacrese < compteur)
                {
                    if (currentmax > minNumberToAim + 2)
                    {
                        currentmax--;
                        timeSave = compteur;
                    }

                }
                compteur += Time.deltaTime;
                text.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    i.Add(Time.time);
                }
                if (i.Count < numberToAim * ratioMinimumMashing && compteur > 1f)
                {
                    Physics.IgnoreLayerCollision(9, 9, false);
                    Physics.IgnoreLayerCollision(9, 10, false);
                    text.gameObject.SetActive(false);
                    resetPlayerScript.ResetFonction(true);
                }
                if (i.Count > numberToAim)
                {
                    Physics.IgnoreLayerCollision(9, 9, false);
                    Physics.IgnoreLayerCollision(9, 10, false);
                    text.gameObject.SetActive(false);
                    agentTransfo.ActiveExplosion();
                    activeExplode = true;
                    PropulsionAtFinish();

                    StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;
                    transform.GetComponent<PlayerMoveAlone>().enabled = true;

                }
            }

        }

    }

    public void PropulsionAtFinish()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 150, 1 << 9);
        colliders.Clear();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Rigidbody>().useGravity == true)
            {
                colliders.Add(hitColliders[i]);
            }
        }

        int[] distShort = new int[colliders.Count];
        int index = 0;

        for (int i = 0; i < colliders.Count; i++)
        {
            float currentDistance = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (currentDistance > shortestDistance)
            {
                distShort[index] = i;
                index++;
            }
        }

        CheckDirection(distShort, index);

    }

    private void CheckDirection(int[] distShort, int index)
    {
        int numberToRemember = 0;
        int[] numberOfentities = new int[index];
        for (int i = 0; i < index; i++)
        {
            Vector3 dirIndex = transform.position - colliders[distShort[i]].transform.position;
            for (int j = 0; j < colliders.Count; j++)
            {
                Vector3 colliderDir = transform.position - colliders[j].transform.position;
                float dotProduct = Vector3.Dot(dirIndex.normalized, colliderDir.normalized);
                float dist = Vector3.Distance(dirIndex, colliderDir);
                if (dotProduct > 0.80f && dist > 20)
                {
                    numberOfentities[i]++;
                }
            }

        }

        for (int i = 0; i < index; i++)
        {
            if (numberOfentities[i] > numberOfentities[numberToRemember])
            {
                numberToRemember = i;
            }
        }

        Vector3 dirGive = colliders[distShort[numberToRemember]].transform.position - transform.position;
        int sens = Random.Range(-1, 0) == 0 ? 1 : -1;

        dirGive = Quaternion.Euler(0, 10 * sens, 0) * dirGive.normalized;

        moveAlone.AddProjection(dirGive.normalized);

    }
}
