using UnityEngine;

public class Nave : MonoBehaviour
{
    // Design da Bala (Prefab)
    public GameObject bulletPrefab;

    // Ponto de Disparo (arraste o objeto FirePoint aqui)
    public Transform firePoint;

    // Frequ�ncia (Cooldown)
    public float fireRate = 0.3f; // 3 tiros por segundo (1 / 0.3s)
    private float nextFireTime = 0f;

    // Custo: Ilimitado (n�o precisamos de uma vari�vel de muni��o, por enquanto)

    void Update()
    {
        // Controle: Tecla Espa�o ou Bot�o A
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Define o tempo do pr�ximo disparo para o Cooldown
        nextFireTime = Time.time + fireRate;

        // Cria a bala (Comportamento: Instancia��o)
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}