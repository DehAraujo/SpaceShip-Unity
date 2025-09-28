using UnityEngine;

public class Nave : MonoBehaviour
{
    // Design da Bala (Prefab)
    public GameObject bulletPrefab;

    // Ponto de Disparo (arraste o objeto FirePoint aqui)
    public Transform firePoint;

    // Frequência (Cooldown)
    public float fireRate = 0.3f; // 3 tiros por segundo (1 / 0.3s)
    private float nextFireTime = 0f;

    // Custo: Ilimitado (não precisamos de uma variável de munição, por enquanto)

    void Update()
    {
        // Controle: Tecla Espaço ou Botão A
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Define o tempo do próximo disparo para o Cooldown
        nextFireTime = Time.time + fireRate;

        // Cria a bala (Comportamento: Instanciação)
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}