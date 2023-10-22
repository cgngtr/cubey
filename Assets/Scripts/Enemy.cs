using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public PlayerAttack _PlayerAttack;
    public int maxHealth;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //anim.SetTrigger("Hurt");

        if(currentHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        //anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }

}

public class Knight : Enemy
{
    public Knight()
    {
        maxHealth = 120;
        TakeDamage(_PlayerAttack.playerDamage);
    }
    
}

public class Wizard : Enemy
{
    public Wizard()
    {
        maxHealth = 80;
        TakeDamage(_PlayerAttack.playerDamage);
    }
}

public class Example
{
    public void PrintMaxHealth()
    {
        Knight knight = new Knight();
        Debug.Log(knight.maxHealth);
    }
}

