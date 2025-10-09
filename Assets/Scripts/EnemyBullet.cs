using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // ESTE É O MÉTODO CORRETO QUE DEVE CAUSAR DANO.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.TakePlayerDamage(1);
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Movimenta o projétil para a ESQUERDA (direção -X)
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Destrói se sair da tela
        if (transform.position.x < -15f || transform.position.x > 15f)
            Destroy(gameObject);
    }
}