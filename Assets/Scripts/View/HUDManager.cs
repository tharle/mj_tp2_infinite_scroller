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
    [SerializeField] private TextMeshProUGUI m_TxtMPLifeInGame;
    [SerializeField] private TextMeshProUGUI m_TxtMPLifeStartScreen;

    [SerializeField] private GameObject m_StartLevelScreen;
    [SerializeField] private GameObject m_GameOverScreen;

    private void Start()
    {
        m_GameOverScreen.SetActive(false);
        m_StartLevelScreen.SetActive(false);
    }

    public void UpdateTimmer(int time)
    {
        m_TxtMPTime.text = time.ToString();
    }

    public void UpdateScore(int score)
    {
        m_TxtMPScoreInGame.text = score.ToString("000000");
        m_TxtMPScoreGameOver.text = score.ToString("000000");
    }

    public void UpdateLife(int life)
    {
        m_TxtMPLifeInGame.text = life.ToString("00");
        m_TxtMPLifeStartScreen.text = life.ToString("00");
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

    public void ShowGameOverScreen()
    {
        PauseGame();
        m_GameOverScreen.SetActive(true);
    }

    public void ShowStartLevelScreen()
    {
        PauseGame();
        m_StartLevelScreen.SetActive(true); 
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
