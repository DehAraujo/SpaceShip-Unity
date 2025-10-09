using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Meteor, Ship }
    [Header("Tipo")]
    public EnemyType type;

    [Header("Atributos")]
    public int hp = 1;
    public float moveSpeed = 2f;

    [Header("Tiro (só Ship)")]
    public GameObject enemyBulletPrefab;
    public float fireRate = 2f;
    private float fireTimer;

    private void Start()
    {
        fireTimer = fireRate;
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        if (type == EnemyType.Meteor)
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        else if (type == EnemyType.Ship)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.y < -6f || transform.position.x < -12f) Destroy(gameObject);
    }

    void HandleShooting()
    {
        if (enemyBulletPrefab == null || type != EnemyType.Ship) return;
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Instantiate(enemyBulletPrefab, transform.position + Vector3.down * 0.6f, Quaternion.identity);
            fireTimer = fireRate;
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Die();
    }

    void Die()
    {
        if (GameManager.instance != null)
        {
            if (type == EnemyType.Meteor)
                GameManager.instance.AddScore(100);
            else if (type == EnemyType.Ship)
                GameManager.instance.AddScore(500);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.TakePlayerDamage(1);
            Die();
        }
    }
}
