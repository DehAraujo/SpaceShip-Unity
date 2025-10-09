using UnityEngine;

public class EfectParallaxStars : MonoBehaviour
{
    [Header("Configuração")]
    public float velocidadeScroll = 1f;
    public Transform segundoFundo;

    private float larguraDoObjeto;
    private float camWidth;

    void Start()
    {
        // Largura do fundo
        Renderer renderer = GetComponent<Renderer>();
        larguraDoObjeto = renderer.bounds.size.x;

        // Largura da câmera
        camWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        transform.Translate(Vector3.left * velocidadeScroll * Time.deltaTime);

        // Limite esquerdo: quando o fundo sai da tela
        if (transform.position.x <= -camWidth - larguraDoObjeto / 2f)
        {
            ReposicionarFundo();
        }
    }

    void ReposicionarFundo()
    {
        float novaPosX = segundoFundo.position.x + larguraDoObjeto;
        transform.position = new Vector3(novaPosX, transform.position.y, transform.position.z);
    }
}
