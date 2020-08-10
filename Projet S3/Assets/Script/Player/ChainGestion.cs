using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainGestion : MonoBehaviour
{
    public GameObject[] chainsPlayer =  new GameObject[3];
    private MouseScope mouseScope;
    private EnnemiStock ennemiStock;

    private LineRend lineRend;
    public SkinnedMeshRenderer[] matChain = new SkinnedMeshRenderer[3];
    public int debugChainActive;
    // Start is called before the first frame update
    void Start()
    {
        mouseScope = GetComponent<MouseScope>();
        ennemiStock = GetComponent<EnnemiStock>();
        lineRend  = GetComponentInChildren<LineRend>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mouseScope.instanceBullet != null && ennemiStock.ennemiStock == null) 
        {
          PosChain(lineRend.pointplayer.transform.position, mouseScope.instanceBullet.transform.position);
        }
        if(ennemiStock.ennemiStock != null) 
        {
          PosChain(lineRend.pointplayer.transform.position, ennemiStock.ennemiStock.transform.position);
        }
        if(ennemiStock.ennemiStock == null && mouseScope.instanceBullet ==null||StateOfGames.currentState == StateOfGames.StateOfGame.Transformation)
        {
            PosChain();
        }
    }

    public void PosChain()
    {
        for(int i = 0 ; i<chainsPlayer.Length;i++)
        {
                chainsPlayer[i].SetActive(false);

        }
    }

    public void PosChain(Vector3 startPos, Vector3 finishPos)
    {
        float distance = Vector3.Distance(startPos,finishPos);
        int currentChain = Mathf.FloorToInt(distance/ 33);
        debugChainActive  = currentChain;
        Vector3 dir = finishPos - startPos; 
        dir =  new Vector3(0,0,dir.z);
        float angle = Vector3.SignedAngle(Vector3.forward, dir.normalized, Vector3.up);
        for(int i = 0 ; i<chainsPlayer.Length;i++)
        {
            if(i>currentChain)
            {   
                chainsPlayer[i].SetActive(false);

            }else
            {
                chainsPlayer[i].SetActive(true);
            }
                chainsPlayer[i].transform.position = startPos + transform.forward * (33 *i);
                float currentDis =  Vector3.Distance(chainsPlayer[i].transform.position,finishPos);
                matChain[i].material.SetFloat("_Radius", currentDis);     
        }
        
    }
}
