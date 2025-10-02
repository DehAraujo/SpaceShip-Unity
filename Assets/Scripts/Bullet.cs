using UnityEngine;

public class Bullet : MonoBehaviour
{
    // A velocidade da bala e o dano que ela causa
    public float speed = 30f;
    public int damageAmount = 1;

    // Limite de posição para destruir a bala (ex: 20 unidades para a direita)
    private float destructionBoundary = 20f;

    // NOVO: Adiciona log de diagnóstico para confirmar o estado do Slow Motion
    void Start()
    {
        Debug.Log($"Bullet criado. TimeScale atual: {Time.timeScale}. Se for baixo (ex: 0.05), o Slow Motion está ATIVO, e o tiro deve ser rápido.");
    }

    void Update()
    {
        // Movimento para frente, usando Time.unscaledDeltaTime para ignorar o Slow Motion
        transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime);

        // Chama o método de destruição
        CheckBoundaryAndDestroy();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verifica se o objeto atingido tem a Tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Tenta buscar o script InimigoBase no GameObject do Collider atingido
            InimigoBase enemy = other.GetComponent<InimigoBase>(); // <-- CORREÇÃO AQUI

            // Se o script não for encontrado no Collider, tente buscá-lo no objeto pai
            if (enemy == null && other.transform.parent != null)
            {
                enemy = other.transform.parent.GetComponent<InimigoBase>(); // <-- CORREÇÃO AQUI
            }

            // 2. Se o script InimigoBase for encontrado, causa dano
            if (enemy != null)
            {
                // Note: O TakeDamage deve ser implementado em Enemy.cs (ou InimigoBase.cs)
                enemy.TakeDamage(damageAmount);
            }

            // 3. Destrói a bala (o objeto DO SCRIPT)
            Destroy(gameObject);
        }
    }

    // LÓGICA AGORA COMPLETA: Destroi a bala se ela sair da tela
    void CheckBoundaryAndDestroy()
    {
        // Se a posição X da bala for maior que o limite (a borda direita)
        if (transform.position.x > destructionBoundary)
        {
            Destroy(gameObject);
        }
    }
}
