using UnityEngine;

[ExecuteAlways]
public class EffectParallax : MonoBehaviour
{
    [Header("Configuração")]
    public MeshRenderer meshRenderer; // arraste o MeshRenderer aqui
    public float scrollSpeed = 0.1f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        AjustarTamanhoEPosicao();
    }

#if UNITY_EDITOR
    void Update()
    {
        if (!Application.isPlaying)
        {
            AjustarTamanhoEPosicao();
            return;
        }
        Scroll();
    }
#else
    void Update() => Scroll();
#endif

    void AjustarTamanhoEPosicao()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null || meshRenderer == null) return;

        // 1️⃣ Obtém tamanho da câmera ortográfica
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        // 2️⃣ Calcula o tamanho real do material do fundo
        Material mat = meshRenderer.sharedMaterial;
        Texture tex = mat != null ? mat.mainTexture : null;
        float texAspect = 1f;

        if (tex != null)
            texAspect = (float)tex.width / tex.height;

        float bgHeight = camHeight;
        float bgWidth = bgHeight * texAspect;

        // Garante que cubra completamente a câmera
        if (bgWidth < camWidth)
        {
            float ajuste = camWidth / bgWidth;
            bgWidth *= ajuste;
            bgHeight *= ajuste;
        }

        // 3️⃣ Ajusta escala e centraliza exatamente no centro da câmera
        transform.localScale = new Vector3(bgWidth, bgHeight, 1f);
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
    }

    void Scroll()
    {
        if (meshRenderer == null) return;

        Material mat = meshRenderer.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.x += scrollSpeed * Time.deltaTime;
        offset.x = Mathf.Repeat(offset.x, 1f);
        mat.mainTextureOffset = offset;
    }
}
