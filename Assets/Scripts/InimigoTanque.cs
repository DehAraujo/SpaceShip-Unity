using UnityEngine;

public class InimigoTanque : InimigoBase
{
    // Use 'new' ou 'override' se precisar de um Start() diferente
    protected override void Start()
    {
        // Chama o Start() da classe base (para configurar borda, tiro, etc.)
        base.Start();

        // C�DIGO EXCLUSIVO PARA O TANQUE:
        // Exemplo: spriteRenderer.color = Color.gray;
    }

    // O Mover() ser� o padr�o (reto) do InimigoBase
}