using UnityEngine; // CR�TICO: Inclui todos os comandos b�sicos do Unity (como Destroy, Debug, etc.)

public class Bullet : MonoBehaviour // CR�TICO: Define a classe e herda do MonoBehaviour
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
    void OnTriggerEnter2D(Collider2D other) // CR�TICO: Usa o tipo Collider2D que o Unity exige
    {
        // 1. Verifica se o objeto atingido tem a Tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Tenta buscar o script Enemy no GameObject do Collider atingido
            Enemy enemy = other.GetComponent<Enemy>();

            // Se o script n�o for encontrado no Collider, tente busc�-lo no objeto pai
            if (enemy == null && other.transform.parent != null)
            {
                enemy = other.transform.parent.GetComponent<Enemy>();
            }

            // 2. Se o script Enemy for encontrado, causa dano
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }

            // 3. Destr�i a bala (o objeto DO SCRIPT)
            Destroy(gameObject);
        }
    }

    // Opcional: Adiciona uma l�gica para destruir a bala se ela sair da tela
    void CheckBoundaryAndDestroy()
    {
        // ... (Seu c�digo de destrui��o de borda aqui, se aplic�vel) ...
    }
}