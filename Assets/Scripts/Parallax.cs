using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Variáveis que você pode ajustar no Inspector
    private float length; // O tamanho horizontal do sprite.
    public float parallaxEffect; // A velocidade do movimento (ex: 0.1 para lento, 1.0 para rápido).

    // --- Parte 1: Início ---
    void Start()
    {
        // Pega a largura exata do Sprite (Bounds.size.x) e armazena em 'length'.
        // Isso é crucial para saber onde teletransportar o sprite.
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // --- Parte 2: A cada Frame ---
    void Update()
    {
        // 1. Move o sprite constantemente para a esquerda (Vector3.left)
        // O movimento é multiplicado por Time.deltaTime para ser suave e por parallaxEffect para controlar a velocidade.
        transform.position += Vector3.left * Time.deltaTime * parallaxEffect;

        // 2. Verifica a Repetição (Looping)
        // Se a posição X do sprite for menor que o negativo do seu próprio tamanho,
        // significa que ele saiu completamente da tela para a esquerda.
        if (transform.position.x < -length)
        {
            // Teletransporta o sprite para o lado direito, logo após o seu ponto de origem.
            // Isso cria a ilusão de repetição infinita.
            transform.position = new Vector3(length, transform.position.y, transform.position.z);
        }
    }
}