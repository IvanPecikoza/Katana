using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : EnemyController
{
    public Vector2 meleeTargetPosition;
    public GameObject shurikenPrefab;
    public float shurikenSpeed;
    private bool shurikenThrown;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public override void Attack()
    {
        if (!shurikenThrown)
        {
            GameObject projectile = Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            projectile.layer = LayerMask.NameToLayer("Projectile");
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(1f, 0f);
            if (!facingRight)
            { direction = new Vector2(-1f, 0f); }
            rb.velocity = direction * shurikenSpeed;
            shurikenThrown = true;
            if (facingRight)
            {
                targetPosition.x = 0 - meleeTargetPosition.x;
            }
            else
            {
                targetPosition.x = meleeTargetPosition.x;
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        }
        else
        {
            base.Attack();
        }
    }
}
