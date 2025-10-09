using UnityEngine;

public class ScrollInfinito : MonoBehaviour
{
    public float velocidade = 2f;
    public Transform[] fundos;

    private float larguraDoFundo;
    private float inicioPosicaoX;

    void Start()
    {
        if (fundos == null || fundos.Length == 0)
        {
            Debug.LogError("❌ Nenhum fundo atribuído no ParallaxController!");
            enabled = false;
            return;
        }

        // Calcula o tamanho real do sprite (não depende do collider)
        SpriteRenderer sr = fundos[0].GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("❌ O fundo precisa ter um SpriteRenderer!");
            enabled = false;
            return;
        }

        larguraDoFundo = sr.bounds.size.x;
        inicioPosicaoX = fundos[0].position.x;
    }

    void Update()
    {
        // Move todos os fundos para a esquerda
        foreach (Transform fundo in fundos)
        {
            fundo.Translate(Vector2.left * velocidade * Time.deltaTime);

            // Se o fundo sair totalmente da tela, move ele para o final da fila
            if (fundo.position.x <= inicioPosicaoX - larguraDoFundo)
            {
                float deslocamento = larguraDoFundo * fundos.Length;
                fundo.position += new Vector3(deslocamento, 0f, 0f);
            }
        }
    }
}
