using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : EnemyController
{
    public GameObject arrowPrefab;
    public float arrowSpeed;
    public int numberOfArrows;
    private int arrowsFired = 0;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    if (transform.position.x < targetPosition.x)
    //    {
    //        targetPosition.x = 0 - targetPosition.x;
    //        base.Flip();
    //    }
    //    player = GameObject.FindWithTag("Player");
    //    numberOfArrows = Random.Range(1, 5);
    //}

    private void Awake()
    {
        numberOfArrows = Random.Range(1, 5);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public override void Attack()
    {
        if(arrowsFired < numberOfArrows)
        {
            GameObject projectile = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            projectile.layer = LayerMask.NameToLayer("Projectile");
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(1f, 0f);
            projectile.transform.rotation = Quaternion.Euler(0f, 180f, 90f);
            if (!facingRight)
            { 
                direction = new Vector2(-1f, 0f);
                projectile.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            rb.velocity = direction * arrowSpeed;
            arrowsFired++;
        }
        else
        {
            if (facingRight)
            {
                targetPosition.x = -12;
            }
            else
            {
                targetPosition.x = 12;
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if(gameObject)
            {
                Destroy(gameObject, 3.0f);
            }
        }

    }
}
