using UnityEngine;

public class RaioBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    void Start()
    {
        // Garante que o projétil seja destruído após o tempo de vida
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // CORREÇÃO: Usa Time.unscaledDeltaTime para IGNORAR o slow motion
        // O tiro mantém a velocidade real.
        transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime);

        // Destroi se sair da margem direita da tela
        if (transform.position.x > 12f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Lógica de colisão unificada do projétil:

        // 1. Acerto na Nave Inimiga
        if (other.CompareTag("EnemyShip"))
        {
            other.GetComponent<EnemyShip>()?.OnHitByPlayer();
            Destroy(gameObject);
        }
        // 2. Acerto no Meteoro
        else if (other.CompareTag("Meteor"))
        {
            other.GetComponent<Meteor>()?.OnHitByPlayer();
            Destroy(gameObject);
        }
        // 3. Acerto no Boss
        else if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>()?.OnHitByPlayer();
            Destroy(gameObject);
        }
        // 4. Acerto em Inimigos Comuns (usando a tag genérica 'Enemy')
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}