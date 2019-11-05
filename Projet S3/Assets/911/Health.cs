using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhealth;
    public int currentHealth;

   

    public void HealthDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void MaxHealth()
    {
        currentHealth = maxhealth;
    }
}
