using UnityEngine;

public class Nave : MonoBehaviour
{
� � // Design da Bala (Prefab)
� � public GameObject bulletPrefab;

� � // Ponto de Disparo (arraste o objeto FirePoint aqui)
� � public Transform firePoint;

� � // Frequ�ncia (Cooldown)
� � public float fireRate = 0.3f; // 3 tiros por segundo (1 / 0.3s)

� � // Vari�vel para gerenciar o tempo de espera do tiro
� � private float fireTimer = 0f;

    void Update()
    {
� � � � // 1. O timer DECRESCE usando o tempo N�O ESCALONADO (velocidade normal)
� � � � // Isso garante que a taxa de tiro n�o diminua no slow motion.
� � � � fireTimer -= Time.unscaledDeltaTime;

        // 2. Controle: Verifica "Fire1" (Ctrl/Clique) OU a tecla ESPA�O.
        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) && fireTimer <= 0f)
        {
            Shoot();
        }
    }

    void Shoot()
    {
� � � � // Define o tempo do pr�ximo disparo para o Cooldown (usa o fireRate)
� � � � fireTimer = fireRate;

� � � � // Cria a bala (Comportamento: Instancia��o)
� � � � if (bulletPrefab != null && firePoint != null)
        {
� � � � � � // O script Bullet.cs DEVE usar Time.unscaledDeltaTime para que o PROJ�TIL tamb�m seja r�pido!
� � � � � � Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
