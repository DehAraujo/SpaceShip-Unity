using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 8f;

    [Header("Tiro")]
    public GameObject raioNavePrefab;
    public Transform firePoint;
    public float fireRate = 0.25f;
    private float fireTimer;

    // VARIÁVEIS DE ÁUDIO ADICIONADAS
    [Header("Áudio")]
    [SerializeField] private AudioSource audioSource; // Componente para tocar o som
    [SerializeField] private AudioClip shootClip;      // O clipe de áudio do tiro

    private Rigidbody2D rb;
    private float camHeight;
    private float camWidth;

    private float halfWidth;
    private float halfHeight;
    private SpriteRenderer spriteRenderer;

    [Header("Debug")]
    public bool showCameraBounds = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }

        // CORREÇÃO: Obter o AudioSource (garante que ele exista no objeto)
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }


        // Dimensões da câmera
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

        // Leitura da margem do sprite (para limite de tela)
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            halfWidth = spriteRenderer.bounds.extents.x;
            halfHeight = spriteRenderer.bounds.extents.y;
        }
        else
        {
            // Fallback
            halfWidth = 0.5f;
            halfHeight = 0.5f;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (rb != null)
        {
            Vector2 dir = new Vector2(moveX, moveY).normalized;

            // Movimento usando tempo não escalonado (Time.unscaledDeltaTime) para ignorar o slow motion.
            Vector3 movement = dir * speed * Time.unscaledDeltaTime;
            transform.position += movement;
            rb.velocity = Vector2.zero; // Reseta a velocidade para evitar efeitos colaterais.
        }

        Vector3 pos = transform.position;
        // Limita o movimento usando a largura real da nave
        pos.x = Mathf.Clamp(pos.x, -camWidth + halfWidth, camWidth - halfWidth);
        pos.y = Mathf.Clamp(pos.y, -camHeight + halfHeight, camHeight - halfHeight);
        transform.position = pos;
    }

    void HandleShooting()
    {
        // Usa tempo não escalonado para que a taxa de tiro não diminua no slow motion.
        fireTimer += Time.unscaledDeltaTime;

        if (Input.GetKey(KeyCode.Space) && fireTimer >= fireRate)
        {
            fireTimer = 0f;

            // LÓGICA DE ÁUDIO ADICIONADA: Toca o som do tiro
            if (audioSource != null && shootClip != null)
            {
                // PlayOneShot permite que vários tiros toquem sem cortar o som anterior
                audioSource.PlayOneShot(shootClip, 0.5f);
            }

            // Instancia o projétil
            if (raioNavePrefab && firePoint)
                Instantiate(raioNavePrefab, firePoint.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Define as Tags de colisão
        bool isHostileProjectile = other.CompareTag("EnemyBullet") || other.CompareTag("BossBullet");
        bool isHostileContact = other.CompareTag("EnemyShip") || other.CompareTag("Meteor");

        // 1. DANO POR PROJÉTIL: Causa dano e destrói o tiro.
        if (isHostileProjectile)
        {
            if (GameManager.instance != null)
                GameManager.instance.TakePlayerDamage(1);

            // O projétil deve ser destruído
            Destroy(other.gameObject);
        }
        // 2. CONTATO COM INIMIGO/METEORO: Apenas destrói o objeto (sem dano de toque).
        else if (isHostileContact)
        {
            // A nave inimiga/meteoro é destruído
            Destroy(other.gameObject);
        }
    }

    // Debug: desenha limites da câmera (OBTIDO O MÉTODO E COLOCADO DENTRO DA CLASSE)
    void OnDrawGizmos()
    {
        if (!showCameraBounds || Camera.main == null) return;

        Gizmos.color = Color.green;
        float camH = Camera.main.orthographicSize;
        float camW = camH * Camera.main.aspect;

        Vector3 topLeft = new Vector3(-camW, camH, 0f);
        Vector3 topRight = new Vector3(camW, camH, 0f);
        Vector3 bottomLeft = new Vector3(-camW, -camH, 0f);
        Vector3 bottomRight = new Vector3(camW, -camH, 0f);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}