using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public GameObject enemyShipPrefab;
    public float spawnRate = 2f;
    [Range(0f, 1f)] public float meteorChance = 0.6f;

    private float timer;
    private float camHeight;
    private float camWidth;

    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float rand = Random.value;

        if (rand < meteorChance)
        {
            // Meteoro: de cima pra baixo
            float x = Random.Range(-camWidth + 0.5f, camWidth - 0.5f);
            float y = camHeight + 1f;
            Instantiate(meteorPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        }
        else
        {
            // EnemyShip: da direita pra esquerda
            float y = Random.Range(-camHeight + 0.5f, camHeight - 0.5f);
            float x = camWidth + 1f;
            Instantiate(enemyShipPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        }
    }
}
