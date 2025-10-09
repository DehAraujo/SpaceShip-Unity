using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    [Header("Configurações do Slow Motion")]
    public float timeBeforeSlow = 20f; // Intervalo de 20 segundos (MODIFICADO)
    public float slowDuration = 6f;    // Duração do efeito (6 segundos) (MODIFICADO)
    public float slowMotionScale = 0.2f; // Velocidade do tempo durante o efeito (20% do normal)

    private float normalTimeScale = 1f;
    private bool isSlowingDown = false;

    void Start()
    {
        // Começa o ciclo de slow motion
        StartCoroutine(SlowMotionCycle());
    }

    IEnumerator SlowMotionCycle()
    {
        while (true)
        {
            // Espera o novo intervalo de 20 segundos
            yield return new WaitForSecondsRealtime(timeBeforeSlow);

            // Inicia o Slow Motion
            StartSlowMotion();

            // Espera a nova duração de 6 segundos
            yield return new WaitForSecondsRealtime(slowDuration);

            // Retorna ao tempo normal
            EndSlowMotion();
        }
    }

    public void StartSlowMotion()
    {
        if (isSlowingDown) return;
        isSlowingDown = true;

        // Define a nova escala de tempo (afeta inimigos e física)
        Time.timeScale = slowMotionScale;
        // Tempo fixo também é afetado para colisões (opcional)
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        Debug.Log("SLOW MOTION INICIADO!");
    }

    public void EndSlowMotion()
    {
        if (!isSlowingDown) return;
        isSlowingDown = false;

        // Retorna a escala de tempo ao normal
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f;

        Debug.Log("SLOW MOTION ENCERRADO.");
    }
}