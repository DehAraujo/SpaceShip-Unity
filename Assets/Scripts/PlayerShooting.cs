using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f; // Taxa de 3 tiros por segundo
    private float nextFireTime = 0f;

    void Update()
    {
        // CORREÇÃO: Usar Input.GetKey para permitir o tiro contínuo
        // Se você quer que o tiro seja semiautomático (segurando a tecla Espaço)
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            Shoot();
        }

        // Se você preferir tiro único (apenas um toque na tecla), use Input.GetKeyDown(KeyCode.Space)
    }

    void Shoot()
    {
        nextFireTime = Time.time + fireRate;

        if (bulletPrefab != null && firePoint != null)
        {
            // O uso de firePoint.position é o que garante que o tiro saia da ponta!
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Toca o som de tiro (implementaremos isso a seguir)
            // if (SoundManager.instance != null) { SoundManager.instance.PlayShootSound(); }
        }
    }
}