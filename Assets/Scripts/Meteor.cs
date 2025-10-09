using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 3f;
    public int hp = 1;
    private float camHeight;

    void Start()
    {
        camHeight = Camera.main.orthographicSize;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -camHeight - 1f)
            Destroy(gameObject);
    }

    public void OnHitByPlayer()
    {
        hp--;
        if (hp <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(100);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.TakePlayerDamage(1);
            Destroy(gameObject);
        }
    }
}
