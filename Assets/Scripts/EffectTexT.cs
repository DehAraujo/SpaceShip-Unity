using UnityEngine;
using TMPro; // Necessário para acessar o componente TextMeshPro
using System.Collections; // Necessário para Coroutines

public class EffectText : MonoBehaviour
{
    private TextMeshProUGUI tmPro;
    private Vector3 originalScale;

    [Header("Efeito Pop-up")]
    public float scaleFactor = 1.2f; // O quanto o texto vai crescer
    public float scaleDuration = 0.1f;

    [Header("Efeito Dano")]
    public Color damageColor = Color.red;
    public float flashDuration = 0.3f; // Duração total do piscar

    void Start()
    {
        tmPro = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;
    }

    // Usado para Pontuação: Faz o texto crescer e voltar ao normal.
    public void AnimatePop()
    {
        StopCoroutine("ScaleRoutine");
        StartCoroutine("ScaleRoutine");
    }

    IEnumerator ScaleRoutine()
    {
        // Pop-up: Cresce
        float timer = 0f;
        while (timer < scaleDuration)
        {
            float scale = Mathf.Lerp(originalScale.x, originalScale.x * scaleFactor, timer / scaleDuration);
            transform.localScale = new Vector3(scale, scale, 1f);
            timer += Time.deltaTime;
            yield return null;
        }

        // Retorna ao tamanho original (instantâneo)
        transform.localScale = originalScale;
    }

    // Usado para Vidas: Pisca a cor vermelha e volta ao normal.
    public void AnimateDamageFlash()
    {
        StopCoroutine("FlashRoutine");
        StartCoroutine("FlashRoutine");
    }

    IEnumerator FlashRoutine()
    {
        Color originalColor = tmPro.color;

        // Pisca para vermelho
        tmPro.color = damageColor;
        yield return new WaitForSeconds(flashDuration / 2f);

        // Volta para a cor original
        tmPro.color = originalColor;
        yield return new WaitForSeconds(flashDuration / 2f);

        tmPro.color = originalColor; // Garante que a cor original seja restaurada
    }
}