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
    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;   
         ratioHp = GetRatioHealth();     
    }

    // Update is called once per frame
    void Update()
    { 
        Debug.Log("Etape 1 = " + currentLife);
        ratioHp = GetRatioHealth();
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddDamage(95);
        }
        ApplyLifeChange();
        uiFeedback.fillAmount = Mathf.Lerp(uiFeedback.fillAmount ,ratioHp,speedOfUiFeedback* Time.deltaTime);
        ResetFrame();
    }

#region Regeneration

    private void RegenerationBehavior()
    {
        Debug.Log(startRegenerationLifeLevel/100 + " // " + GetRatioHealth());
        if(ratioHp<(startRegenerationLifeLevel/100))
        {
           AddHealth(regenerationLifePerSecond *Time.deltaTime);
        }
    }

#endregion

#region DamageTake

    public void AddHealth(float health)
    {
        frameHealth = health;
    }
    public void AddDamage(float damage)
    {
        frameDamage = damage;
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
     
        Debug.Log("Currrent Life = " + currentLife + " // " + ratioToReturn + " // " + maxLife);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endregion
    private void ResetFrame()
    {
        frameHealth = 0;
        frameDamage = 0;
    }

}
