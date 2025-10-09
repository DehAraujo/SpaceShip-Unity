using UnityEngine;

public class BossRaio : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    private float camHeight;
    private float camWidth;

    void Start()
    {
        // Define um vetor de direção aleatória (diagonal ou horizontal/vertical)
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void Update()
    {
        // Movimento
        transform.Translate(direction * speed * Time.deltaTime);

        Vector3 pos = transform.position;

        // Rebater nas bordas horizontais
        if (pos.x > camWidth || pos.x < -camWidth)
        {
            direction.x *= -1;
            pos.x = Mathf.Clamp(pos.x, -camWidth, camWidth);
        }

        // Rebater nas bordas verticais
        if (pos.y > camHeight || pos.y < -camHeight)
        {
            direction.y *= -1;
            pos.y = Mathf.Clamp(pos.y, -camHeight, camHeight);
        }

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Dano no Player
        if (other.CompareTag("Player"))
        {
            GameManager.instance.TakePlayerDamage(1);
        }
    }
}
