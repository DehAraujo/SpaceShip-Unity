using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    // A velocidade com que a camada se move (ajuste no Inspector)
    public float scrollSpeed = 0.5f;

    // A largura exata do seu sprite de fundo em World Units.
    // É CRUCIAL que este valor esteja correto para o loop funcionar!
    public float spriteWidth = 20.48f; // Exemplo: 2048 pixels / 100 PPU

    // Onde a camada começou (posição X inicial)
    private Vector3 startPosition;

    void Start()
    {
        // Armazena a posição inicial da camada (deve ser 0, 0, 15)
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. Calcula o movimento: Movendo para a esquerda, afetado pelo Slow Motion (Time.deltaTime)
        float movement = scrollSpeed * Time.deltaTime;

        // 2. Aplica o movimento à camada (move o objeto pai e seus dois sprites filhos)
        transform.Translate(Vector3.left * movement);

        // 3. Verifica o Loop: Se o primeiro sprite saiu totalmente da tela
        // Se a posição X da camada for menor ou igual ao negativo da largura do sprite,
        // significa que o sprite já foi totalmente visto.
        if (transform.position.x <= -spriteWidth)
        {
            // Reseta a posição da camada, alinhando-a com o segundo sprite para o loop infinito
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        // Define a nova posição X como a posição X inicial (startPosition.x, que é 0)
        // Isso coloca a camada de volta na frente do sprite que está visível.
        transform.position = startPosition;
    }
}