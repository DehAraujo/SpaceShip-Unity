# 🚀 SpaceShip 2D

Um jogo de naves espaciais desenvolvido em **Unity (C#)** onde você enfrenta inimigos, destrói meteoros e derrota um chefão final!

---

## 🕹️ Como jogar

- **Mover:** Use as setas ou `WASD`  
- **Atirar:** Pressione `Espaço`  
- **Slow Motion:** Pressione `Shift` para ativar o tempo lento (disponível a cada **20 segundos**, dura **6 segundos**)  
- **Objetivo:** Ganhar pontos destruindo meteoros, naves inimigas e o boss final.

---

## 🎯 Regras do jogo

| Entidade      | Vidas | Movimento          | Atira | Direção do tiro         | Pontuação                       |
| ------------- | ----- | ------------------ | ----- | ----------------------- | ------------------------------- |
| **Player**    | 3     | Esquerda ↔ Direita | Sim   | ➡️ (esquerda → direita) | —                               |
| **EnemyShip** | 2     | Direita → Esquerda | Sim   | ➡️ (direita → esquerda) | +200 por vida                   |
| **Meteor**    | 1     | Cima → Baixo       | Não   | —                       | +100                            |
| **Boss**      | 4     | Fixo (Direita)     | Sim   | ➡️ (direita → esquerda) | +2000 por vida, +5000 ao morrer |

---

## 🧩 Estrutura de Cenas

| Cena | Descrição |
|------|------------|
| **LoaderScene** | Tela inicial com botão "Start" |
| **SampleScene** | Gameplay principal |
| **VictoryScene** | Tela de vitória mostrando pontuação final |
| **GameOverScene** | Tela de derrota com botão "Retry" |

---

## ⚡ Slow Motion (Modo Especial)

Durante o jogo, o jogador pode ativar o **modo Slow Motion** pressionando `Shift`.  
Quando ativado:
- O tempo desacelera em **50%** por **6 segundos**.  
- O jogador ganha mais precisão para desviar e mirar.  
- Após o uso, há um **tempo de recarga de 20 segundos** antes de poder ativar novamente.

---

## 💻 Tecnologias utilizadas

- **Unity 2022+**  
- **C#**  
- **TextMeshPro**  
- **Sprites 2D e Física 2D**

---

## 🏗️ Estrutura de Scripts

- `GameManager.cs` → controla pontuação, vidas e transições de cena.  
- `PlayerController.cs` → controla o movimento e disparos do jogador.  
- `EnemyShip.cs` → comportamento das naves inimigas.  
- `Meteor.cs` → comportamento dos meteoros.  
- `Boss.cs` → comportamento do chefão final.  
- `Spawner.cs` → cria inimigos aleatoriamente.  
- `SlowMotionController.cs` → gerencia a ativação do modo Slow Motion.  

---

## 🧠 Créditos

Desenvolvido por **Deise Araújo, Victor Iak e Vinicios Said** ✨  
Projeto para estudos de desenvolvimento de jogos 2D com Unity.

---

## 💬 Sugestões

🪐 *O espaço é infinito, e as ideias também!*  
Aceitamos sugestões para novas mecânicas, fases ou melhorias — envie sua ideia e ajude a evoluir o **SpaceShip 2D**! 🚀
