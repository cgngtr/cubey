using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float currentHealth;
    public float maxHealth;
    private Animator anim;

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
        yield return new WaitForSeconds(0);
    }

}
