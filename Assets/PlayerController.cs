using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float interval, upgradeInterval;
    public float moveSpeed = 5f;
    public int projectileType;
    public int maxHealth = 100;
    public GameObject[] projectilePrefab;
    public float currentHealth;
    public bool canShoot, doubleShoot;
    private Rigidbody2D rb;
    public Vector2 moveInput;
    public HealthBar healthBar;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnFire()
    {
        if (moveInput == Vector2.zero)
        {
            canShoot = true;
           
        }
        else
        {
            canShoot = false;
        }
    }

    void FixedUpdate()
    {
        OnFire();
        Shoot();

        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
      
    }

    void SingleShot()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Vector2 targetPosition = nearestEnemy.transform.position;
            GameObject projectile = Instantiate(projectilePrefab[projectileType], transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(targetPosition); ;
        }
    }

    
    public void DoubleShot()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Vector2 targetPosition = nearestEnemy.transform.position;
           

            GameObject projectile = Instantiate(projectilePrefab[projectileType], transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(targetPosition);
            GameObject projectile2 = Instantiate(projectilePrefab[projectileType], transform.position, Quaternion.identity);
            projectile2.GetComponent<Projectile>().Initialize(new Vector2(targetPosition.x + Random.Range(-2,3), targetPosition.y));
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealth(currentHealth / maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        currentHealth += heal;
        healthBar.UpdateHealth(currentHealth / maxHealth);
    }

    void Die()
    {
        // Implement death logic
        Debug.Log("Player died");
    }

    private void Shoot()
    {
        
        interval -= Time.deltaTime;

        if (canShoot && interval <= 0)
        {

            if(doubleShoot == false)
            {
                SingleShot();
                interval = upgradeInterval;
            }
            else
            {
                DoubleShot();
                interval = upgradeInterval;
            }
           

            
        }
    }
}
