using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ... (Suas variáveis de HP, Pontuação e Movimento) ...
    public int hp = 1;
    public int scoreValue = 100;
    public float speed = 5f;
    public bool isType2 = false;
    public float frequency = 1f;
    public float magnitude = 2f;
    private Vector3 initialPosition;

    // Variável para a destruição de borda
    private float destructionBoundary;

    void Start()
    {
        initialPosition = transform.position;

        // Configuração da Borda de Destruição
        Camera mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.orthographic)
        {
            // Calcula a borda esquerda da tela
            float camHeight = mainCamera.orthographicSize;
            float camWidth = camHeight * mainCamera.aspect;

            // Define o limite de destruição um pouco fora da borda esquerda
            destructionBoundary = mainCamera.transform.position.x - camWidth - 1f;
        }
    }

    void Update()
    {
        // === Lógica de Movimento (Usa Time.deltaTime, afetado pelo Slow Motion) ===
        if (isType2)
        {
            // ... (Movimento Tipo 2 Senoidal) ...
            Vector3 tempPos = initialPosition;
            tempPos.y += Mathf.Sin(Time.time * frequency) * magnitude;
            tempPos.x -= speed * Time.deltaTime;
            transform.position = tempPos;
        }
        else
        {
            // ... (Movimento Tipo 1 Linear) ...
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // === Verificação de Borda para Destruição ===
        CheckBoundaryAndDestroy();
    }

    // ... (Seus métodos TakeDamage e Die) ...
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(scoreValue);
        }
        Destroy(gameObject);
    }

    // Novo método para verificar a borda e substituir OnBecameInvisible()
    void CheckBoundaryAndDestroy()
    {
        if (transform.position.x < destructionBoundary)
        {
            Destroy(gameObject);
        }
    }

    // Removemos OnBecameInvisible(), pois a nova lógica é mais robusta.
}