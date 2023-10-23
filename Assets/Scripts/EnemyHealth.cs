using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    private Animator anim;
    private float animationDelay = 1.5f;

    void Start()    
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //anim.SetTrigger("Hurt");
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        Debug.Log("Enemy died!");
        anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(animationDelay);

        Debug.Log("Disabling Collider and Object...");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }
}
