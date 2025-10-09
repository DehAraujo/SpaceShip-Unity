using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text bossLivesText;
    public TMP_Text finalScoreText;

    [Header("Player Stats")]
    public int playerLives = 3;
    public int score;

    [Header("Boss Config")]
    public GameObject bossPrefab;
    private GameObject currentBoss;
    private int bossLives = 4;
    private bool bossSpawned = false;

    private bool isGameOver = false;
    private float damageCooldown = 0.8f;
    private float lastDamageTime = -999f;

    private Spawner spawner;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        // Chamado apenas na primeira vez que o objeto é criado
        InitializeGameStart();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. Sempre busca as referências de UI da cena atual
        InitializeSceneReferences(scene);

        if (scene.name == "SampleScene")
        {
            // 2. Reinicia o estado do jogo apenas ao carregar a cena principal ("SampleScene")
            InitializeGameStart();

            // Garantimos que o Spawner está habilitado ao iniciar o jogo
            if (spawner != null)
                spawner.enabled = true;
        }

        // 3. Garante que a UI seja atualizada imediatamente com o novo estado (3 vidas, 0 score)
        UpdateUI();

        // 4. Se estiver em uma cena de placar, carrega o placar final
        if (scene.name == "GameOverScene" || scene.name == "VictoryScene")
        {
            LoadFinalScore();
            if (finalScoreText != null) finalScoreText.gameObject.SetActive(true);
        }
    }

    // Função para redefinir todas as variáveis de jogo para um novo início
    void InitializeGameStart()
    {
        playerLives = 3;
        score = 0;
        bossSpawned = false;
        isGameOver = false;
        bossLives = 4;
    }

    // Função para encontrar referências de objetos na nova cena carregada
    void InitializeSceneReferences(Scene scene)
    {
        scoreText = GameObject.Find("Score")?.GetComponent<TMP_Text>();
        livesText = GameObject.Find("Lives:")?.GetComponent<TMP_Text>();
        bossLivesText = GameObject.Find("BossLivesText")?.GetComponent<TMP_Text>();
        finalScoreText = GameObject.Find("FinalScoreTxt")?.GetComponent<TMP_Text>();

        // CORREÇÃO DO AVISO CS0618: Usando FindFirstObjectByType no lugar de FindObjectOfType
        spawner = FindFirstObjectByType<Spawner>();

        if (bossLivesText != null)
        {
            // Esconde a vida do Boss por padrão
            bossLivesText.gameObject.SetActive(false);
        }
        if (finalScoreText != null)
        {
            // Esconde o placar final por padrão, exceto nas cenas de game over
            finalScoreText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!bossSpawned && score >= 2000)
        {
            SpawnBoss();
        }
    }


    // ====== BOSS ======
    void SpawnBoss()
    {
        bossSpawned = true;

        if (spawner != null)
            spawner.enabled = false;

        Vector3 spawnPos = new Vector3(10f, 0f, 0f);
        currentBoss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        bossLives = 4;

        if (bossLivesText != null)
        {
            bossLivesText.text = "BOSS LIVES: " + bossLives;
            bossLivesText.gameObject.SetActive(true);
        }
    }

    public void DamageBoss(int amount)
    {
        if (!bossSpawned) return;

        bossLives -= amount;
        if (bossLives <= 0)
        {
            bossLives = 0;
            score += 5000;
            UpdateUI();

            if (currentBoss != null)
                Destroy(currentBoss);

            ShowFinalScoreAndWin();
        }
        else
        {
            score += 2000;
            UpdateUI();
        }
    }

    // ====== SCORE ======
    public void AddScore(int value)
    {
        if (isGameOver) return;
        score += value;
        UpdateUI();
    }

    // ====== PLAYER ======
    public void TakePlayerDamage(int amount)
    {
        if (isGameOver) return;
        if (Time.time - lastDamageTime < damageCooldown) return;

        lastDamageTime = Time.time;
        playerLives -= amount;

        if (playerLives <= 0)
        {
            playerLives = 0;
            GameOver();
        }

        UpdateUI();
    }

    // ====== INTERFACE ======
    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score;

        if (livesText != null)
            livesText.text = "LIVES: " + playerLives;

        if (bossLivesText != null)
        {
            if (bossSpawned)
            {
                bossLivesText.text = "BOSS LIVES: " + bossLives;
                bossLivesText.gameObject.SetActive(true);
            }
            else
            {
                bossLivesText.gameObject.SetActive(false);
            }
        }
    }

    // ====== GAME STATES ======
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        SceneManager.LoadScene("GameOverScene");
    }

    void ShowFinalScoreAndWin()
    {
        if (isGameOver) return;
        isGameOver = true;

        PlayerPrefs.SetInt("FinalScore", score);
        SceneManager.LoadScene("VictoryScene");
    }

    public void LoadFinalScore()
    {
        if (finalScoreText != null)
        {
            int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
            finalScoreText.text = "FINAL SCORE: " + finalScore;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void ReturnToMenu()
    {
        InitializeGameStart();
        SceneManager.LoadScene("LoaderScene");
    }
}