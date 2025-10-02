using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    // Arraste os Prefabs de n�vel superior aqui no Inspector
    public GameObject naveLevel2Prefab;
    public GameObject naveLevel3Prefab;

    // Vari�vel para rastrear o n�vel atual da nave
    public int currentLevel = 1;

    /// <summary>
    /// M�todo para acionar a troca de nave (chamado pelo GameManager)
    /// </summary>
    public void PerformUpgrade()
    {
        // Garante que n�o est� no n�vel m�ximo
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
            // 1. Instancia a nova nave na posi��o e rota��o da nave atual
            Instantiate(nextNavePrefab, transform.position, transform.rotation);

            // 2. Destr�i a nave atual (este objeto)
            Destroy(gameObject);

            Debug.Log($"UPGRADE COMPLETO: Nave avan�ou para o N�vel {nextLevel}");
        }
    }
}