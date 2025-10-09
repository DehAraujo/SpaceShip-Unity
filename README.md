# 🚀 SpaceShip 2D

Um jogo de naves espaciais desenvolvido em **Unity (C#)** onde você enfrenta inimigos, destrói meteoros e derrota um chefão final!

---

## 🕹️ Como jogar

- **Mover:** Use as setas ou `WASD`  
- **Atirar:** Pressione `Espaço`  
- **Slow Motion:** É ativado a cada **20 segundos** para ativar o tempo lento com duração de **6 segundos**. 
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

Durante o jogo, aa cada 20 segundos o Slow Motion é ativado.
Quando ativado:
- O tempo desacelera em **50%** por **6 segundos**.  
- O jogador ganha mais precisão para desviar e mirar.  
- Após o uso, há um **tempo de espera de 20 segundos** antes de ativar novamente.

---

## 🔊 Áudio e Trilha Sonora

O jogo conta com efeitos sonoros e música ambiente para intensificar a imersão espacial:

| Tipo de Som | Arquivo / Efeito | Descrição |
|--------------|------------------|------------|
| **Tiro do Player** | `shoot` | Som emitido ao disparar |
| **Boss Spawn** | `bosssound` | Sinal sonoro de alerta do chefão |
| **Trilha de Fundo** | `backgroundsound` | Música ambiente espacial com batidas suaves |


---

## 💻 Tecnologias utilizadas

- **Unity 2022+**  
- **C#**  
- **TextMeshPro**  
- **Sprites 2D e Física 2D**
- **AudioSource / AudioMixer**
- **Time.timeScale (para Slow Motion)**

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
