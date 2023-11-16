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
    private float m_AmountTimmerToNextSession = 0;
    private float m_TimmerToNextSession = 0;
    private float m_ElapseTimmer = 0;

    private void Start()
    {
        m_HUDManager = FindAnyObjectByType<HUDManager>();
        m_AmountTimmerToNextSession = m_AmountTimeScore * 2;
        LoadPlayerPrefs();
        UpdateScores();

        ChangeGameState(GameState.GAME);
    }

    private void Update()
    {
        UpdateTimmer();

        UpdateSession();
    }

    private void UpdateSession()
    {
        m_TimmerToNextSession += Time.deltaTime;
        if (m_TimmerToNextSession >= m_AmountTimmerToNextSession)
        {
            m_TimmerToNextSession -= m_AmountTimmerToNextSession;
            ChangeToNextSession();
        }
    }

    private void ChangeToNextSession()
    {
        switch (m_Session)
        {
            case Session.SUMMER:
                m_Session = Session.FALL;
                break;
            case Session.FALL:
                m_Session = Session.WINTER;
                break;
            case Session.WINTER:
                m_Session = Session.SPRING;
                break;
            case Session.SPRING:
            default:
                m_Session = Session.SUMMER;
                break;
        }
    }

    public int GetLevel()
    {
        return (m_Timmer / m_AmountTimeScore) + 1;
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
        PlayerPrefs.SetInt("HiScore", m_HiScore);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPrefs()
    {
        m_HiScore = PlayerPrefs.GetInt("HiScore");
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
