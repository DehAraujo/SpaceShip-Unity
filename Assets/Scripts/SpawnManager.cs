using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    // O Prefab �NICO do Inimigo (aquele com o script Enemy.cs anexado)
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;

    // Configura��o de Limites
    public float minY = -4f;
    public float maxY = 4f;
    public float spawnX = 10f; // Posi��o X de onde o inimigo come�a (borda direita)

    void Start()
    {
        // ... (Verifica��o de c�mera, se voc� usa a vers�o din�mica, use essa) ...
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 1. Define a Posi��o
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);

            // 2. Cria o Inimigo (Instancia)
            GameObject newEnemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // 3. Obt�m o script Enemy
            Enemy enemyScript = newEnemyObj.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                // 4. Decis�o Aleat�ria do Tipo de Inimigo
                // 0: Tipo 1 (B�sico) | 1: Tipo 2 (Senoidal)
                int enemyType = Random.Range(0, 2); // Gera 0 ou 1

                if (enemyType == 1)
                {
                    // Configura��es para INIMIGO AVAN�ADO (Tipo 2 Senoidal)
                    enemyScript.isType2 = true;
                    enemyScript.hp = 3;             // Mais resistente
                    enemyScript.speed = 4f;         // Um pouco mais lento, mas com movimento complexo
                    enemyScript.scoreValue = 300;
                    // Note: Frequency e Magnitude j� est�o no Prefab, mas podem ser ajustados aqui se necess�rio.
                }
                else
                {
                    // Configura��es para INIMIGO B�SICO (Tipo 1 Linear)
                    enemyScript.isType2 = false;
                    enemyScript.hp = 1;
                    enemyScript.speed = 5f;
                    enemyScript.scoreValue = 100;
                }
            }
        }
    }
}