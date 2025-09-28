using UnityEngine;
using TMPro; // Necess�rio para TextMeshPro
using System.Collections; // Necess�rio para Coroutines (IEnumerator)

public class GameManager : MonoBehaviour
{
    // --- Vari�veis de Pontua��o ---
    public int score = 0;
    public TextMeshProUGUI scoreText;
    private float timeScoreTimer = 0f; // Timer para pontua��o por tempo
    public int scorePerSecond = 1;     // Pontos por segundo (Regra: Sobreviver)

    // --- Vari�veis de Time Warp ---
    public float slowMotionFactor = 0.3f; // Efeito no jogo: x 0.3
    public float slowMotionDuration = 5f; // Dura��o: 5 segundos
    private float normalTimeScale = 1.0f;
    private float normalFixedDeltaTime;

    // O Singleton garante que apenas uma inst�ncia deste script exista
    public static GameManager instance;

    void Awake()
    {
        // Configura��o do Singleton
        if (instance == null)
        {
            instance = this;
            // Salva o valor padr�o do FixedDeltaTime aqui!
            normalFixedDeltaTime = Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 1. Pontua��o por Tempo (Usando Time.deltaTime para ser suave e consistente)
        timeScoreTimer += Time.deltaTime;
        if (timeScoreTimer >= 1f)
        {
            score += scorePerSecond;
            UpdateScoreDisplay();
            timeScoreTimer = 0f; // Reseta o timer
        }

        // 2. Gatilho de Slow Motion (Pode ser chamado aqui para verificar pontua��o em tempo real)
        // OBS: Voc� pode chamar este m�todo no AddScore() para maior efici�ncia.
        CheckForSlowTime();
    }

    // --- M�todos de Pontua��o ---
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
        // Pode verificar o gatilho aqui ap�s cada pontua��o
        // CheckForSlowTime(); 
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Pontua��o: " + score.ToString();
        }
    }

    // --- M�todos de Time Warp ---

    // Gatilho: Atingir 1000 pontos (ou Power-up)
    public void CheckForSlowTime()
    {
        // Se a pontua��o for o gatilho (e ainda n�o est� em Slow Time)
        if (score >= 1000 && Time.timeScale == normalTimeScale)
        {
            ActivateSlowTime();
            // A��o: Subtrair a pontua��o como "custo" para usar a habilidade.
            score -= 1000;
            UpdateScoreDisplay();
        }
    }

    // M�todo principal para iniciar o Slow Motion
    public void ActivateSlowTime()
    {
        // Evita iniciar novamente se j� estiver ativo
        if (Time.timeScale != normalTimeScale) return;

        StartCoroutine(SlowTimeCoroutine());

        // **Visual/Feedback:** Implemente aqui a mudan�a visual (ex: Tela azulada)
    }

    // Coroutine para gerenciar a dura��o do efeito
    IEnumerator SlowTimeCoroutine()
    {
        // Aplica o Slow Motion
        Time.timeScale = slowMotionFactor;

        // Importante para a F�sica: Ajusta o fixedDeltaTime
        Time.fixedDeltaTime = normalFixedDeltaTime * Time.timeScale;

        // Espera pela dura��o (em tempo real)
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Restaura o tempo normal
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime;

        // **Visual/Feedback:** Restaura o visual normal
    }
}