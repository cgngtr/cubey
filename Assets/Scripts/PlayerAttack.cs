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
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private int playerDamage;
    [SerializeField] private Enemy enemy1;
    [SerializeField] private Wall wall1;
    [SerializeField] private float animationDelay = 0.15f;

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
                StartCoroutine(MeleeAttack());
                nextAttackTime = Time.time + 1f / attackRate; 
            }
        }
        

        // Deal damage
    }
    IEnumerator MeleeAttack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(animationDelay);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log($"You hit : {enemy}");
            enemy1.TakeDamage(40);
        }
        Collider2D[] hitWalls = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, wallLayer);
        foreach (Collider2D wall in hitWalls)
        {
            Debug.Log($"You hit : {wall}");
            wall1.TakeDamage(1);
        }
    }

    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
