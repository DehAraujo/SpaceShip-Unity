using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshRenderer))]
public class BackgroundAlignToCamera : MonoBehaviour
{
    [Header("Configuração")]
    public MeshRenderer meshRenderer; // arraste o MeshRenderer do fundo aqui
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

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        // Ajusta a escala do fundo pra caber na câmera
        transform.localScale = new Vector3(camWidth, camHeight, 1f);

        // Centraliza o fundo na posição da câmera
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
