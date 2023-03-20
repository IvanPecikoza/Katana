using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; // enemy movement speed
    public float attackRange = 1f; // range for triggering the attack
    public GameObject player; // reference to the player object

    private bool isAttacking = false; // flag to prevent multiple attacks
    public Vector2 targetPosition; // target position to move towards
    public Transform attackPoint;
    public float attackDelay = 1.0f;

    protected bool facingRight = false;
    public LayerMask playerLayer;

    void Start()
    {
        if(transform.position.x < targetPosition.x)
        {
            targetPosition.x = 0 - targetPosition.x;
            Flip();
        }
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (!isAttacking)
        {
            // move towards target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // calculate distance to player
            float distance = Vector2.Distance(transform.position, targetPosition);

            // check if within attack range
            if (distance <= 0)
            {
                isAttacking = true;
                Attack();
                StartCoroutine(DelayAttack(attackDelay));
            }
        }
    }

    IEnumerator DelayAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    public virtual void Attack()
    {
        //TO DO: meele attack behaviour
        //cooldown if ()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            foreach (Collider2D hit in hits)
            {
                Debug.Log("hit" + hit.name);
                Destroy(hit.gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
