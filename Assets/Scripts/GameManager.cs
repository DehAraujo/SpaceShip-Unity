// GameManager.cs - C�DIGO FINAL COM UPGRADE

using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ... (Vari�veis de Vidas e Pontua��o - MANTIDAS)
    public int playerLives = 3;
    public TextMeshProUGUI livesText;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    private float timeScoreTimer = 0f;
    public int scorePerSecond = 1;

    // --- Vari�veis de Time Warp ---
    [Header("Slow Motion Control")]
    public float slowMotionFactor = 0.05f;
    private float normalTimeScale = 1f;
    private float normalFixedDeltaTime;

    // NOVO: Controle de Evento do Slow Motion
    [Header("Slow Motion Game Event")]
    public float initialSlowMotionDelay = 20f; // Come�a aos 20s
    public float slowMotionDuration = 10f; // Dura��o: 10 segundos (Alterado)
    private bool hasSlowMotionTriggered = false; // Garante que o evento s� ocorra uma vez

    // --- Vari�veis de Upgrade de Nave ---
    [Header("Upgrade Settings")]
    public int scoreUpgradeLevel2 = 1000;
    public int scoreUpgradeLevel3 = 2000;

    private bool upgradedToLevel2 = false;
    private bool upgradedToLevel3 = false;


    public static GameManager instance;
    public string gameOverSceneName = "GameOverScene";
    public string winSceneName = "Vencedor";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            normalFixedDeltaTime = Time.fixedDeltaTime;

            UpdateLivesDisplay();
            UpdateScoreDisplay();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 1. Pontua��o por Tempo (MANTIDO)
        timeScoreTimer += Time.deltaTime;
        if (timeScoreTimer >= 1f)
        {
            AddScore(scorePerSecond);
            timeScoreTimer = 0f;
        }

        // NOVO: 2. L�gica de Evento - In�cio Autom�tico do Slow Motion
        if (!hasSlowMotionTriggered && Time.time >= initialSlowMotionDelay)
        {
            hasSlowMotionTriggered = true;
            StartCoroutine(SlowTimeCoroutine());
        }

        // NOVO: 3. L�gica de Upgrade de Nave por Pontua��o
        if (!upgradedToLevel2 && score >= scoreUpgradeLevel2)
        {
            TriggerUpgrade(2);
        }
        else if (!upgradedToLevel3 && score >= scoreUpgradeLevel3)
        {
            TriggerUpgrade(3);
        }

        // 4. CONDI��O DE VIT�RIA (MANTIDO)
        if (score >= 2600)
        {
            LoadWinScene();
        }
    }

    // NOVO: M�todo para acionar o Upgrade
    void TriggerUpgrade(int targetLevel)
    {
        // 1. Localiza a nave com a tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            // Tenta obter o script PlayerUpgrade da nave
            PlayerUpgrade upgradeScript = playerObj.GetComponent<PlayerUpgrade>();

            if (upgradeScript != null && upgradeScript.currentLevel < targetLevel) // Verifica se o upgrade � realmente um avan�o
            {
                upgradeScript.PerformUpgrade();

                // Marca o upgrade como feito para n�o disparar novamente
                if (targetLevel == 2) upgradedToLevel2 = true;
                else if (targetLevel == 3) upgradedToLevel3 = true;
            }
        }
    }


    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Pontua��o: " + score.ToString();
        }
    }

    public void LoseLife()
    {
        if (playerLives <= 0) return;

        playerLives--;
        UpdateLivesDisplay();

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    void UpdateLivesDisplay()
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + playerLives.ToString();
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER! Sua pontua��o final: " + score);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = normalFixedDeltaTime;
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void LoadWinScene()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = normalFixedDeltaTime;
        SceneManager.LoadScene(winSceneName);
    }

    // --- M�todo de Slow Warp�
    IEnumerator SlowTimeCoroutine()
    {
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = normalFixedDeltaTime * Time.timeScale;
        Debug.Log($"Time Warp ATIVADO! TimeScale setado para {slowMotionFactor}.");

        yield return new WaitForSecondsRealtime(slowMotionDuration);

        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime;
        Debug.Log("Time Warp RESTAURADO! TimeScale setado para 1.0.");
    }
}