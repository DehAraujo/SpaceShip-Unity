using UnityEngine;
using System.Collections; // Necessário para a Coroutine

public class PlayerMovement : MonoBehaviour
{
    // Ajuste a velocidade no Inspector
    public float speed = 15f;

    // Variáveis internas para cálculo de limites (Clamping)
    private Camera mainCamera;
    private float camHeight;
    private float camWidth;
    private float spriteHalfWidth;
    private float spriteHalfHeight;

    // Variáveis para armazenar o input (AGORA SÃO VARIÁVEIS DE CLASSE)
    private float xInput;
    private float yInput;

    // --- Variáveis de Dano ---
    public float invulnerabilityDuration = 2f; // Invulnerabilidade por 2 segundos
    private bool isInvulnerable = false; // Controle de estado
    private SpriteRenderer spriteRenderer; // Para fazer a nave piscar

    void Start()
    {
        // Pega a câmera e calcula as dimensões do mundo
        mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.orthographic)
        {
            camHeight = mainCamera.orthographicSize;
            camWidth = camHeight * mainCamera.aspect;
        }

        // Pega metade do tamanho do sprite para o Clamping ser preciso
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            spriteHalfWidth = renderer.bounds.extents.x;
            spriteHalfHeight = renderer.bounds.extents.y;
        }

        // Pega o componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. INPUT
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        // 2. LIMITE VISUAL
        ClampPosition();
    }

    void FixedUpdate()
    {
        // 1. Calcula a direção
        Vector3 direction = new Vector3(xInput, yInput, 0);

        // 2. MOVIMENTO: Usa o tempo real (Time.unscaledDeltaTime) no FixedUpdate.
        transform.Translate(direction * speed * Time.unscaledDeltaTime);
    }

    // --- Colisão e Dano ---

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se colidiu com um inimigo OU com um tiro inimigo
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            TakeDamage(other.gameObject);
        }
    }

    // LÓGICA DE DANO CORRIGIDA
    void TakeDamage(GameObject otherObject)
    {
        if (isInvulnerable) return;

        // 1. Perde uma vida (Corrigido: Este código estava faltando)
        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife();
        }

        // 2. Destroi o objeto que causou o dano
        if (otherObject != null) // Checagem para evitar o MissingReferenceException anterior
        {
            Destroy(otherObject);
        }

        // 3. Se ainda tiver vidas, inicia a invulnerabilidade (Corrigido: Este código estava faltando)
        if (GameManager.instance != null && GameManager.instance.playerLives > 0)
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
        else
        {
            // Game Over: Destrói a nave do jogador
            Destroy(gameObject);
        }
    } // <--- FECHA A FUNÇÃO TakeDamage (CORRIGIDO)


    /// <summary>
    /// Coroutine para o efeito de piscar e controle de invulnerabilidade.
    /// </summary>
    IEnumerator InvulnerabilityCoroutine() // <--- AGORA FORA DO TakeDamage
    {
        isInvulnerable = true;
        float startTime = Time.time;
        float blinkRate = 0.1f;

        while (Time.time < startTime + invulnerabilityDuration)
        {
            if (spriteRenderer != null)
            {
                // Alterna entre visível/invisível
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }

            // Usa WaitForSecondsRealtime para que o piscar tenha sempre a mesma velocidade
            yield return new WaitForSecondsRealtime(blinkRate);
        }

        // Garante que o sprite esteja visível ao final da invulnerabilidade
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        isInvulnerable = false;
    } // <--- FECHA A FUNÇÃO InvulnerabilityCoroutine


    /// <summary>
    /// Restringe a posição da nave dentro dos limites visíveis da câmera.
    /// </summary>
    void ClampPosition() // <--- AGORA FORA DO TakeDamage
    {
        if (mainCamera == null) return;

        // Calcula os limites
        float minX = mainCamera.transform.position.x - camWidth + spriteHalfWidth;
        float maxX = mainCamera.transform.position.x + camWidth - spriteHalfWidth;
        float minY = mainCamera.transform.position.y - camHeight + spriteHalfHeight;
        float maxY = mainCamera.transform.position.y + camHeight - spriteHalfHeight;

        Vector3 newPosition = transform.position;

        // Mathf.Clamp força o valor X e Y a ficar dentro dos limites definidos
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    } // <--- FECHA A FUNÇÃO ClampPosition


} // <--- CHAVE FINAL DA CLASSE PlayerMovement (ESTA ERA A CHAVE QUE FALTAVA)