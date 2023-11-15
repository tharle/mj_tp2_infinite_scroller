using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int m_AmountTimeScore = 12;
    HUDManager m_HUDManager;

    private Session m_Session = Session.SPRING;

    private int m_Level = 1;
    private int m_Score = 0;
    private int m_Hiscore = 1000;
    private int m_Life = 3;
    private int m_Timmer = 0;
    private float m_ElapseTimmer = 0;

    private void Start()
    {
        m_HUDManager = FindAnyObjectByType<HUDManager>();
        UpdateScore();
        UpdateLife();
    }

    private void Update()
    {
        UpdateTimmer();
    }

    public int GetLevel()
    {
        return m_Timmer / m_AmountTimeScore;
    }

    public Session getCurrentSession()
    {
        return m_Session;
    }

    public void AddScorePoints(int score)
    {
        score += score * GetLevel();
        m_Score += score;
        ShowScorePoints(score);
        UpdateScore();
    }

    private void ShowScorePoints(int score) {
        // TODO afficher le texte dans le écran, dans le moment de la collition
        Debug.Log("GameController - ShowScorePoints: " + score);
    }

    public void OnGameOver()
    {
        // TODO show game over menu
        ChangeGameState(GameState.GAME_OVER);
        Debug.Log("GameController: GAME OVER");
    }


    // ---------------------------------------------
    // HUD UPDATES
    // ---------------------------------------------

    public void ChangeGameState(GameState gameState)
    {
        m_HUDManager.ChangeGameState(gameState);
    }

    private void UpdateTimmer()
    {
        m_ElapseTimmer += Time.deltaTime;
        if (m_ElapseTimmer > 1) // change le timmer chaque 1s
        {
            m_ElapseTimmer -= 1;
            m_HUDManager.UpdateTimmer(++m_Timmer);
        }
    }

    private void UpdateScore()
    {
        m_HUDManager.UpdateScore(m_Score);
    }

    private void UpdateLife()
    {
        m_HUDManager.UpdateLife(m_Life);
    }
}
