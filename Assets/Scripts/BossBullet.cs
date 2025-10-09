using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move na direção da rotação atual
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ADICIONE A CHECAGEM DE NULL AQUI
            if (GameManager.instance != null)
            {
                GameManager.instance.TakePlayerDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
