using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Atributos do Boss")]
    public int hp = 4;
    public float speed = 0.4f;
    public float shootInterval = 1.2f;
    private float shootTimer = 0f;

    [Header("Ataque")]
    public GameObject bulletPrefab;
    public Transform firePoint; // Arraste no inspector um ponto de tiro à frente do boss
    public float bulletSpeed = 10f;

    [Header("Referências")]
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        Move();
        HandleShooting();
    }

    void Move()
    {
        // Move da direita para a esquerda até um limite
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < 6f)
            speed = 0f; // para no lado direito da tela
    }

    void HandleShooting()
    {
        if (player == null || bulletPrefab == null) return;

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        // Instancia o tiro
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calcula direção até o jogador
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Ajusta a rotação do tiro na direção do player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Move o tiro nessa direção
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }

    public void OnHitByPlayer()
    {
        hp--;
        GameManager.instance.DamageBoss(1); // Atualiza UI e pontuação no GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(5000);
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.TakePlayerDamage(1);
        }
    }
}
