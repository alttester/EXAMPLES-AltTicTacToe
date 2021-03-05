using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public string currentPlayer = "X";
    int rounds = 0;
    bool gameOver = false;
    public GameObject gameOverPanel;
    public TMP_Text gameOverPanelText;

    public Slot[] slots;
    public Slot currentPlayerSlot;
    void Start()
    {
        rounds = 0;
        currentPlayer = "X";
        gameOver = false;
        currentPlayerSlot.XorO.text = currentPlayer;
        gameOverPanel.SetActive(false);
        gameOverPanelText.text = "";
    }

    public void NextRound()
    {
        CheckBoard();
        if (!gameOver)
        {
            rounds++;
            currentPlayer = (currentPlayer == "X") ? "O" : "X";
            currentPlayerSlot.XorO.text = currentPlayer;
        }
    }

    public void CheckBoard()
    {
        if (CheckSlotsLine(0, 1, 2) || CheckSlotsLine(3, 4, 5) || CheckSlotsLine(6, 7, 8) || CheckSlotsLine(0, 3, 6) || CheckSlotsLine(1, 4, 7) || CheckSlotsLine(2, 5, 8) || CheckSlotsLine(0, 4, 8) || CheckSlotsLine(2, 4, 6))
        {
            GameOver();
        }
        else if (rounds == 8)
        {
            GameOver(draw: true);
        }
    }

    public void GameOver(bool draw = false)
    {
        gameOver = true;
        if (draw)
        {
            gameOverPanelText.text = "It's a draw!";
        }
        else
        {
            gameOverPanelText.text = "Player " + currentPlayer + " wins!";
        }
        gameOverPanel.SetActive(true);
    }

    bool CheckSlotsLine(int slot1, int slot2, int slot3)
    {
        return slots[slot1].Player() == currentPlayer && slots[slot2].Player() == currentPlayer && slots[slot3].Player() == currentPlayer;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}

