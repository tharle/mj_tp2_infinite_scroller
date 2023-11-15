using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private GameState m_State = GameState.GAME;

    [SerializeField] private TextMeshProUGUI m_TxtMPTime;
    [SerializeField] private TextMeshProUGUI m_TxtMPScoreInGame;
    [SerializeField] private TextMeshProUGUI m_TxtMPScoreGameOver;
    [SerializeField] private TextMeshProUGUI m_TxtMPHiScoreInGame;
    [SerializeField] private TextMeshProUGUI m_TxtMPHiScoreGameOver;

    [SerializeField] private GameObject m_GameOverScreen;

    private void Start()
    {
        m_GameOverScreen.SetActive(false);
    }

    public void UpdateTimmer(int time)
    {
        m_TxtMPTime.text = time.ToString();
    }

    public void UpdateScores(int score, int hiScore)
    {
        m_TxtMPScoreInGame.text = score.ToString("000000");
        m_TxtMPScoreGameOver.text = score.ToString("000000");
        m_TxtMPHiScoreInGame.text = hiScore.ToString("000000");
        m_TxtMPHiScoreGameOver.text = hiScore.ToString("000000");
    }

    public void ChangeGameState(GameState state)
    {
        m_State = state;

        OnChangeGameState();
    }

    private void OnChangeGameState()
    {
        switch (m_State)
        {
            case GameState.MAIN_MENU:
                PauseGame();
                break;
            case GameState.PAUSE_MENU:
                PauseGame();
                break;
            case GameState.GAME_OVER:
                ShowGameOverScreen();
                break;
            case GameState.GAME:
            default:
                ResumeGame();
                break;
        }
    }

    private void ShowGameOverScreen()
    {
        PauseGame();
        m_GameOverScreen.SetActive(true);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
