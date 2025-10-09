using UnityEngine;

public class RaioNave : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 2.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyShip"))
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
