using UnityEngine;
using TMPro; // Necessário para TextMeshPro
using System.Collections; // Necessário para Coroutines (IEnumerator)

public class GameManager : MonoBehaviour
{
    // --- Variáveis de Pontuação ---
    public int score = 0;
    public TextMeshProUGUI scoreText;
    private float timeScoreTimer = 0f; // Timer para pontuação por tempo
    public int scorePerSecond = 1;     // Pontos por segundo (Regra: Sobreviver)

    // --- Variáveis de Time Warp ---
    public float slowMotionFactor = 0.3f; // Efeito no jogo: x 0.3
    public float slowMotionDuration = 5f; // Duração: 5 segundos
    private float normalTimeScale = 1.0f;
    private float normalFixedDeltaTime;

    // O Singleton garante que apenas uma instância deste script exista
    public static GameManager instance;

    void Awake()
    {
        // Configuração do Singleton
        if (instance == null)
        {
            instance = this;
            // Salva o valor padrão do FixedDeltaTime aqui!
            normalFixedDeltaTime = Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 1. Pontuação por Tempo (Usando Time.deltaTime para ser suave e consistente)
        timeScoreTimer += Time.deltaTime;
        if (timeScoreTimer >= 1f)
        {
            score += scorePerSecond;
            UpdateScoreDisplay();
            timeScoreTimer = 0f; // Reseta o timer
        }

        // 2. Gatilho de Slow Motion (Pode ser chamado aqui para verificar pontuação em tempo real)
        // OBS: Você pode chamar este método no AddScore() para maior eficiência.
        CheckForSlowTime();
    }

    // --- Métodos de Pontuação ---
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
        // Pode verificar o gatilho aqui após cada pontuação
        // CheckForSlowTime(); 
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Pontuação: " + score.ToString();
        }
    }

    // --- Métodos de Time Warp ---

    // Gatilho: Atingir 1000 pontos (ou Power-up)
    public void CheckForSlowTime()
    {
        // Se a pontuação for o gatilho (e ainda não está em Slow Time)
        if (score >= 1000 && Time.timeScale == normalTimeScale)
        {
            ActivateSlowTime();
            // Ação: Subtrair a pontuação como "custo" para usar a habilidade.
            score -= 1000;
            UpdateScoreDisplay();
        }
    }

    // Método principal para iniciar o Slow Motion
    public void ActivateSlowTime()
    {
        // Evita iniciar novamente se já estiver ativo
        if (Time.timeScale != normalTimeScale) return;

        StartCoroutine(SlowTimeCoroutine());

        // **Visual/Feedback:** Implemente aqui a mudança visual (ex: Tela azulada)
    }

    // Coroutine para gerenciar a duração do efeito
    IEnumerator SlowTimeCoroutine()
    {
        // Aplica o Slow Motion
        Time.timeScale = slowMotionFactor;

        // Importante para a Física: Ajusta o fixedDeltaTime
        Time.fixedDeltaTime = normalFixedDeltaTime * Time.timeScale;

        // Espera pela duração (em tempo real)
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Restaura o tempo normal
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime;

        // **Visual/Feedback:** Restaura o visual normal
    }
}