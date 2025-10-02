using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f;
    public int damageAmount = 1;

    private Rigidbody2D rb;

    // NOVO: Armazena a direção para onde a bala deve se mover
    private Vector2 moveDirection;

    // Limite de destruição
    private float destructionBoundary = -20f;

    void Start()
    {
        // Pega o Rigidbody e aplica a velocidade imediatamente
        rb = GetComponent<Rigidbody2D>();

        // Se a direção foi definida, aplica a velocidade
        if (rb != null)
        {
            rb.velocity = moveDirection * speed;
        }
    }

    // NOVO: Método que o inimigo chamará para definir a direção
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; // Normaliza para que o vetor tenha comprimento 1

        // Se o Rigidbody já estiver disponível (se Start() já rodou), aplica a velocidade
        if (rb != null)
        {
            rb.velocity = moveDirection * speed;
        }
    }

    void Update()
    {
        // Chama a checagem de limite no Update
        CheckBoundaryAndDestroy();
    }

    // --- Colisão e Destruição ---

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // O dano ao jogador é tratado no PlayerMovement.cs
            Destroy(gameObject);
        }
    }

    // MÉTODO REQUERIDO PELO UPDATE
    void CheckBoundaryAndDestroy()
    {
        // Se a posição X ou Y for muito extrema, destrói
        if (transform.position.x < destructionBoundary ||
            transform.position.x > 20f ||
            Mathf.Abs(transform.position.y) > 10f)
        {
            Destroy(gameObject);
        }
    }
}