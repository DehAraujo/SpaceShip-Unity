using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("Configuração de Movimento")]
    // Velocidade de rolagem (pode ser ajustada no Inspector)
    public float scrollSpeed = 0.1f;

    // Referência ao material do seu fundo
    private Material backgroundMaterial;

    void Start()
    {
        // Pega a referência do Material do componente Mesh Renderer/Sprite Renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Pega uma cópia da instância do material para evitar modificar o Asset original
            backgroundMaterial = renderer.material;
        }
    }

    void Update()
    {
        if (backgroundMaterial == null) return;

        // 1. Calcula o novo offset no eixo X (Horizontal)
        // O tempo atual é usado para garantir movimento suave e contínuo.
        float offset = Time.time * scrollSpeed;

        // 2. Aplica o novo offset ao material
        // "_MainTex" é o nome padrão da textura principal no Shader Unlit/Texture.
        backgroundMaterial.mainTextureOffset = new Vector2(offset, 0);
    }

    void OnDisable()
    {
        // Importante: Quando o objeto é desativado ou destruído, 
        // reinicie o offset para evitar problemas se o material for reutilizado.
        if (backgroundMaterial != null)
        {
            backgroundMaterial.mainTextureOffset = Vector2.zero;
        }
    }
}