using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private EnemyAttack _EnemyAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("target");
        StartCoroutine(Shoot());
        _EnemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        
    }

    IEnumerator Shoot()
    {
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        yield return null;
        Destroy(this.gameObject,3);
     }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("a");
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(_EnemyAttack.enemyDamage);
            Debug.Log($"Hit player for {_EnemyAttack.enemyDamage}");
        }
        Destroy(this.gameObject);
    }
}
