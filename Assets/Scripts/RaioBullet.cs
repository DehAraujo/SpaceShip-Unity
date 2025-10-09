    using UnityEngine;

    public class RaioBullet : MonoBehaviour
    {
        public float speed = 10f;
        public float lifetime = 3f;

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
            else if (other.CompareTag("Player"))
            {
                GameManager.instance.TakePlayerDamage(1);
                Destroy(gameObject);
            }
        }
    }
