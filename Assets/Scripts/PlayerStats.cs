using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int health;
    [SerializeField] private int breath = 10;

    public void HealDamage(int damageAmount)
    {
        health += damageAmount;
    }
        public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {


        if(health <= 0)
        {
            HandlePlayerDeath();
        }
    }

    private void HandlePlayerDeath()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
