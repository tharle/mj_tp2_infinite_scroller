using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float m_BaseScorePerObstacle = 50;
    [SerializeField] private float m_AmountTimeScore = 12;
    HUDManager m_HUDManager;

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

    public void AddScorePoints()
    {
        float score = m_BaseScorePerObstacle * (Time.time % m_AmountTimeScore);
        m_Score += score;
        ShowScorePoints(score);
    }

    private void ShowScorePoints(float score) {
        Debug.Log("GameController - ShowScorePoints: " + score);
    }
}
