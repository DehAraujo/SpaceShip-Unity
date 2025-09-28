using UnityEngine; // CRÍTICO: Inclui todos os comandos básicos do Unity (como Destroy, Debug, etc.)

public class Bullet : MonoBehaviour // CRÍTICO: Define a classe e herda do MonoBehaviour
{
    // A velocidade da bala e o dano que ela causa
    public float speed = 30f;
    public int damageAmount = 1;

    void Update()
    {
        // Movimento para frente, usando Time.unscaledDeltaTime para ignorar o Slow Motion
        transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime);
    }

    // Aprimorado para buscar o script Enemy de forma mais robusta (como no passo anterior)
    void OnTriggerEnter2D(Collider2D other) // CRÍTICO: Usa o tipo Collider2D que o Unity exige
    {
        // 1. Verifica se o objeto atingido tem a Tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Tenta buscar o script Enemy no GameObject do Collider atingido
            Enemy enemy = other.GetComponent<Enemy>();

            // Se o script não for encontrado no Collider, tente buscá-lo no objeto pai
            if (enemy == null && other.transform.parent != null)
            {
                enemy = other.transform.parent.GetComponent<Enemy>();
            }

            // 2. Se o script Enemy for encontrado, causa dano
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }

            // 3. Destrói a bala (o objeto DO SCRIPT)
            Destroy(gameObject);
        }
    }

    // Opcional: Adiciona uma lógica para destruir a bala se ela sair da tela
    void CheckBoundaryAndDestroy()
    {
        // ... (Seu código de destruição de borda aqui, se aplicável) ...
    }
}