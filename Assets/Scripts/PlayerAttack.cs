using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Animator animF;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float animationDelay = 0.15f;
    public float playerDamage = 40f;
    private Wall _Wall;
    private EnemyHealth _EnemyHealth;
    [SerializeField] private GameObject fireball;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float reflectPower = 50f;
    [SerializeField] private GameObject wizardEnemy;
    [SerializeField] private GameObject knightEnemy;


    public float attackRate = 2f;
    float nextMeleeTime = 0f;
    float nextFireballTime = 0f;



    void Start()
    {
        anim = GetComponent<Animator>();
        fireball = GameObject.Find("Fireball");
        animF = fireball.GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextMeleeTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(MeleeAttack());
                nextMeleeTime = Time.time + 1f / attackRate;
            }
        }
        
        if (Time.time >= nextFireballTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animF.SetTrigger("Attack");
                StartCoroutine(ThrowFireball());
                nextFireballTime = Time.time + 1f / attackRate;
            }
        }


        // Deal damage

        if (animF == null)
        {
            animF = GetComponent<Animator>();
        }
    }

    

    IEnumerator MeleeAttack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(animationDelay);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            GameObject enemyObject = enemyCollider.gameObject;

            if (enemyObject.CompareTag("Wizard"))
            {
                enemyObject.GetComponent<EnemyHealth>().TakeDamage(playerDamage);
                Debug.Log("Hit wizard for 40!");

            }
            else if (enemyObject.CompareTag("Knight"))
            {
                enemyObject.GetComponent<EnemyHealth>().TakeDamage(playerDamage);
                Debug.Log("Hit knight for 40!");

            }
            else if(enemyObject.CompareTag("Bullet"))
            {
                Debug.Log("Hit bullet.");
                bulletSpeed = enemyObject.GetComponent<Rigidbody2D>().velocity.x;
                if(bulletSpeed > 0) // Directs to right
                {
                    enemyObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-reflectPower,enemyObject.transform.position.y), ForceMode2D.Impulse);
                }
                else if(bulletSpeed < 0) // Directs to left
                {
                    enemyObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(reflectPower, enemyObject.transform.position.y), ForceMode2D.Impulse);

                }
            }
        }

        Collider2D[] hitWalls = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, wallLayer);
        foreach (Collider2D wall in hitWalls)
        {
            Debug.Log($"You hit : {wall}");
            _Wall.TakeDamage(1);
            Debug.Log(_Wall.currentHealth);
        }
    }

    IEnumerator ThrowFireball()
    {
        Instantiate(fireball, new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0);
        

    }
    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
}
