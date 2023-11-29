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
        // Checking the direction of the player updatingly
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        // Applying the speed.
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        yield return null;
        // Destroying game object after 3 seconds.
        Destroy(this.gameObject,3);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("a");
            Destroy(this.gameObject);
            //Debug.Log($"Hit player for {_EnemyAttack.enemyDamage}");
            //collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(_EnemyAttack.enemyDamage);
        }
    }
}
