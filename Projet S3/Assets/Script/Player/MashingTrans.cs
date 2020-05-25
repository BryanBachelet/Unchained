using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Colorful;
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

    public List<float> i = new List<float>();
    public float timing;

    public Image text;
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
    private Vector3 posStart;
    private bool activePos;

    public float tempsEcouleMashing;
    public float tempsMinMashing;

    private GainVelocitySystem gainVelocitySyst;
    private bool isP2 = false;
    private int h;

    private bool setMashingActive;

    private PlayerAnimState playerAnim;

    public float timeFinishMash;

    private float compteurFinishMash;

    private bool activeFinishMash;

    [HideInInspector] public bool activeMash;

    public float tempsEntreInputPulse = 1;
    float tempsEcouleInputPulse = 0;

    LifePlayer lifePlayerScript;

    private  bool setPulse;
    private  float tempsEcoulePulse;
    private  float tempsminBtwPulse;
    private bool winMashing;

    public GameObject charaOne;

    public GameObject charaTwo;

    private int numberOfTransformation;

    void Start()
    {
        resetPlayerScript = GetComponent<ResetPlayer>();
        agentTransfo = GetComponent<TransformationAgent>();
        currentmax = maxNumberToAim;
        moveAlone = GetComponent<PlayerMoveAlone>();
        gainVelocitySyst = GetComponent<GainVelocitySystem>();
        playerAnim = GetComponent<PlayerAnimState>();
        lifePlayerScript = GetComponent<LifePlayer>();
    }

    private void OnEnable()
    {
        if (this.enabled == true)
        {

            activationTransformation = false;
            i.Clear();
            currentmax = maxNumberToAim;
            hitColliders = new Collider[0];
            compteur = 0;
            numberInput = 0;
            numberToAim = 0;
            debugMinRatio = 0;
            tempsEcouleMashing = 0;
            posStart = transform.position;
            activePos = true;
            if (FastTest.debugMashing)
            {
                maxNumberToAim = 6;
            }

        }
    }

    void Update()
    {



        if (activePos)
        {
            moveAlone.StopVelocity();
        }
        if (setMashingActive)
        {
            if (tempsEcoulePulse < tempsEntreInputPulse)
            {
                tempsEcoulePulse += Time.deltaTime;
             
            }
            // Camera.main.GetComponent<Threshold>().enabled =true;
            if (!activationTransformation)
            {
                numberOfTransformation++;
                agentTransfo.startTranformationAnim(timing);
                activationTransformation = true;
                SetupMash();
            }
            if (activeMash)
            {
                playerAnim.ChangeSpeedAnimator(1f);
                if (timeSave + timeToDeacrese < compteur)
                {
                    if (currentmax > minNumberToAim + 2)
                    {
                        currentmax--;
                        timeSave = compteur;
                    }

                }
                compteur += Time.deltaTime;
                tempsEcouleMashing += Time.deltaTime;
                text.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
                {
                    i.Add(Time.time);
                    if(tempsEcoulePulse >= tempsEntreInputPulse)
                    {
                        tempsEcoulePulse = 0;
                        Pulse.tempsEcoulePulse = 0;
                    }
                }
              
                if (tempsEcouleMashing > tempsMinMashing)
                {
                    playerAnim.ChangeStateAnim(PlayerAnimState.PlayerStateAnim.EntraveFinish);
                    text.gameObject.SetActive(false);
                    SlowTime.RestartTime();
                    if((i.Count/tempsMinMashing) > minNumberToAim)
                    {
                        winMashing = true;
                    }else
                    {
                        winMashing =false;
                    }
                  

                    if (compteurFinishMash > timeFinishMash)
                    {
                        activePos = false;
                    
                        
                        activeExplode = true;
                        PropulsionAtFinish();

                        agentTransfo.ActiveExplosion();
                        StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;
                        transform.GetComponent<PlayerMoveAlone>().enabled = true;
                        setMashingActive = false;
                        ResetMash();
                        compteurFinishMash = 0;
                        activeMash = false;
                        if(numberOfTransformation>= 2)
                        {
                            charaOne.SetActive(false);
                            charaTwo.SetActive(true);
                            playerAnim.animator =  charaTwo.GetComponent<Animator>();
                        }
                        if(winMashing)
                        {                                                                                 
                            if ((i.Count/tempsMinMashing) <= 5)                                                                
                            {                                                                                //ScoringSystem + Health
                                gainVelocitySyst.gainMashP1 = 10;                                            //ScoringSystem + Health
                                DataPlayer.ChangeScore(1000, false);                                         //ScoringSystem + Health
                                                                                                            //ScoringSystem + Health
                                lifePlayerScript.AddHealth(10);                                              //ScoringSystem + Health
                            }                                                                                //ScoringSystem + Health
                            else if ((i.Count/tempsMinMashing) > 5 && (i.Count/tempsMinMashing) <= 7)                                            //ScoringSystem + Health
                            {                                                                                //ScoringSystem + Health
                                gainVelocitySyst.gainMashP1 = 20;                                            //ScoringSystem + Health
                                DataPlayer.ChangeScore(2500, false);                                         //ScoringSystem + Health
                                                                                                                //ScoringSystem + Health
                                lifePlayerScript.AddHealth(25);                                              //ScoringSystem + Health
                            }                                                                                //ScoringSystem + Health
                            else if ((i.Count/tempsMinMashing) > 7)                                                            //ScoringSystem + Health
                            {                                                                                //ScoringSystem + Health
                                gainVelocitySyst.gainMashP1 = 30;                                            //ScoringSystem + Health
                                DataPlayer.ChangeScore(5000, false);                                         //ScoringSystem + Health
                                                                                                                //ScoringSystem + Health
                                lifePlayerScript.AddHealth(50);                                              //ScoringSystem + Health
                            }                                                                                //ScoringSystem + Health
                        }else
                        {
                             lifePlayerScript.AddDamage(15);       
                        }

                    }
                    compteurFinishMash += Time.deltaTime;
                    //Camera.main.GetComponent<Threshold>().enabled =false;
                }

            }

        }
    }

    public void ResetMash()
    {
        activationTransformation = false;
        i.Clear();
        currentmax = maxNumberToAim;
        hitColliders = new Collider[0];
        compteur = 0;
        numberInput = 0;
        numberToAim = 0;
        debugMinRatio = 0;
        tempsEcouleMashing = 0;
        posStart = transform.position;
        activePos = true;
        if (FastTest.debugMashing)
        {
            maxNumberToAim = 6;
        }
    }
    public void SetupMash()
    {
        numberInput = i.Count;
        numberToAim = maxNumberToAim;//hitColliders.Length / ratioMashingToEntities;
        numberToAim = Mathf.Clamp(numberToAim, minNumberToAim, currentmax);
        debugMinRatio = numberToAim * ratioMinimumMashing;
    }

    public void ActiveMashing()
    {
        setMashingActive = true;
        ResetMash();

    }
    public void PropulsionAtFinish()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 150, 1 << 9);
        colliders.Clear();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Rigidbody>() && hitColliders[i].GetComponent<Rigidbody>().useGravity == true)
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
        CinematicCam.StartTransformation(false);

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
