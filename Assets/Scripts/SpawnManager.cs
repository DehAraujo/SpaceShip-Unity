using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    // O Prefab �NICO do Inimigo
    public GameObject inimigobasePrefab;
    public float spawnInterval = 5f;

    // Configura��o de Limites
    public float minY = -4f;
    public float maxY = 4f;
    public float spawnX = 10f; // Posi��o X de onde o inimigo come�a

    void Start()
    {
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
            GameObject newInimigoBaseObj = Instantiate(inimigobasePrefab, spawnPosition, Quaternion.identity);

            // 3. Obt�m o script InimigoBase
            InimigoBase inimigoScript = newInimigoBaseObj.GetComponent<InimigoBase>();

            if (inimigoScript != null)
            {
                // 4. Decis�o Aleat�ria do Tipo de Inimigo
                int inimigoType = Random.Range(0, 3);

                // Zera as flags de tipo
                inimigoScript.isType2 = false;
                inimigoScript.isHeavy = false;

                // === CONFIGURA��O DOS TIPOS ===

                if (inimigoType == 2) // TIPO 3: PESADO (Lento, Muita Vida)
                {
                    inimigoScript.isHeavy = true;
                    inimigoScript.hp = 5;
                    inimigoScript.speed = 2.5f;
                    inimigoScript.scoreValue = 500;
                    inimigoScript.fireRate = 4f;
                }
                else if (inimigoType == 1) // TIPO 2: AVAN�ADO (Senoidal)
                {
                    inimigoScript.isType2 = true;
                    inimigoScript.hp = 3;
                    inimigoScript.speed = 4f;
                    inimigoScript.scoreValue = 300;
                    inimigoScript.fireRate = 2f;
                }
                else // TIPO 1: B�SICO (Linear Padr�o) (enemyType == 0)
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