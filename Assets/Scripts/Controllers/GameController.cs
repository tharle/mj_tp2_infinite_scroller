using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int m_AmountTimeScore = 12;
    HUDManager m_HUDManager;

    private GameState m_State = GameState.GAME;
    private float m_Score = 0;
    private float m_ElapseTimmer = 0;
    private int m_Timmer = 0;
    private Session m_Session = Session.SPRING;

    private void Start()
    {
        m_HUDManager = FindAnyObjectByType<HUDManager>();
    }

    private void Update()
    {
        UpdateTimmer();
    }

    private void UpdateTimmer()
    {
        m_ElapseTimmer += Time.deltaTime;
        if(m_ElapseTimmer > 1) // change le timmer chaque 1s
        {
            m_ElapseTimmer -= 1;
            m_HUDManager.UpdateTimmer(++m_Timmer);
        }
    }

    public Session getCurrentSession()
    {
        return m_Session;
    }

    public void AddScorePoints(float score)
    {
        score += score * (m_Timmer / m_AmountTimeScore);
        m_Score += score;
        ShowScorePoints(score);
    }

    private void ShowScorePoints(float score) {
        Debug.Log("GameController - ShowScorePoints: " + score);
    }

    public void OnGameOver()
    {
        // TODO show game over menu
        ChangeGameState(GameState.GAME_OVER);
        Debug.Log("GameController: GAME OVER");
    }

    public void ChangeGameState(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.MAIN_MENU:
                PauseGame();
                break;
            case GameState.PAUSE_MENU:
                PauseGame();
                break;
            case GameState.GAME_OVER:
                PauseGame();
                break;
            case GameState.GAME:
            default:
                ResumeGame();
                break;
        }

        m_State = gameState;
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
