using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage;
    private Vector2 target;
    public bool isIce;
    public bool isFire;

    public void Start()
    {
        Destroy(gameObject, 5f);
    }
    public void Initialize(Vector2 target)
    {
        this.target = target;
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(GameManager.Instance.bulletDamage);

            if (isIce)
            {
                collision.gameObject.GetComponent<Enemy>().StopBurning();
                collision.gameObject.GetComponent<Enemy>().Ice();
            }
            if(isFire)
            {
                collision.gameObject.GetComponent<Enemy>().StartBurning();
            }
            
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        
    }
}

