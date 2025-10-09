using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime);
        if (transform.position.x > 12f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("EnemyShip"))
        {
            other.GetComponent<EnemyShip>()?.OnHitByPlayer();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Meteor"))
        {
            other.GetComponent<Meteor>()?.OnHitByPlayer();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>()?.OnHitByPlayer();
            Destroy(gameObject);
        }
    }
}
