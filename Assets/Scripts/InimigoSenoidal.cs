using UnityEngine;

public class InimigoSenoidal : InimigoBase
{
    // VARI�VEIS ESPEC�FICAS DE MOVIMENTO
    public float frequency = 1f;
    public float magnitude = 2f;

    // Sobrescreve o Mover() do InimigoBase
    public override void Mover()
    {
        // L�gica do Movimento Tipo 2 (Senoidal/Zig-Zag)
        Vector3 tempPos = initialPosition;
        tempPos.y += Mathf.Sin(Time.time * frequency) * magnitude;
        tempPos.x -= speed * Time.deltaTime; // Mant�m o movimento horizontal
        transform.position = tempPos;
    }

    // Start(), TakeDamage(), Tiro, etc., s�o automaticamente herdados.
}