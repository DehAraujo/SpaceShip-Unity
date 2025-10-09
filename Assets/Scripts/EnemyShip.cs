using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Atributos da Nave Inimiga")]
    public int hp = 2;
    public float speed = 2.5f;
    public float shootInterval = 1.5f;

    [Header("Ataque")]
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private float shootTimer = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        Move();
        Shoot();
        CheckOutOfBounds();
    }

    void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void Shoot()
    {
        if (enemyBulletPrefab == null || firePoint == null) return;

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            // Instancia o tiro.
            GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);

            // A nave inimiga AGORA SÓ ATIRA. O dano será tratado pelo tiro.
        }
    }

    void CheckOutOfBounds()
    {
        if (transform.position.x < -12f)
            Destroy(gameObject);
    }

    public void OnHitByPlayer()
    {
        hp--;
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(200);
        }

        if (hp <= 0)
            Destroy(gameObject);
    }

    // O MÉTODO QUE CAUSAVA DANO AO ENCOSTAR FOI REMOVIDO DAQUI.
}