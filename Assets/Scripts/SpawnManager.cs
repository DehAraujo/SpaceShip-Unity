using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    // O Prefab ÚNICO do Inimigo (aquele com o script Enemy.cs anexado)
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;

    // Configuração de Limites
    public float minY = -4f;
    public float maxY = 4f;
    public float spawnX = 10f; // Posição X de onde o inimigo começa (borda direita)

    void Start()
    {
        // ... (Verificação de câmera, se você usa a versão dinâmica, use essa) ...
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 1. Define a Posição
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);

            // 2. Cria o Inimigo (Instancia)
            GameObject newEnemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // 3. Obtém o script Enemy
            Enemy enemyScript = newEnemyObj.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                // 4. Decisão Aleatória do Tipo de Inimigo
                // 0: Tipo 1 (Básico) | 1: Tipo 2 (Senoidal)
                int enemyType = Random.Range(0, 2); // Gera 0 ou 1

                if (enemyType == 1)
                {
                    // Configurações para INIMIGO AVANÇADO (Tipo 2 Senoidal)
                    enemyScript.isType2 = true;
                    enemyScript.hp = 3;             // Mais resistente
                    enemyScript.speed = 4f;         // Um pouco mais lento, mas com movimento complexo
                    enemyScript.scoreValue = 300;
                    // Note: Frequency e Magnitude já estão no Prefab, mas podem ser ajustados aqui se necessário.
                }
                else
                {
                    // Configurações para INIMIGO BÁSICO (Tipo 1 Linear)
                    enemyScript.isType2 = false;
                    enemyScript.hp = 1;
                    enemyScript.speed = 5f;
                    enemyScript.scoreValue = 100;
                }
            }
        }
    }
}