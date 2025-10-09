using UnityEngine;
using TMPro; // IMPORTANTE: Precisa deste 'using'

public class EfeitoTexto : MonoBehaviour
{
    private TextMeshProUGUI texto;
    private Vector3 escalaOriginal;
    public float tempoDeEfeito = 0.1f;
    public float fatorDeAumento = 1.2f;
    public Color corDano = Color.red;

    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        escalaOriginal = transform.localScale;
    }

    public void AtivarPiscarDano()
    {
        StopAllCoroutines();
        StartCoroutine(FlashColor());
    }

    System.Collections.IEnumerator FlashColor()
    {
        Color corOriginal = texto.color;

        // Pisca para a cor de dano
        texto.color = corDano;
        yield return new WaitForSeconds(0.1f);

        // Volta para a cor original
        texto.color = corOriginal;
        yield return new WaitForSeconds(0.1f);

        // Repete se necessário, ou termina.
        // Para um piscar simples, basta o código acima.
    }
    // Chame esta função sempre que a pontuação ou vida mudar
    public void AtivarEfeito()
    {
        // Interrompe qualquer rotina de efeito anterior para evitar falhas
        StopAllCoroutines();
        StartCoroutine(AnimateScale());
    }

    System.Collections.IEnumerator AnimateScale()
    {
        // 1. Aumenta o texto (escala maior)
        transform.localScale = escalaOriginal * fatorDeAumento;

        // 2. Espera um momento
        yield return new WaitForSeconds(tempoDeEfeito);

        // 3. Retorna à escala original
        transform.localScale = escalaOriginal;
    }
}