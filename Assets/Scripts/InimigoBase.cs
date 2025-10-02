using UnityEngine;
using System.Collections;

public class InimigoBase : MonoBehaviour
{
    // --- VARI�VEIS DE STATUS PADR�O (Ajuste no Prefab) ---
    public int hp = 3;
    public int scoreValue = 100;
    public float speed = 3f;

    // --- VARI�VEIS DE TIRO ---
    public GameObject enemyBulletPrefab;
    public float fireRate = 2f;
    public Transform firePoint;

    // --- VARI�VEIS DE TIPO DE INIMIGO (Requerido pelo SpawnManager) ---
    public bool isType2 = false; // Flag para movimento Senoidal
    public bool isHeavy = false; // Flag para Inimigo Pesado

    // --- VARI�VEIS DE SPRITES (Configurar no Prefab) ---
    public Sprite basicSprite;
    public Sprite advancedSprite;
    public Sprite heavySprite;

    // --- REFER�NCIAS INTERNAS ---
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected Vector3 initialPosition;
    private float destructionBoundary;

    // =========================================================================

    protected virtual void Start()
    {
        // 1. Inicializa��o
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // PEGA O ANIMATOR AQUI

        // 2. Configura��o da Borda de Destrui��o
        Camera mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.orthographic)
        {
            float camHeight = mainCamera.orthographicSize;
            float camWidth = camHeight * mainCamera.aspect;
            destructionBoundary = mainCamera.transform.position.x - camWidth - 1f;
        }

        // 3. Inicializa o Tiro
        if (enemyBulletPrefab != null)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    void Update()
    {
        Mover();
        CheckBoundaryAndDestroy();
    }

    // --- L�GICA DE MOVIMENTO ---
    public virtual void Mover()
    {
        // Movimento Tipo 1 (Padr�o: Linha Reta para a Esquerda)
        if (isType2)
        {
            // Movimento senoidal (exemplo simples)
            float newY = initialPosition.y + Mathf.Sin(Time.time * 5f) * 1f;
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, newY, 0);
        }
        else
        {
            // Movimento linear padr�o
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    // --- L�GICA DE VISUAL (M�todo requerido pelo SpawnManager) ---
    public void SetVisuals()
    {
        if (spriteRenderer == null) return;

        if (isHeavy && heavySprite != null)
        {
            spriteRenderer.sprite = heavySprite;
        }
        else if (isType2 && advancedSprite != null)
        {
            spriteRenderer.sprite = advancedSprite;
        }
        else if (basicSprite != null)
        {
            spriteRenderer.sprite = basicSprite;
        }
    }


    // --- L�GICA DE DANO E FIM DE JOGO ---

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void CheckBoundaryAndDestroy()
    {
        if (transform.position.x < destructionBoundary)
        {
            Destroy(gameObject);
        }
    }

    // =================================================================================
    // --- M�TODOS DE ANIMA��O E TIRO ---
    // =================================================================================

    // M�TODO 1: � CHAMADO PELO EVENTO DE ANIMA��O (Animation Event)
    // InimigoBase.cs

    public void ShootBulletEvent()
    {
        if (enemyBulletPrefab != null && firePoint != null)
        {
            // 1. Encontra o objeto do jogador na cena (com a tag "Player")
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // 2. Calcula a dire��o: Posi��o do Jogador - Posi��o de Disparo
                Vector2 directionToPlayer = player.transform.position - firePoint.position;

                // 3. Cria a bala
                GameObject newBulletObj = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);

                // 4. Obt�m o script da bala
                EnemyBullet bulletScript = newBulletObj.GetComponent<EnemyBullet>();

                if (bulletScript != null)
                {
                    // 5. Define a dire��o da bala antes de Start() rodar
                    bulletScript.SetDirection(directionToPlayer);
                }
            }
            // Se o player for nulo (destru�do), a bala n�o � instanciada
        }
    }

    IEnumerator ShootRoutine()
    {
        // Pequeno atraso inicial para o primeiro tiro
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        while (true)
        {
            if (animator != null)
            {
                // 1. LINHA REMOVIDA: N�o precisa mais chamar o trigger de ataque!
                // animator.SetTrigger("Attack"); 
            }

            // 2. DISPARA A BALA IMEDIATAMENTE (O tiro continua funcionando aqui)
            ShootBulletEvent();

            // 3. Espera pelo Cooldown antes do pr�ximo ciclo
            yield return new WaitForSeconds(fireRate);
        }
    }   

}