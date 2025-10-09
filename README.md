# 🚀 SpaceShip 2D

Um jogo de naves espaciais desenvolvido em **Unity (C#)** onde você enfrenta inimigos, destrói meteoros e derrota um chefão final!

---

## 🕹️ Como jogar

- **Mover:** Use as setas ou `WASD`
- **Atirar:** Pressione `Espaço`
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

---

## 🧠 Créditos
Desenvolvido por **Deise Araújo, Victor Iak, Vinicios Said** ✨  
Projeto para estudos de desenvolvimento de jogos 2D com Unity.
