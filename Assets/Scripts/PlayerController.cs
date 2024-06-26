using UnityEngine;
using UnityEngine.Events;
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

    private Vector2 initialPosition;
    private float baseInterval;
    private float baseUpgradeInterval;
    private float baseMoveSpeed;
    private int baseProjectileType;
    private int baseMaxHealth;
    private bool baseCanShoot;
    private bool baseDoubleShoot;

    [SerializeField] 
    private UnityEvent OnShoot;
    [SerializeField] 
    private UnityEvent OnHeal;
    [SerializeField] 
    private UnityEvent OnTakeDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        // Zapisywanie wartości bazowych
        baseInterval = interval;
        baseUpgradeInterval = upgradeInterval;
        baseMoveSpeed = moveSpeed;
        baseProjectileType = projectileType;
        baseMaxHealth = maxHealth;
        baseCanShoot = canShoot;
        baseDoubleShoot = doubleShoot;

        // Inicjalizacja bieżących wartości
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
            projectile2.GetComponent<Projectile>().Initialize(new Vector2(targetPosition.x + Random.Range(-2, 3), targetPosition.y));
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
        OnTakeDamage?.Invoke();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        currentHealth += heal;
        healthBar.UpdateHealth(currentHealth / maxHealth);
        OnHeal?.Invoke();
    }

    void Die()
    {
        // Implement death logic
        Debug.Log("Player Died");
        GameManager.Instance.OnPlayerDie?.Invoke();
    }

    private void Shoot()
    {
        interval -= Time.deltaTime;

        if (canShoot && interval <= 0)
        {
            if (doubleShoot == false)
            {
                SingleShot();
                interval = upgradeInterval;
            }
            else
            {
                DoubleShot();
                interval = upgradeInterval;
            }
            
            OnShoot?.Invoke();
        }
    }

    [ContextMenu("Reset Player")] 
    public void ResetPlayer()
    {
        transform.position = initialPosition;
        currentHealth = baseMaxHealth;
        moveInput = Vector2.zero;
        canShoot = baseCanShoot;
        doubleShoot = baseDoubleShoot;
        interval = baseInterval;
        upgradeInterval = baseUpgradeInterval;
        moveSpeed = baseMoveSpeed;
        projectileType = baseProjectileType;
        healthBar.UpdateHealth(1); // Assuming health bar takes a normalized value
        Debug.Log("Player has been reset to initial settings.");
    }
}
