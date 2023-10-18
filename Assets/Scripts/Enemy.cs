using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public Animator anim;

    public int maxHealth = 100;
    int currentHealth;
    private Animator anim;

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
    }
}
