using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private int startingHealth;

    public int currentHealth;
    public Animator anim;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //hurt animation
            //invulnerability
        }



    }

    void Die()
    {
        if (currentHealth <= 0)
        {

            Debug.Log("Enemy Dead!");
            Destroy(gameObject);
            //die animation
        }


    }
}