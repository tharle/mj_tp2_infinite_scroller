using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private int m_AmountTimeScore = 12;
    HUDManager m_HUDManager;

    private Session m_Session = Session.SPRING;

    private int m_Score = 0;
    private int m_HiScore = 0;
    private int m_Timmer = 0;
    private float m_ElapseTimmer = 0;

    private void Start()
    {
        m_HUDManager = FindAnyObjectByType<HUDManager>();
        UpdateScores();

        ChangeGameState(GameState.GAME);
        LoadPlayerPrefs();
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

        m_HiScore = m_Score >= m_HiScore ? m_Score : m_HiScore;

        UpdateScores();
    }

    private void ShowScorePoints(int score) {
        Debug.Log("GameController - ShowScorePoints: " + score);
    }

    public void LossLife()
    {
        SavePlayerPrefs();
        ChangeGameState(GameState.GAME_OVER);
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("HiScore", m_Score);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPrefs()
    {
        m_Score = PlayerPrefs.GetInt("HiScore", 0);
    } 

    public void ChangeGameState(GameState gameState)
    {
        m_HUDManager.ChangeGameState(gameState);
    }


    // ---------------------------------------------
    // HUD UPDATES
    // ---------------------------------------------
    private void UpdateTimmer()
    {
        m_ElapseTimmer += Time.deltaTime;
        if (m_ElapseTimmer > 1) // change le timmer chaque 1s
        {
            m_ElapseTimmer -= 1;
            m_HUDManager.UpdateTimmer(++m_Timmer);
        }
    }

    private void UpdateScores()
    {
        m_HUDManager.UpdateScores(m_Score, m_HiScore);
    }

    // ---------------------------------------------
    // HUD UPDATES
    // ---------------------------------------------
    public void OnTryAgain()
    {
        SceneManager.LoadScene(GameParameters.SceneName.GAME);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(GameParameters.SceneName.MAIN_MENU);
    }

}
