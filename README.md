# 🚀 SpaceShip

Este é um protótipo de jogo **2D Shoot 'Em Up**, onde o jogador controla uma nave espacial, derrota inimigos e utiliza um recurso estratégico de **Time Warp (Slow Motion)** para ganhar vantagem em combate.  

---

## 🎮 Funcionalidades Chave

- **Combate Básico**: Nave do jogador com sistema de disparos (cooldown incluso).  
- **Inimigos**: Prefabs com HP, ataques e valores de pontuação definidos.  
- **Pontuação**: Sistema de Score que desbloqueia o recurso de Time Warp.  
- **Parallax Scrolling**: Fundo infinito com múltiplas camadas para profundidade visual.  
- **Time Warp (Slow Motion)**: Mecânica central que manipula a velocidade do tempo no jogo.  

---

## ⏱️ Time Warp (Mecânica Central)

O recurso **Time Warp** é controlado pelo `TimeController` e afeta todo o cenário:

| Aspecto              | Regra de Implementação                                                   | Vantagem do Jogador |
|----------------------|---------------------------------------------------------------------------|----------------------|
| **Controle**         | Define `Time.timeScale` para `0.3`.                                      |                      |
| **Gatilhos**         | Ativado ao coletar um Power-Up **OU** quando o Score atinge **1000 pontos**. |                      |
| **Duração**          | Dura **5 segundos**, usando **tempo real** (não desacelera o timer).     |                      |
| **Movimentos Lentos** | Inimigos e fundo usam `Time.deltaTime`.                                  | Mais tempo de reação |
| **Movimentos Rápidos** | Nave e tiros usam `Time.unscaledDeltaTime`.                             | Mantêm velocidade normal, facilitando a pontaria |

---

## 🌌 Parallax Scrolling

- **Velocidade de Rolagem (`parallaxEffect`)**: Definida individualmente para cada camada.  
  - Estrelas (distantes) = velocidade baixa  
  - Camadas próximas = velocidade alta  

- **Loop Infinito**:  
  - O script reposiciona sprites quando saem da tela, criando fundo sem fim.  

- **Estrutura**:  
  - Montado com **sprites idênticos**, posicionados lado a lado com `Parallax.cs` anexado.  

---

## 🔫 Combate e Entidades

### Nave do Jogador e Tiros
- Dispara apenas **para frente**, respeitando cooldown.  
- **Bala** (Prefab):  
  - Alta velocidade  
  - Destrói-se ao atingir inimigo ou sair da tela  

### Inimigos e Pontuação
- Cada inimigo possui:  
  - **HP** (pontos de vida)  
  - **ScoreValue** (pontos ao ser destruído)  
  - **Capacidade de ataque** (disparam projéteis ou realizam colisões que causam dano)  

- Comportamento:  
  - Movem-se da **direita para a esquerda**  
  - Podem **atacar o jogador** durante o deslocamento  
  - Ao serem destruídos, notificam o `GameManager` para atualizar a pontuação  

---

## 🛠️ Tecnologias Usadas
- **Unity 2D**  
- **C#** para scripts  
- **Sprites** para fundo e entidades  

---

## 📌 Sobre
Este projeto foi desenvolvido como protótipo para explorar **mecânicas de combate com desaceleração do tempo em jogos de nave 2D**.  
💡 “Sugestões e contribuições são muito bem-vindas! Sinta-se à vontade para abrir uma issue ou enviar um pull request.”


