# 🚀 SpaceShip 2D

Um jogo de naves espaciais desenvolvido em **Unity (C#)** onde você enfrenta inimigos, destrói meteoros e derrota um chefão final!

---

## 🕹️ Como jogar

- **Mover:** Use as setas ou `WASD`
- **Atirar:** Pressione `Espaço`
- **Objetivo:** Ganhar pontos destruindo meteoros, naves inimigas e o boss final.

---

## 🎯 Regras do jogo

### 👨‍🚀 Player
- 3 vidas.
- Atira da esquerda para a direita.
- Se perder todas as vidas → Game Over.

### 👾 EnemyShip
- 2 vidas.
- Move-se da direita para a esquerda.
- Atira na direção do Player.
- Cada vida perdida vale **500 pontos**.

### ☄️ Meteor
- Move-se de cima para baixo.
- Não atira.
- Cada meteoro destruído vale **100 pontos**.

### 🧠 Boss
- Surge ao atingir **2000 pontos**.
- Possui **4 vidas**.
- Atira da direita para a esquerda em direção ao Player.
- Cada vida perdida dá **+2000 pontos**.
- Ao ser derrotado, o jogador ganha **+5000 pontos** e vence o jogo.

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
