using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Ajuste a velocidade no Inspector
    public float speed = 15f;

    // Vari�veis internas para c�lculo de limites (Clamping)
    private Camera mainCamera;
    private float camHeight;
    private float camWidth;
    private float spriteHalfWidth;
    private float spriteHalfHeight;

    void Start()
    {
        // Pega a c�mera e calcula as dimens�es do mundo
        mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.orthographic)
        {
            camHeight = mainCamera.orthographicSize;
            camWidth = camHeight * mainCamera.aspect;
        }

        // Pega metade do tamanho do sprite para o Clamping ser preciso
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            spriteHalfWidth = renderer.bounds.extents.x;
            spriteHalfHeight = renderer.bounds.extents.y;
        }
    }

    void Update()
    {
        // 1. INPUT: L� as setas (Left/Right para Horizontal, Up/Down para Vertical)
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        // 2. MOVIMENTO: Usa Time.unscaledDeltaTime para IGNORAR o Slow Motion
        Vector3 movement = new Vector3(xInput, yInput, 0f) * speed * Time.unscaledDeltaTime;
        transform.position += movement;

        // 3. LIMITE: Restringe a nave dentro da tela
        ClampPosition();
    }

    void ClampPosition()
    {
        if (mainCamera == null) return;

        // Calcula os limites reais da tela, subtraindo o tamanho do sprite
        float minX = mainCamera.transform.position.x - camWidth + spriteHalfWidth;
        float maxX = mainCamera.transform.position.x + camWidth - spriteHalfWidth;
        float minY = mainCamera.transform.position.y - camHeight + spriteHalfHeight;
        float maxY = mainCamera.transform.position.y + camHeight - spriteHalfHeight;

        Vector3 newPosition = transform.position;

        // Mathf.Clamp for�a o valor X e Y a ficar dentro dos limites definidos
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}