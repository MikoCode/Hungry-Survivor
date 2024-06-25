using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float damage = 10;
    public float health = 30;
    public GameObject exp;
    private Transform player;
    private Rigidbody2D rb;
    public bool isBurning;
    private int burnDamage = 10;
    private float burnInterval = 1f;
    public GameObject fireParticle;
    public GameObject iceParticle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = (Vector2)player.position - rb.position;
        direction.Normalize();
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Implement death logic
        Instantiate(exp, this.transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }

        
    }

    public void Ice()
    {
        moveSpeed *= 0.5f;
        iceParticle.gameObject.SetActive(true);

    }

    public void StartBurning()
    {
        if (!isBurning)
        {
            isBurning = true;
            StartCoroutine(Burn());
            Invoke("StopBurning", 3.1f);
            fireParticle.gameObject.SetActive(true);
        }
    }

    public void StopBurning()
    {
        if (isBurning)
        {
            isBurning = false;
            StopCoroutine(Burn());
            fireParticle.gameObject.SetActive(false);
        }
    }

    private IEnumerator Burn()
    {
        while (isBurning)
        {
            yield return new WaitForSeconds(burnInterval);
            
            TakeDamage(burnDamage);
        }
    }

}
