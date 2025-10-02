using UnityEngine;

public class InimigoTanque : InimigoBase
{
    // Use 'new' ou 'override' se precisar de um Start() diferente
    protected override void Start()
    {
        // Chama o Start() da classe base (para configurar borda, tiro, etc.)
        base.Start();

        // CÓDIGO EXCLUSIVO PARA O TANQUE:
        // Exemplo: spriteRenderer.color = Color.gray;
    }

    // O Mover() será o padrão (reto) do InimigoBase
}