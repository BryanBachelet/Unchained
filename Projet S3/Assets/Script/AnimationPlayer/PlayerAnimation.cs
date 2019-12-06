using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    public Animation animPlayer;
    [System.Serializable]
    public struct AnimBoard
    {
        public string name;
        public AnimationClip animationClip;
    }
    public List<AnimBoard> animArray;
  
    // Start is called before the first frame update
    void Start()
    {
        ////animator = GetComponent<Animator>();
        animPlayer = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayAnim("Walk");
        }
    }

   public void PlayAnim( string nameAnim)
    {
        for (int i = 0; i < animArray.Count; i++)
        {
            if( animArray[i].name == nameAnim)
            {
                animPlayer.Play(animArray[i].animationClip.name);
             
            }
        }
    }

}
