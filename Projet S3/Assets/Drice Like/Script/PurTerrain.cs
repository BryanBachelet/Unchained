using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurTerrain : MonoBehaviour
{
    public List<Collider> plaque = new List<Collider>();
    public PlayerControl1 player;
    public float timingofCorupt = 1;
    public float compteur;
    public Collider[] test;
    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<PlayerControl1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.speed > player.highSpeed)
        {
        test = Physics.OverlapSphere(transform.position, 5.5f);
        for (int i = 0; i < test.Length; i++)
        {

            if (!test[i].GetComponent<PurTerrain>() && plaque.IndexOf(test[i]) == -1)
            {
                plaque.Add(test[i]);
            }

        }
            if (compteur > timingofCorupt && plaque.Count > 0)
            {
                int i = Random.Range(0, plaque.Count - 1);
                GameObject target = plaque[i].gameObject;
                plaque.RemoveAt(i);
                target.GetComponent<Renderer>().material.color = Color.white;
                target.AddComponent<PurTerrain>();
                Corupt cor = target.GetComponent<Corupt>();
                Destroy(cor);
                compteur = 0;

            }
            else
            {
                compteur += Time.deltaTime;
            }
        }
    }
}
