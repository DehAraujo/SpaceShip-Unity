using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    // Arraste os Prefabs de nível superior aqui no Inspector
    public GameObject naveLevel2Prefab;
    public GameObject naveLevel3Prefab;

    // Variável para rastrear o nível atual da nave
    public int currentLevel = 1;

    /// <summary>
    /// Método para acionar a troca de nave (chamado pelo GameManager)
    /// </summary>
    public void PerformUpgrade()
    {
        // Garante que não está no nível máximo
        if (currentLevel >= 3) return;

        GameObject nextNavePrefab = null;
        int nextLevel = currentLevel + 1;

        // Determina qual Prefab instanciar
        if (nextLevel == 2 && naveLevel2Prefab != null)
        {
            nextNavePrefab = naveLevel2Prefab;
        }
        else if (nextLevel == 3 && naveLevel3Prefab != null)
        {
            nextNavePrefab = naveLevel3Prefab;
        }

        if (nextNavePrefab != null)
        {
            // 1. Instancia a nova nave na posição e rotação da nave atual
            Instantiate(nextNavePrefab, transform.position, transform.rotation);

            // 2. Destrói a nave atual (este objeto)
            Destroy(gameObject);

            Debug.Log($"UPGRADE COMPLETO: Nave avançou para o Nível {nextLevel}");
        }
    }
}