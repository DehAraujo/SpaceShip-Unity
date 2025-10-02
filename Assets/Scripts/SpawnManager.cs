using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    // O Prefab ÚNICO do Inimigo
    public GameObject inimigobasePrefab;
    public float spawnInterval = 5f;

    // Configuração de Limites
    public float minY = -4f;
    public float maxY = 4f;
    public float spawnX = 10f; // Posição X de onde o inimigo começa

    void Start()
    {
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
            GameObject newInimigoBaseObj = Instantiate(inimigobasePrefab, spawnPosition, Quaternion.identity);

            // 3. Obtém o script InimigoBase
            InimigoBase inimigoScript = newInimigoBaseObj.GetComponent<InimigoBase>();

            if (inimigoScript != null)
            {
                // 4. Decisão Aleatória do Tipo de Inimigo
                int inimigoType = Random.Range(0, 3);

                // Zera as flags de tipo
                inimigoScript.isType2 = false;
                inimigoScript.isHeavy = false;

                // === CONFIGURAÇÃO DOS TIPOS ===

                if (inimigoType == 2) // TIPO 3: PESADO (Lento, Muita Vida)
                {
                    inimigoScript.isHeavy = true;
                    inimigoScript.hp = 5;
                    inimigoScript.speed = 2.5f;
                    inimigoScript.scoreValue = 500;
                    inimigoScript.fireRate = 4f;
                }
                else if (inimigoType == 1) // TIPO 2: AVANÇADO (Senoidal)
                {
                    inimigoScript.isType2 = true;
                    inimigoScript.hp = 3;
                    inimigoScript.speed = 4f;
                    inimigoScript.scoreValue = 300;
                    inimigoScript.fireRate = 2f;
                }
                else // TIPO 1: BÁSICO (Linear Padrão) (enemyType == 0)
                {
                    inimigoScript.hp = 1;
                    inimigoScript.speed = 5f;
                    inimigoScript.scoreValue = 100;
                    inimigoScript.fireRate = 1f;
                }

                // 5. APLICA O VISUAL
                inimigoScript.SetVisuals();
            }
        }
    }
}