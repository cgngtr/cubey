using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int playerDamage;
    [SerializeField] public Enemy enemy1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;



    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.time >= nextAttackTime) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate; 
            }
        }
        

        // Deal damage
    }
    void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log($"You hit : {enemy}");
            enemy1.TakeDamage(40);
        }
    }

    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
