using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPlayerSpeed : MonoBehaviour
{
   public List<GameObject> environnement;
    public PlayerControl1 player;
    public float timingofCorupt = 3;
    public float compteur;
    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
          
                if (compteur > timingofCorupt)
                {
                    int i = Random.Range(0, environnement.Count - 1);
                    GameObject target = environnement[i];
                    target.GetComponent<Renderer>().material.color = Color.black;
                    target.AddComponent<Corupt>();
                PurTerrain cor = target.GetComponent<PurTerrain>();
                Destroy(cor);
                hit = true;

                }
                else
                {
                    compteur += Time.deltaTime;
                }
            }
        }
        
    }

