using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Configurações de Tiro")]
    public GameObject raioPrefab;      // arraste seu prefab aqui
    public Transform firePoint;        // ponto de saída do tiro
    public float fireRate = 0.25f;     // tempo entre tiros

    private float fireTimer = 0f;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    void Shoot()
    {
        if (raioPrefab != null && firePoint != null)
        {
            Instantiate(raioPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
