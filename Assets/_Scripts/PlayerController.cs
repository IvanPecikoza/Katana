using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight;

    //Attack atributes
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private bool isAttacking = false;
    private float attackStartTime;
    public float attackDelay = 0.5f;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !isAttacking)
            {
                isAttacking = true;
                if (touch.position.x < Screen.width / 2) // Check if touch is on the left half of the screen
                {
                    if(facingRight)
                    { Flip(); }
                    transform.position += new Vector3(-0.5f, 0f, 0f);
                    Attack();
                    StartCoroutine(DelayedPositionChange(0.5f, 0.5f));
                }
                else
                {
                    if (!facingRight)
                    { Flip(); }
                    transform.position += new Vector3(+0.5f, 0f, 0f);
                    Attack();
                    StartCoroutine(DelayedPositionChange(0.5f, -0.5f));
                }
            }
        }

        IEnumerator DelayedPositionChange(float delay, float direction)
        {
            yield return new WaitForSeconds(delay);
            transform.position += new Vector3(direction, 0f, 0f);
            isAttacking = false;
        }

    }


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void Attack()
    {
        //TO DO: meele attack behaviour
        //cooldown if ()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("hit" + enemy.name);
                Destroy(enemy.gameObject);

            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {

            //GameObject collidedObject = collision.gameObject;

            // List all the components of the collided object
            //Component[] components = collidedObject.GetComponents<Component>();
            //foreach (Component component in components)
            //{
            //    Debug.Log("Component: " + component.GetType().Name);
            //}

            //if(projectile != null)
            //{
            //    Debug.Log("projectile not null");
            //}
            Destroy(gameObject);

            // Destroy the projectile
            Destroy(collision.gameObject);
            gameManager.GameOver();
        }
    }
}
