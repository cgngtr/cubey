using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 3;
    public SpriteRenderer sr;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    /*5AFF8F yesil 90 255 143 255
      FFBA5A turuncu 255 186 90 255
      D4234A kirmizi 212 35 74 255
    */ 

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //anim.SetTrigger("Hurt");
        if(currentHealth == 2)
        {
            // Yellow
            sr.color = Color.yellow;
        }
        if (currentHealth == 1)
        {
            // Red
            sr.color = Color.red;
        }

        if (currentHealth <= 0)
        {
            Break();
        }

    }

    public void Break()
    {
        Debug.Log("Wall broke!");
        //anim.SetBool("IsDead", true);
        Destroy(gameObject);
    }
}
