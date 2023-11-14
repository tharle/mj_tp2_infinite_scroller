using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TxtMPTime;
    [SerializeField] private TextMeshProUGUI m_TxtMPScore;
    [SerializeField] private TextMeshProUGUI m_TxtMPLife;

    public void UpdateTimmer(int time)
    {
        m_TxtMPTime.text = time.ToString();
    }

    public void UpdateScore(int score)
    {
        m_TxtMPScore.text = score.ToString("000000");
    }

    public void UpdateLife(int life)
    {
        m_TxtMPLife.text = life.ToString("00");
    }
}
