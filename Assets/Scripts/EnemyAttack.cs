using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool isInRange = false;
    float nextAttackTime = 0f;
    [SerializeField] private float range; // declare later.
    [SerializeField] private float attackCooldown = 2f; // 0 = fireable.
    [SerializeField] private float attackRate = 5f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        attackCooldown -= Time.time;
        if (isInRange && attackCooldown <= 0f)
        {

            StartCoroutine("ThrowFireball");
        }
    }

    IEnumerator ThrowFireball()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0);
        attackCooldown = 2f;

    }
}
