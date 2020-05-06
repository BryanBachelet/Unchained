using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifePlayer : MonoBehaviour
{
    public float maxLife;
    public float currentLife;
    [Header("Regeneration")]
    [Range (0,100)]
    public float startRegenerationLifeLevel = 30;
    public float regenerationLifePerSecond = 1;
        
    [Header("Feedback")]
    public Image uiFeedback;  
    public float speedOfUiFeedback;
    
    
    private float frameDamage = 0;
    private float countOfDamage = 0;
    
    private float frameHealth = 0;
    
    private float ratioHp;

    public float deathTimeBeforeReset= 2;
    [HideInInspector]
    public bool deathState =false;

    private float compteurDeath = 0;


     
    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;   
        ratioHp = GetRatioHealth();     
    }

    // Update is called once per frame
    void Update()
    { 
       if(!deathState)
       {
      
        ApplyLifeChange();
        ResetFrame();
       }else
       {
        Debug.Log("Dead");
        DeathGestion();
       }
        ratioHp = GetRatioHealth();
        uiFeedback.fillAmount = Mathf.Lerp(uiFeedback.fillAmount ,ratioHp,speedOfUiFeedback* Time.deltaTime);
    }

#region Regeneration

    private void RegenerationBehavior()
    {
        if(ratioHp<(startRegenerationLifeLevel/100))
        {
           AddHealth(regenerationLifePerSecond *Time.deltaTime);
        }
    }

#endregion

#region DamageTake

    public void AddHealth(float health)
    {
        frameHealth += health;
    }
    public void AddDamage(float damage)
    {
        frameDamage += damage;
    }
    private void ApplyLifeChange()
    {
        if(!CountOfDamage())
        {
            RegenerationBehavior();
        }
        currentLife +=  (-frameDamage)+ frameHealth; 
        frameDamage =0;
        if(currentLife<=0 )
        {
            PlayerDead();
        }
    }
    private float GetRatioHealth()
    {
        float ratioToReturn = 0 ;
        ratioToReturn = currentLife/maxLife;
     
       
        return  ratioToReturn;
    }

    private bool CountOfDamage()
    {
        if(frameDamage != 0)
        {
            countOfDamage +=frameDamage;
            return true;
        }
        else
        {
            countOfDamage = 0;
            return false;
        }

    }

    private void PlayerDead()
    {
        deathState = true;
    }

    private void DeathGestion()
    {
        if(compteurDeath>deathTimeBeforeReset)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            compteurDeath = 0;
        }
        else
        {
            compteurDeath +=Time.deltaTime;
        }

    }

#endregion
    private void ResetFrame()
    {
        frameHealth = 0;
        frameDamage = 0;
    }

}
