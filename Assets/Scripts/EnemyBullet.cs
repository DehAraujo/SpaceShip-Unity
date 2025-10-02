using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f;
    public int damageAmount = 1;

    private Rigidbody2D rb;

    // NOVO: Armazena a dire��o para onde a bala deve se mover
    private Vector2 moveDirection;

    // Limite de destrui��o
    private float destructionBoundary = -20f;

    void Start()
    {
        // Pega o Rigidbody e aplica a velocidade imediatamente
        rb = GetComponent<Rigidbody2D>();

        // Se a dire��o foi definida, aplica a velocidade
        if (rb != null)
        {
            rb.velocity = moveDirection * speed;
        }
    }

    // NOVO: M�todo que o inimigo chamar� para definir a dire��o
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; // Normaliza para que o vetor tenha comprimento 1

        // Se o Rigidbody j� estiver dispon�vel (se Start() j� rodou), aplica a velocidade
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

    // --- Colis�o e Destrui��o ---

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // O dano ao jogador � tratado no PlayerMovement.cs
            Destroy(gameObject);
        }
    }

    // M�TODO REQUERIDO PELO UPDATE
    void CheckBoundaryAndDestroy()
    {
        // Se a posi��o X ou Y for muito extrema, destr�i
        if (transform.position.x < destructionBoundary ||
            transform.position.x > 20f ||
            Mathf.Abs(transform.position.y) > 10f)
        {
            Destroy(gameObject);
        }
    }
}