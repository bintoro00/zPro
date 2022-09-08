using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator anim;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if(x>0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        if(x<0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        anim.SetFloat("Speed",Mathf.Abs(x));
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    void Attack()
    {
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        anim.SetTrigger("IsAttack");

        foreach(Collider2D hitEnemy in hitEnemys)
        {
            Debug.Log(hitEnemy.gameObject.name + "に攻撃");
            hitEnemy.GetComponent<EnemyController>().OnDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
