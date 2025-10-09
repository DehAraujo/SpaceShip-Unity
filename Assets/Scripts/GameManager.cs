using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections; // Necessário para Coroutines (atrasos)

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TMP_Text scoreText; // ÚNICA DECLARAÇÃO MANTIDA
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
    public AudioClip bossSpawnSound; // Efeito sonoro de alerta

    private bool isGameOver = false;
    private float damageCooldown = 0.8f;
    private float lastDamageTime = -999f;

    private float scoreTimer = 0f;
    public float scoreInterval = 1f;

    private Spawner spawner;
    private AudioSource backgroundMusicSource; // Referência à música de fundo

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
            if (backgroundMusicSource != null) backgroundMusicSource.Stop();
            Destroy(gameObject);
        }

        InitializeGameStart();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeSceneReferences(scene);

        if (scene.name == "SampleScene")
        {
            InitializeGameStart();
            if (spawner != null)
                spawner.enabled = true;

            if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
            {
                backgroundMusicSource.Play();
            }
        }

        UpdateUI();

        if (scene.name == "GameOverScene" || scene.name == "VictoryScene")
        {
            LoadFinalScore();
            if (finalScoreText != null) finalScoreText.gameObject.SetActive(true);

            if (backgroundMusicSource != null) backgroundMusicSource.Stop();
        }
    }

    void InitializeGameStart()
    {
        playerLives = 3;
        score = 0;
        bossSpawned = false;
        isGameOver = false;
        bossLives = 4;
        scoreTimer = 0f;
    }

    void InitializeSceneReferences(Scene scene)
    {
        scoreText = GameObject.Find("Score")?.GetComponent<TMP_Text>();
        livesText = GameObject.Find("Lives")?.GetComponent<TMP_Text>();
        bossLivesText = GameObject.Find("BossLivesText")?.GetComponent<TMP_Text>();
        finalScoreText = GameObject.Find("FinalScoreTxt")?.GetComponent<TMP_Text>();

        spawner = FindFirstObjectByType<Spawner>();
        GameObject bgmObject = GameObject.Find("BackgroundMusic");
        if (bgmObject != null)
        {
            backgroundMusicSource = bgmObject.GetComponent<AudioSource>();
        }

        if (bossLivesText != null)
            bossLivesText.gameObject.SetActive(false);
        if (finalScoreText != null)
            finalScoreText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= scoreInterval)
            {
                score++;
                scoreTimer = 0f;
                UpdateUI();
            }

            if (!bossSpawned && score >= 4000)
            {
                bossSpawned = true;
                StartCoroutine(PreBossCleanup(1f));
            }
        }
    }

    // ====== BOSS ENTRANCE SEQUENCE (Coroutine) ======

    private IEnumerator PreBossCleanup(float preSpawnDelay)
    {
        DestroyAllEnemies();
        yield return new WaitForSeconds(preSpawnDelay);
        SpawnBossAction();
    }

    void DestroyObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    void DestroyAllEnemies()
    {
        if (spawner != null)
            spawner.enabled = false;

        DestroyObjectsWithTag("EnemyShip");
        DestroyObjectsWithTag("Meteor");
        DestroyObjectsWithTag("Enemy");
        DestroyObjectsWithTag("EnemyBullet");
        DestroyObjectsWithTag("BossBullet");
    }

    void SpawnBossAction()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }

        Vector3 spawnPos = new Vector3(10f, 0f, 0f);
        GameObject bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        currentBoss = bossInstance;
        bossLives = 4;

        if (bossSpawnSound != null)
        {
            AudioSource.PlayClipAtPoint(bossSpawnSound, Camera.main.transform.position);
        }

        AudioSource bossAudio = currentBoss.GetComponent<AudioSource>();
        if (bossAudio != null)
        {
            bossAudio.Play();
        }

        if (bossLivesText != null)
        {
            bossLivesText.text = "BOSS LIVES: " + bossLives;
            bossLivesText.gameObject.SetActive(true);
        }
    }

    // ====== BOSS DEATH SEQUENCE (Coroutine) ======

    private IEnumerator BossDeathSequence(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowFinalScoreAndWin();
    }

    // ====== DAMAGE LOGIC ======

    public void DamageBoss(int amount)
    {
        if (!bossSpawned) return;

        bossLives -= amount;
        if (bossLives <= 0)
        {
            bossLives = 0;
            score += 5000;

            if (currentBoss != null)
            {
                AudioSource bossAudio = currentBoss.GetComponent<AudioSource>();
                if (bossAudio != null) bossAudio.Stop();
                Destroy(currentBoss);
            }

            UpdateUI();
            StartCoroutine(BossDeathSequence(2f));
        }
        else
        {
            score += 2000;
            UpdateUI();
        }
    }

    // ====== SCORE AND DAMAGE UTILITIES ======

    public void AddScore(int value)
    {
        if (isGameOver) return;
        score += value;
        UpdateUI();
    }

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

    // ====== INTERFACE AND GAME STATES UTILITIES ======

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE NAVE: " + score;

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

    void ShowFinalScoreAndWin()
    {
        if (isGameOver) return;
        isGameOver = true;

        PlayerPrefs.SetInt("FinalScore", score);
        SceneManager.LoadScene("VictoryScene");
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        SceneManager.LoadScene("GameOverScene");
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